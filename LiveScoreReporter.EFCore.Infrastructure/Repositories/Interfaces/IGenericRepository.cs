using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        DbContext Context { get; }
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        bool Remove(int id);
        void Add(in T sender);
        void Update(in T sender);
        int Save();
        Task<int> SaveAsync();
        public T? Select(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> include = null);
        public Task<T?> SelectAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> include = null);
    }
}
