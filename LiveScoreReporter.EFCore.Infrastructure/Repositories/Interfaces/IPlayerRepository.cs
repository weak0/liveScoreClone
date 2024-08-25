using LiveScoreReporter.EFCore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
    }
}
