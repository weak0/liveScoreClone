using LiveScoreReporter.EFCore.Infrastructure.Entities;
using System.Linq.Expressions;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        void Add(in Player sender);
        Task<int> SaveAsync();

        Task<Player?> SelectAsync(Expression<Func<Player, bool>> predicate,
            Func<IQueryable<Player>, IQueryable<Player>> include = null);
        
        Task<List<Player>> GetAllAsync();
        Task<Player?> GetPlayerAsync(int playerId);

    }
}
