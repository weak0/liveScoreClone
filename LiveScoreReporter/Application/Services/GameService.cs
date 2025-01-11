using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Lineup = LiveScoreReporter.EFCore.Infrastructure.Entities.Lineup;

namespace LiveScoreReporter.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISerializerService _serializerService;
        private readonly ISeederService _seederService;


        public GameService(IGameRepository gameRepository, ISerializerService serializerService, ISeederService seederService)
        {
            _gameRepository = gameRepository;
            _serializerService = serializerService;
            _seederService = seederService;
        }
        
        public async Task<Game> GetSingleGameWithDetailsAsync(int gameId)
        {
            return  await _gameRepository.GetGameWithScoreAndTeamsAsync(gameId);
        }
        
        public async Task<List<Lineup>> GetGameLineupAsync(int gameId)
        {
            var lineup =  await _gameRepository.GetGameLineupAsync(gameId);
            if (lineup.Count != 0) return lineup;
            
            await  _seederService.SeedGameLineupAsync(gameId);
            lineup = await _gameRepository.GetGameLineupAsync(gameId);
            
            return lineup;
        }

        public GameDto MapSingleGameToDto(Game gamesWithDetails)
        {
            return new GameDto()
            {
                GameId = gamesWithDetails.FixtureId,
                HomeTeamName = gamesWithDetails.HomeTeam.Name,
                HomeTeamLogo = gamesWithDetails.HomeTeam.Logo,
                AwayTeamName = gamesWithDetails.AwayTeam.Name,
                AwayTeamLogo = gamesWithDetails.AwayTeam.Logo,
                GameResult = $"{gamesWithDetails.Score.Home}:{gamesWithDetails.Score.Away}"
            };
        }
        
        public GameDetailsDto MapToGameDetailsDto(Game game, Lineup homeTeamLineup, Lineup awayTeamLineup, List<Event> gameEvents)
        {
            return new GameDetailsDto
            {
                GameId = game.FixtureId,
                HomeTeamName = game.HomeTeam.Name,
                HomeTeamLogo = game.HomeTeam.Logo,
                AwayTeamName = game.AwayTeam.Name,
                AwayTeamLogo = game.AwayTeam.Logo,
                HomeTeamLineup = homeTeamLineup.Players.Select(p => new PlayerDto(p.Id, p.Name, p.Postition) ).ToList(),
                AwayTeamLineup = awayTeamLineup.Players.Select(p => new PlayerDto(p.Id, p.Name, p.Postition)).ToList(),
                Events = gameEvents.Select( e => new EventWithDetailsDto
                {
                    TeamId = e.TeamId,
                    TeamName = e.Team?.Name,
                    Type = e.Type,
                    Details = e.Details,
                    Comments = e.Comments,
                    Time = e.Time,
                    PlayerName = e.Player?.Name,
                    AssistPlayerName = e.AssistPlayer?.Name,
                }).ToList(),
                GameResult = $"{game.Score.Home}:{game.Score.Away}"
            };
        }

        public string SerializeSingleGameToJson(GameDto gameDtos)
        {
            return _serializerService.SerializeObjectToJson(gameDtos);
        }

        public async Task<List<Game>> GetGamesWithDetailsAsync()
        {
            return await _gameRepository.GetAllGamesWithScoreAndTeamsAsync();
        }

        public List<GameDto> MapGamesToDto(List<Game> gamesWithDetails)
        {
            return gamesWithDetails.Select(g => new GameDto()
            {
                GameId = g.FixtureId,
                HomeTeamName = g.HomeTeam.Name,
                HomeTeamLogo = g.HomeTeam.Logo,
                AwayTeamName = g.AwayTeam.Name,
                AwayTeamLogo = g.AwayTeam.Logo,
                GameResult = $"{g.Score.Home}:{g.Score.Away}"
            }).ToList();
        }
        public string SerializeGamesToJson(List<GameDto> gamesWithDetailsDtos)
        {
            return _serializerService.SerializeObjectToJson(gamesWithDetailsDtos);
        }
    }
}
