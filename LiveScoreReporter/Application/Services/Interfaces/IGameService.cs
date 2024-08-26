using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.EFCore.Infrastructure.Entities;

namespace LiveScoreReporter.Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game> GetSingleGameWithDetailsAsync(int gameId);
        GameWithDetailsDto MapSingleGameToDto(Game gamesWithDetails);
        string SerializeSingleGameToJson(GameWithDetailsDto gameWithDetailsDtos);

        Task<List<Game>> GetGamesWithDetailsAsync();
        List<GameWithDetailsDto> MapGamesToDto(List<Game> gamesWithDetails);
        string SerializeGamesToJson(List<GameWithDetailsDto> gamesWithDetailsDtos);
    }
}
