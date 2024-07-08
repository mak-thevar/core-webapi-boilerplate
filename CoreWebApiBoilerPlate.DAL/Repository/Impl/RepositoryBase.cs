using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Context;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities.Base;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Impl
{

    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected readonly DefaultDBContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(DefaultDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                if (entity is IStatusEntity statusEntity)
                {
                    statusEntity.IsActive = false;
                }
                else
                {
                    _dbSet.Remove(entity);
                }
                return await Task.FromResult(true);
            }
            throw new KeyNotFoundException($"Object with id {id} not found in the database.");
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(params string[] navigationsToInclude)
        {
            IQueryable<T> query = _dbSet;

            if (navigationsToInclude != null && navigationsToInclude.Length > 0)
            {
                foreach (var navigation in navigationsToInclude)
                {
                    query = query.Include(navigation);
                }
            }

            var items = await query.ToListAsync();
            return items.Where(x => x is not IStatusEntity || ((IStatusEntity)x).IsActive).ToList();
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return _dbSet;
        }

        public virtual async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return await _dbSet.Where(condition).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id, string? navigationsToInclude = null)
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(navigationsToInclude))
            {
                var includes = navigationsToInclude.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T> UpdateAsync(int id, T updatedEntity)
        {
            var dbEntity = await GetByIdAsync(id);
            if (dbEntity == null)
                throw new KeyNotFoundException($"Resource with Id : {id}, Not found!");

            _dbContext.Entry(dbEntity).CurrentValues.SetValues(updatedEntity);

            return dbEntity;
        }

        public virtual async Task<T> UpdateAsync(int id, object updatedEntity)
        {
            var dbEntity = await GetByIdAsync(id);
            if (dbEntity == null)
                throw new KeyNotFoundException($"Resource with Id : {id}, Not found!");

            _dbContext.Entry(dbEntity).CurrentValues.SetValues(updatedEntity);

            return dbEntity;
        }
    }

}
