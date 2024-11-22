using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using League = LiveScoreReporter.EFCore.Infrastructure.Entities.League;
using Player = LiveScoreReporter.EFCore.Infrastructure.Entities.Player;
using Score = LiveScoreReporter.EFCore.Infrastructure.Entities.Score;
using Team = LiveScoreReporter.EFCore.Infrastructure.Entities.Team;
using ResponseLeague = LiveScoreReporter.Application.Models.League;
using ResponseTeam = LiveScoreReporter.Application.Models.Teams;
using ResponseLineup = LiveScoreReporter.Application.Models.Lineup;
using ResponseScore = LiveScoreReporter.Application.Models.Score;


namespace LiveScoreReporter.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly RestClient _restClient;
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILeagueRepository _leagueRepository;

        public MatchService(IGameRepository gamesRepository, ITeamRepository teamRepository, IScoreRepository scorerRepository, IPlayerRepository playerRepository, ILeagueRepository leagueRepository)
        {
            _gameRepository = gamesRepository;
            _teamRepository = teamRepository;
            _scoreRepository = scorerRepository;
            _playerRepository = playerRepository;
            _leagueRepository = leagueRepository;
            _restClient = new RestClient("https://v3.football.api-sports.io/");
        }

        public async Task<Root> GetMatchDetailsAsync(int fixtureId)
        {
            try
            {
                var request = new RestRequest($"fixtures?id={fixtureId}");
                request.AddHeader("x-apisports-key", "4038020fe2c0d80536856c4f340a1732");

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
        public async Task AddOrUpdatePlayersAsync(IEnumerable<Lineup>? lineups)
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
        private async Task<Score> GetOrAddScoreAsync(int fixtureId, ResponseScore obj)
        {
            var existingScore = _scoreRepository.Select(s => s.GameId == fixtureId);
          
            if (existingScore != null)
                return existingScore;
            

            var score = new Score
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

            var league = new League
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

            var player = new Player
            {
                Id = playerData.Id,
                Name = playerData.Name
            };

            _playerRepository.Add(player);
        }
    }
}
