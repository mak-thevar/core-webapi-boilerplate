using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities.Base;
using System.Linq.Expressions;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T?> GetByIdAsync(int id, string? navigationsToInclude = null);
        Task<IEnumerable<T>> GetAllAsync(params string[] navigationsToInclude);
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> condition);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(int id, T updatedEntity);
        Task<T> UpdateAsync(int id, object updatedEntity);
        Task<bool> DeleteAsync(int id);
        IQueryable<T> GetQueryable();
    }
}
