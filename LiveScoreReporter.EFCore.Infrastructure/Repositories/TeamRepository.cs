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
    public class TeamRepository : ITeamRepository, IDisposable
    {
        private readonly LiveScoreReporterDbContext _context;

        public TeamRepository(LiveScoreReporterDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.ToList();
        }

        public Task<List<Team>> GetAllAsync()
        {
            return _context.Teams.ToListAsync();
        }

        public Team GetById(int id)
        {
            return _context.Teams.Find(id);
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public bool Remove(int id)
        {
            var team = _context.Teams.Find(id);

            if (team is null)
                return false;

            _context.Teams.Remove(team);

            return true;
        }

        public void Add(in Team sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }

        public void Update(in Team sender)
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

        public Team Select(Expression<Func<Team, bool>> predicate, Func<IQueryable<Team>, IQueryable<Team>> include = null)
        {
            IQueryable<Team> query = _context.Set<Team>();

            if (include != null)
            {
                query = include(query);
            }

            return  query.FirstOrDefault(predicate);
        }

        public async Task<Team> SelectAsync(Expression<Func<Team, bool>> predicate, Func<IQueryable<Team>, IQueryable<Team>> include = null)
        {
            IQueryable<Team> query = _context.Set<Team>();

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

        public async  Task<Team?> GetTeamAsync(int teamId)
        {
           return await _context.Teams
               .AsNoTracking()
               .Include(t => t.HomeGames)
               .Include(t => t.AwayGames)
               .Include(t => t.Lineups)
               .ThenInclude(l => l.Players)
               .FirstOrDefaultAsync(t => t.Id == teamId)!;
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
           return await _context.Teams
                .AsNoTracking()
                .Include(t => t.HomeGames)
                .Include(t => t.AwayGames)
                .Include(t => t.Lineups).ToListAsync();
        }
    }
}
