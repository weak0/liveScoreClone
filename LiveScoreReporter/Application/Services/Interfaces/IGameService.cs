using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using Lineup = LiveScoreReporter.EFCore.Infrastructure.Entities.Lineup;

namespace LiveScoreReporter.Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game> GetSingleGameWithDetailsAsync(int gameId);
        GameDto MapSingleGameToDto(Game gamesWithDetails);
        string SerializeSingleGameToJson(GameDto gameDtos);

        Task<List<Game>> GetGamesWithDetailsAsync();
        List<GameDto> MapGamesToDto(List<Game> gamesWithDetails);
        string SerializeGamesToJson(List<GameDto> gamesWithDetailsDtos);
        Task<List<Lineup>> GetGameLineupAsync(int gameId);
        public GameDetailsDto MapToGameDetailsDto(Game game, Lineup homeTeamLineup, Lineup awayTeamLineup);
    }
}
