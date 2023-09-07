using CoreWebApiBoilerPlate.BusinessLogicLayer.DTO;
using CoreWebApiBoilerPlate.BusinessLogicLayer.Exceptions;
using CoreWebApiBoilerPlate.DataLayer.Context;
using CoreWebApiBoilerPlate.DataLayer.Entities.Base;
using CoreWebApiBoilerPlate.DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoreWebApiBoilerPlate.DataLayer.Repository.Impl
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        private readonly DefaultDBContext dbContext;
        private readonly DbSet<T> dbSet;

        public RepositoryBase(DefaultDBContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            var result = await dbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity is not null)
            {

                IStatusEntity? statusEntity = entity as IStatusEntity;
                if (statusEntity != null)
                    statusEntity.IsActive = false;
                else
                    dbSet.Remove(entity);
                return await Task.FromResult(true);
            }
            throw new KeyNotFoundException($"Object with id {id} not found in the database.");
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(params string[] navigationsToInclude)
        {

            IQueryable<T> query = dbSet;
            if (navigationsToInclude is not null && navigationsToInclude.Length > 0)
            {
                foreach (var item in navigationsToInclude)
                {
                    query = query.Include(item);
                }
            }
                
            var items = (await query.ToListAsync()).Where(x => x as IStatusEntity != null ? ((IStatusEntity)x).IsActive : 1 == 1);
            return items.ToList();
        }


        public IQueryable<T> GetQueryable()
        {
            return dbSet;
        }

        public async Task<IReadOnlyList<T>> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return await dbSet.Where(condition).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, string? navigationsToInclude = null)
        {
            IQueryable<T> query = dbSet;
            if (navigationsToInclude is not null)
                navigationsToInclude.Split(',').ToList().ForEach(q => query = query.Include(q));
            var result = await query.SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<T> UpdateAsync(int id, T updatedEntity)
        {
            var dbEntity = await GetByIdAsync(id);
            if (dbEntity == null)
                throw new AppException($"Resource with Id : {id}, Not found!");
            dbContext.Entry(dbEntity).CurrentValues.SetValues(updatedEntity);
            var auditedProps = dbEntity as IAuditedEntity;
            if (auditedProps != null)
            {
                auditedProps.ModifiedOn = DateTime.UtcNow;
                auditedProps.CreatedById = Constants.CurrentUserId;
            }
            return dbEntity;
        }

        public async Task<T> UpdateAsync(int id, object updatedEntity)
        {
            var dbEntity = await GetByIdAsync(id);
            if (dbEntity == null)
                throw new AppException($"Resource with Id : {id}, Not found!");
            dbContext.Entry(dbEntity).CurrentValues.SetValues(updatedEntity);
            var auditedProps = dbEntity as IAuditedEntity;
            if (auditedProps != null)
            {
                auditedProps.ModifiedOn = DateTime.UtcNow;
                auditedProps.CreatedById = Constants.CurrentUserId;
            }
            return dbEntity;
        }
    }
}
