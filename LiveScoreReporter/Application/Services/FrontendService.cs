using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;

namespace LiveScoreReporter.Application.Services
{
    public class FrontendService : IFrontendService
    {
        private readonly IGameRepository _gameRepository;

        public FrontendService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
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
            return JsonConvert.SerializeObject(gamesWithDetailsDtos);
        }
    }
}
