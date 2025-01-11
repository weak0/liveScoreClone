using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository, IDisposable
    {
        private readonly LiveScoreReporterDbContext _context;

        public PlayerRepository(LiveScoreReporterDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;
        
        public async Task<Player> GetByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public void Add(in Player sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }
        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<Player?> SelectAsync(Expression<Func<Player, bool>> predicate, Func<IQueryable<Player>, IQueryable<Player>> include = null)
        {
            IQueryable<Player> query = _context.Set<Player>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public Task<List<Player>> GetAllAsync()
        {
            return _context.Players.ToListAsync();
        }
        
        public async Task<Player?> GetPlayerAsync(int playerId)
        {
            return await _context.Players
                .AsNoTracking()
                .Include(p => p.Events)
                .Include(p => p.Lineups)
                .ThenInclude(l => l.Team)
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
