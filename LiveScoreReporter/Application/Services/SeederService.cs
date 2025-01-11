using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using Event = LiveScoreReporter.Application.Models.Event;
using Entities = LiveScoreReporter.EFCore.Infrastructure.Entities;
using Team = LiveScoreReporter.EFCore.Infrastructure.Entities.Team;
using ResponseLeague = LiveScoreReporter.Application.Models.League;
using ResponseTeam = LiveScoreReporter.Application.Models.Teams;
using ResponseLineup = LiveScoreReporter.Application.Models.Lineup;


namespace LiveScoreReporter.Application.Services
{
    public class SeederService : ISeederService
    {
        private readonly RestClient _restClient;
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILeagueRepository _leagueRepository;
        private readonly ILineupRepository _lineupRepository;
        private readonly LiveScoreReporterDbContext _context;
        private readonly IEventRepository _eventRepository;
        
        const string  API_KEY = "4038020fe2c0d80536856c4f340a1732";

        public SeederService(IGameRepository gamesRepository, 
            ITeamRepository teamRepository, 
            IScoreRepository scorerRepository, 
            IPlayerRepository playerRepository, 
            ILeagueRepository leagueRepository, 
            ILineupRepository lineupRepository,
            IEventRepository eventRepository,
            LiveScoreReporterDbContext context)
        {
            _gameRepository = gamesRepository;
            _teamRepository = teamRepository;
            _scoreRepository = scorerRepository;
            _playerRepository = playerRepository;
            _leagueRepository = leagueRepository;
            _lineupRepository = lineupRepository;
            _eventRepository = eventRepository;
            _context = context;
            _restClient = new RestClient("https://v3.football.api-sports.io/");
        }

