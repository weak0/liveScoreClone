using LiveScoreReporter.EFCore.Infrastructure.Entities;

namespace LiveScoreReporter.Application.Services.Interfaces;

public interface IPlayerService
{
    Task<Player> GetPlayerAsync(int playerId);
    Task<ICollection<Player>> GetPlayersAsync();
}