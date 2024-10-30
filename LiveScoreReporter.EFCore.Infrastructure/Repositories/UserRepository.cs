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
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly LiveScoreReporterDbContext _context;

        public UserRepository(LiveScoreReporterDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public Task<List<User>> GetAllAsync()
        {
            return _context.Users.ToListAsync();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Remove(int id)
        {
            var user = _context.Users.Find(id);

            if (user is null)
                return false;

            _context.Users.Remove(user);

            return true;
        }

        public void Add(in User sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }

        public void Update(in User sender)
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

        public User Select(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IQueryable<User>> include = null)
        {
            IQueryable<User> query = _context.Set<User>();

            if (include != null)
            {
                query = include(query);
            }

            return query.FirstOrDefault(predicate);
        }

        public async Task<User> SelectAsync(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IQueryable<User>> include = null)
        {
            IQueryable<User> query = _context.Set<User>();

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
