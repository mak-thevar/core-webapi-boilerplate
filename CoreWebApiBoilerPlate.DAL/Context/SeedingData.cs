using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Context
{
    public class SeedingData
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>
             {
                 new Role{ Id =1 , Description = "Admin",  CreatedOn = new DateTime(2023,1,1,1,1,1)},
                 new Role{ Id =2 , Description = "Normal", CreatedOn = new DateTime(2023,1,1,1,1,1)},
             };
        }

        public static List<User> GetUsers()
        {
            var password = new PasswordHasher<object>().HashPassword(null!, "12345678");
            return new List<User>
            {
                new User{ Id =1 , CreatedOn = new DateTime(2023,1,1,1,1,1), EmailId = "mak.thevar@outlook.com", IsActive = true, Name = "mak thevar", RoleId =1, Username = "mak-thevar", Password = password},
            };
        }
    }
}
