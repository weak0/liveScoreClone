using LiveScoreReporter.EFCore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<Game?> GetGameWithScoreAndTeamsAsync(int fixtureId);
        Task<List<Game>> GetAllGamesWithScoreAndTeamsAsync();
        Task<List<Lineup>> GetGameLineupAsync(int gameId);
    }
}
