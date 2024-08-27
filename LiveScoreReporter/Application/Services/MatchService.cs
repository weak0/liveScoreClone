using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.Controllers;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using LiveScoreReporter.MockApiAssets;
using Newtonsoft.Json;
using RestSharp;
using League = LiveScoreReporter.EFCore.Infrastructure.Entities.League;
using Player = LiveScoreReporter.EFCore.Infrastructure.Entities.Player;
using Score = LiveScoreReporter.EFCore.Infrastructure.Entities.Score;
using Team = LiveScoreReporter.EFCore.Infrastructure.Entities.Team;

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
                var request = new RestRequest($"fixtures?id={fixtureId}", Method.Get);
                request.AddHeader("x-apisports-key", "");

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
            await using (var transaction = await _gameRepository.Context.Database.BeginTransactionAsync())
            {
                try
                {
                    var fixtureId = obj.Response.First().Fixture.Id;

                    var score = await GetOrAddScoreAsync(fixtureId);

                    await AddOrUpdateLeagueAsync(obj);
                    await AddOrUpdateTeamsAsync(obj);
                    await AddOrUpdatePlayersAsync(obj);

                    await GetOrAddFixtureAsync(fixtureId, score.Id, obj);

                    await transaction.CommitAsync();
                  
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    return false;
                }
            }
        }
        private async Task<Game> GetOrAddFixtureAsync(int fixtureId, int scoreId, Root obj)
        {
            var existingFixture = await _gameRepository.SelectAsync(f => f.FixtureId == fixtureId);
            
            if (existingFixture != null)
                return existingFixture;
            
            var fixture = new Game
            {
                FixtureId = fixtureId,
                HomeTeamId = obj.Response.First().Teams.Home.Id,
                AwayTeamId = obj.Response.First().Teams.Away.Id,
                LeagueId = obj.Response.First().League.Id,
                ScoreId = scoreId
            };

            _gameRepository.Add(fixture);
            await _gameRepository.SaveAsync();
           
            return fixture;
        }
        private async Task<Score> GetOrAddScoreAsync(int fixtureId)
        {
            var existingScore = _scoreRepository.Select(s => s.GameId == fixtureId);
          
            if (existingScore != null)
                return existingScore;
            

            var score = new Score
            {
                Home = 0,
                Away = 0,
                Result = "",
                GameId = fixtureId
            };

            _scoreRepository.Add(score);
            await _scoreRepository.SaveAsync();
           
            return score;
        }
        private async Task AddOrUpdateLeagueAsync(Root obj)
        {
            var leagueData = obj.Response.First().League;
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
        private async Task AddOrUpdateTeamsAsync(Root obj)
        {
            var teams = obj.Response.First().Teams;

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
        private async Task AddOrUpdatePlayersAsync(Root obj)
        {
            var lineups = obj.Response.First().Lineups;

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
