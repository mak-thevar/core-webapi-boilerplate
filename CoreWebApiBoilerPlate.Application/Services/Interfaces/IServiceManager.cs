using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Application.Services.Interfaces
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IAuthService AuthService { get; }
        ITodoService TodoService { get; }
    }
}
