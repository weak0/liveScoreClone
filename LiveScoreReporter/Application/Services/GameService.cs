using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;

namespace LiveScoreReporter.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISerializerService _serializerService;


        public GameService(IGameRepository gameRepository, ISerializerService serializerService)
        {
            _gameRepository = gameRepository;
            _serializerService = serializerService;
        }
        public async Task<List<Game>> GetGamesWithDetailsAsync()
        {
            return await _gameRepository.GetAllGamesWithScoreAndTeamsAsync();
        }

        public List<GameWithDetailsDto> MapGamesToDto(List<Game> gamesWithDetails)
        {
            return gamesWithDetails.Select(g => new GameWithDetailsDto()
            {
                GameId = g.FixtureId,
                HomeTeamName = g.HomeTeam.Name,
                HomeTeamLogo = g.HomeTeam.Logo,
                AwayTeamName = g.AwayTeam.Name,
                AwayTeamLogo = g.AwayTeam.Logo,
                GameResult = $"{g.Score.Home}:{g.Score.Away}"
            }).ToList();
        }
        public string SerializeGamesToJson(List<GameWithDetailsDto> gamesWithDetailsDtos)
        {
            return _serializerService.SerializeObjectToJson(gamesWithDetailsDtos);
        }
    }
}
