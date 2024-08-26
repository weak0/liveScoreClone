using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.EFCore.Infrastructure.Entities;

namespace LiveScoreReporter.Application.Services
{
    public interface IFrontendService
    {
        Task<List<Game>> GetGamesWithDetailsAsync();
        List<GameWithDetailsDto> MapGamesToDto(List<Game> gamesWithDetails);
        string SerializeGamesToJson(List<GameWithDetailsDto> gamesWithDetailsDtos);
    }
}
