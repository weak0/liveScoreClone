﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories
{
    public class GameRepository : IGenericRepository<Game>, IDisposable
    {
        private readonly LiveScoreReporterDbContext _context;
        public GameRepository(LiveScoreReporterDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public IEnumerable<Game> GetAll()
        {
           return _context.Games.ToList();
        }

        public Task<List<Game>> GetAllAsync()
        {
            return _context.Games.ToListAsync();
        }

        public Game GetById(int id)
        {
            return _context.Games.Find(id);
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }


        public bool Remove(int id)
        {
            var game = _context.Games.Find(id);
            
            if (game is null) 
                return false;
            
            _context.Games.Remove(game);
            
            return true;
        }

        public void Add(in Game sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }

        public void Update(in Game sender)
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

        public Game Select(Expression<Func<Game, bool>> predicate, Func<IQueryable<Game>, IQueryable<Game>> include = null)
        {
            IQueryable<Game> query = _context.Set<Game>();

            if (include != null)
            {
                query = include(query);
            }

            return query.FirstOrDefault(predicate);
        }

        public async Task<Game> SelectAsync(Expression<Func<Game, bool>> predicate, Func<IQueryable<Game>, IQueryable<Game>> include = null)
        {
            IQueryable<Game> query = _context.Set<Game>();

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
