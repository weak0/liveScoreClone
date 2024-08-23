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
    public class EventRepository : IGenericRepository<Event>, IDisposable
    {
        private readonly LiveScoreReporterDbContext _context;

        public EventRepository(LiveScoreReporterDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.ToList();
        }

        public Task<List<Event>> GetAllAsync()
        {
            return _context.Events.ToListAsync();
        }

        public Event GetById(int id)
        {
            return _context.Events.Find(id);
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public bool Remove(int id)
        {
            var matchEvent = _context.Events.Find(id);

            if (matchEvent is null)
                return false;

            _context.Events.Remove(matchEvent);

            return true;
        }

        public void Add(in Event sender)
        {
            _context.Add(sender).State = EntityState.Added;
        }

        public void Update(in Event sender)
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

        public Event Select(Expression<Func<Event, bool>> predicate)
        {
            return _context.Events
                .Where(predicate).FirstOrDefault()!;
        }

        public async Task<Event> SelectAsync(Expression<Func<Event, bool>> predicate)
        {
            return (await _context.Events
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