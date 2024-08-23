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
    public class LeagueRepository : IGenericRepository<League>, IDisposable
    {
        private readonly LiveScoreReporterDbContext _context;

        public LeagueRepository(LiveScoreReporterDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public IEnumerable<League> GetAll()
        {
            return _context.Leagues.ToList();
        }

        public Task<List<League>> GetAllAsync()
        {
            return _context.Leagues.ToListAsync();
        }

        public League GetById(int id)
        {
            return _context.Leagues.Find(id);
        }

        public async Task<League> GetByIdAsync(int id)
        {
            return await _context.Leagues.FindAsync(id);
        }

        public bool Remove(int id)
        {
            var league = _context.Leagues.Find(id);

            if (league is null)
                return false;

            _context.Leagues.Remove(league);

            return true;
        }

        public void Add(in League sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }

        public void Update(in League sender)
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

        public League Select(Expression<Func<League, bool>> predicate)
        {
            return _context.Leagues
                .Where(predicate).FirstOrDefault()!;
        }

        public async Task<League> SelectAsync(Expression<Func<League, bool>> predicate)
        {
            return (await _context.Leagues
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