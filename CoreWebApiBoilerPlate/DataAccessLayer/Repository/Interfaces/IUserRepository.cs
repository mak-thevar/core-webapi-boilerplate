using CoreWebApiBoilerPlate.DataLayer.Entities;

namespace CoreWebApiBoilerPlate.DataLayer.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<Role> CreateRoleAsync(Role role);

        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role?> GetRolesByIdAsync(int id);
        Task<bool> DeleteRole(int id);
    }
}