        public async Task<Root> GetMatchDetailsAsync(int fixtureId)
        {
            try
            {
                var request = new RestRequest($"fixtures?id={fixtureId}");
                request.AddHeader("x-apisports-key", API_KEY);

                var response = await _restClient.ExecuteAsync(request);

                if (!response.IsSuccessful)
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.Content}");

                var responseDeserializedToRootObject = JsonConvert.DeserializeObject<Root>(response.Content);

                return responseDeserializedToRootObject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> AddDataToDb(Root obj)
        {
            await using var transaction = await _gameRepository.Context.Database.BeginTransactionAsync();
            try
            {
                foreach (var response in obj.Response)
                {
                    var score = await GetOrAddScoreAsync(response.Fixture.Id, response.Score);

                    await AddOrUpdateLeagueAsync(response.League);
                    await AddOrUpdateTeamsAsync(response.Teams);
                    await AddOrUpdatePlayersAsync(response.Lineups);

                    await GetOrAddFixtureAsync(response.Fixture.Id, score.Id, response);
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Optionally, log the exception here for debugging purposes
                return false;
            }
        }

        public async Task SeedGameLineupAsync(int fixtureId)
        {
            try
            {
                var request = new RestRequest($"fixtures/lineups?fixture={fixtureId}");
                request.AddHeader("x-apisports-key", API_KEY);

                var response = await _restClient.ExecuteAsync(request);
                
                if (!response.IsSuccessful)
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.Content}");

                var deserializedLineups = JsonConvert.DeserializeObject<ApiListResponse<Models.Lineup>>(response.Content!);
                
                await AddOrUpdatePlayersAsync(deserializedLineups.Response);
                
                var homePlayers = deserializedLineups.Response[0].StartXI
                    .Select(p => _context.Players.Local.FirstOrDefault(pl => pl.Id == p.Player.Id)
                                 ?? _context.Players.Find(p.Player.Id)
                                 ?? new Entities.Player { Id = p.Player.Id })
                    .ToList();

                var awayPlayers = deserializedLineups.Response[1].StartXI
                    .Select(p => _context.Players.Local.FirstOrDefault(pl => pl.Id == p.Player.Id)
                                 ?? _context.Players.Find(p.Player.Id)
                                 ?? new Entities.Player { Id = p.Player.Id  })
                    .ToList();
                
                var homeTeamLineUp = new Entities.Lineup
                {
                    GameId = fixtureId,
                    TeamId = deserializedLineups.Response[0].Team.Id,
                    Players = homePlayers
                };

                var awayTeamLineup = new Entities.Lineup
                {
                    GameId = fixtureId,
                    TeamId = deserializedLineups.Response[1].Team.Id,
                    Players = awayPlayers
                };

                await _lineupRepository.AddOrUpdateLineupAsync(homeTeamLineUp);
                await _lineupRepository.AddOrUpdateLineupAsync(awayTeamLineup);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task SeedGameEventsAsync(int gameid)
        {
            try
            {
                var request = new RestRequest($"fixtures/events?fixture={gameid}");
                request.AddHeader("x-apisports-key", API_KEY);

                var response = await _restClient.ExecuteAsync(request);
                
                if (!response.IsSuccessful)
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.Content}");

                var deserializedEvents = JsonConvert.DeserializeObject<ApiListResponse<Event>>(response.Content).Response;
                
                var gameEvents = deserializedEvents.Select(e => new Entities.Event
                {
                    GameId = gameid,
                    PlayerId = e.Player.Id,
                    TeamId = e.Team.Id,
                    Details = e.Detail,
                    Comments = e.Comments,
                    AssistPlayerId = e.Assist.Id,
                    Time = e.Time.Elapsed + e.Time.Extra,
                    Type = e.Type switch
                    {
                        "Goal" => EventType.Goal,
                        "Card" => EventType.Card,
                        "Var" => EventType.Var,
                        _ => EventType.Subst
                    }
                    
                }).ToList();
                
                await _eventRepository.AddGameEvents(gameEvents);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task AddOrUpdatePlayersAsync(IEnumerable<ResponseLineup>? lineups)
        {
            if (lineups == null) return;
            foreach (var lineup in lineups)
            {
                foreach (var startXi in lineup.StartXI)
                {
                    await AddOrUpdatePlayerAsync(startXi.Player);
                }

                foreach (var substitute in lineup.Substitutes)
                {
                    await AddOrUpdatePlayerAsync(substitute.Player);
                }
            }

            await _playerRepository.SaveAsync();
        }
        private async Task<Game> GetOrAddFixtureAsync(int fixtureId, int scoreId, Response obj)
        {
            var existingFixture = await _gameRepository.SelectAsync(f => f.FixtureId == fixtureId);
            
            if (existingFixture != null)
                return existingFixture;
            
            var fixture = new Game
            {
                FixtureId = fixtureId,
                HomeTeamId = obj.Teams.Home.Id,
                AwayTeamId = obj.Teams.Away.Id,
                LeagueId = obj.League.Id,
                ScoreId = scoreId
            };

            _gameRepository.Add(fixture);
            await _gameRepository.SaveAsync();
           
            return fixture;
        }
        private async Task<Entities.Score> GetOrAddScoreAsync(int fixtureId, Models.Score obj)
        {
            var existingScore = _scoreRepository.Select(s => s.GameId == fixtureId);
          
            if (existingScore != null)
                return existingScore;
            

            var score = new Entities.Score
            {
                Home = obj.Fulltime.Home.GetValueOrDefault(),
                Away = obj.Fulltime.Away.GetValueOrDefault(),
                Result = "" ,
                GameId = fixtureId
            };

            _scoreRepository.Add(score);
            await _scoreRepository.SaveAsync();
           
            return score;
        }
        private async Task AddOrUpdateLeagueAsync(ResponseLeague leagueData)
        {
            var existingLeague = await _leagueRepository.SelectAsync(l => l.Id == leagueData.Id);
            
            if (existingLeague != null) 
                return;

            var league = new Entities.League
            {
                Id = leagueData.Id,
                Name = leagueData.Name,
                Country = leagueData.Country,
                Logo = leagueData.Logo,
                Flag = leagueData.Flag
            };

            _leagueRepository.Add(league);
            await _leagueRepository.SaveAsync();
        }
        private async Task AddOrUpdateTeamsAsync(ResponseTeam teams)
        {
            await AddOrUpdateTeamAsync(teams.Home);
            await AddOrUpdateTeamAsync(teams.Away);
        }
        
        private async Task AddOrUpdateTeamAsync(TeamBase teamData)
        {
            var existingTeam = await _teamRepository.SelectAsync(t => t.Id == teamData.Id);
            
            if (existingTeam != null) 
                return;

            var team = new Team
            {
                Id = teamData.Id,
                Name = teamData.Name,
                Logo = teamData.Logo
            };

            _teamRepository.Add(team);
            await _teamRepository.SaveAsync();
        }
        private async Task AddOrUpdatePlayerAsync(Models.Player playerData)
        {
            var existingPlayer = await _playerRepository.SelectAsync(p => p.Id == playerData.Id);
            
            if (existingPlayer != null) 
                return;

            var player = new Entities.Player
            {
                Id = playerData.Id,
                Name = playerData.Name,
                Postition = playerData.Pos,
            };

            _playerRepository.Add(player);
        }
    }
}
