using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories
{
    public class ScoreRepository : IGenericRepository<Score>, IDisposable
    {
        private readonly LiveScoreReporterDbContext _context;

        public ScoreRepository(LiveScoreReporterDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public IEnumerable<Score> GetAll()
        {
            return _context.Scores.ToList();
        }

        public Task<List<Score>> GetAllAsync()
        {
            return _context.Scores.ToListAsync();
        }

        public Score GetById(int id)
        {
            return _context.Scores.Find(id);
        }

        public async Task<Score> GetByIdAsync(int id)
        {
            return await _context.Scores.FindAsync(id);
        }

        public bool Remove(int id)
        {
            var score = _context.Scores.Find(id);

            if (score is null)
                return false;

            _context.Scores.Remove(score);

            return true;
        }

        public void Add(in Score sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }

        public void Update(in Score sender)
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

        public Score Select(Expression<Func<Score, bool>> predicate)
        {
            return _context.Scores
                .Where(predicate).FirstOrDefault()!;
        }

        public async Task<Score> SelectAsync(Expression<Func<Score, bool>> predicate)
        {
            return (await _context.Scores
                .Where(predicate).FirstOrDefaultAsync())!;
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
