using LiveScoreReporter.EFCore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        void Add(in Player sender);
        Task<int> SaveAsync();

        Task<Player> SelectAsync(Expression<Func<Player, bool>> predicate,
            Func<IQueryable<Player>, IQueryable<Player>> include = null);

    }
}
