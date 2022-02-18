using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        T UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
