using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;

namespace LiveScoreReporter.Application.Services;

public class PlayerService(IPlayerRepository playerRepository) : IPlayerService
{
    public async Task<Player> GetPlayerAsync(int playerId)
    {
       return await playerRepository.GetPlayerAsync(playerId);
    }

    public async Task<ICollection<Player>> GetPlayersAsync()
    {
       return await playerRepository.GetAllAsync();
       
    }
}