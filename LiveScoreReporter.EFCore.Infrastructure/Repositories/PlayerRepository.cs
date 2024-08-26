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

        public IEnumerable<Player> GetAll()
        {
            return _context.Players.ToList();
        }

        public Task<List<Player>> GetAllAsync()
        {
            return _context.Players.ToListAsync();
        }

        public Player GetById(int id)
        {
            return _context.Players.Find(id);
        }

        public async Task<Player> GetByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public bool Remove(int id)
        {
            var player = _context.Players.Find(id);

            if (player is null)
                return false;

            _context.Players.Remove(player);

            return true;
        }

        public void Add(in Player sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }

        public void Update(in Player sender)
        {
            _context.Entry(sender).State = EntityState.Modified;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Player Select(Expression<Func<Player, bool>> predicate, Func<IQueryable<Player>, IQueryable<Player>> include = null)
        {
            IQueryable<Player> query = _context.Set<Player>();

            if (include != null)
            {
                query = include(query);
            }

            return  query.FirstOrDefault(predicate);
        }

        public async Task<Player> SelectAsync(Expression<Func<Player, bool>> predicate, Func<IQueryable<Player>, IQueryable<Player>> include = null)
        {
            IQueryable<Player> query = _context.Set<Player>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(predicate);
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
