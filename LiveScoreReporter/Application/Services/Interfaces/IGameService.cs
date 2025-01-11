using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using Entities = LiveScoreReporter.EFCore.Infrastructure.Entities;


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
        Task<List<Entities.Lineup>> GetGameLineupAsync(int gameId);
        GameDetailsDto MapToGameDetailsDto(Game game, Entities.Lineup homeTeamLineup, Entities.Lineup awayTeamLineup,  List<Entities.Event> gameEvents);
        
        
    }
}
