using CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces;
using HelpDeskBBQN.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected DefaultContext RepositoryContext { get; set; }
        public RepositoryBase(DefaultContext defaultContext)
        {
            RepositoryContext = defaultContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            return (await RepositoryContext.Set<T>().AddAsync(entity)).Entity;
        }

        public void DeleteAsync(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(RepositoryContext.Set<T>().Where(expression).AsNoTracking());
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            return Task.FromResult(RepositoryContext.Set<T>().AsNoTracking());
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await RepositoryContext.FindAsync<T>(typeof(T), new { Id = id });
        }

        public T UpdateAsync(T entity)
        {
            return RepositoryContext.Set<T>().Update(entity).Entity;
        }
    }
}
