using CoreWebApiBoilerPlate.Entity;
using CoreWebApiBoilerPlate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Auth
{
    public interface IJWTAuth
    {
        public Task<User> Authenticate(string userName, string password);

        TokenResponseModel GenerateToken(User user);
    }
}
