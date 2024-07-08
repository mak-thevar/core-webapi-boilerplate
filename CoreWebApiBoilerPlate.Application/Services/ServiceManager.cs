using AutoMapper;
using CoreWebApiBoilerPlate.Application.Services.Interfaces;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<ITodoService> _todoService;

        public ServiceManager(IRepositoryWrapper repositoryWrapper, IMapper mapper, IConfiguration configuration, IUserContext userContext)
        {
            _authService = CreateLazyService<IAuthService>(new AuthService(repositoryWrapper, configuration, mapper));
            _userService = CreateLazyService<IUserService>(new UserService(mapper, repositoryWrapper));
            _todoService = CreateLazyService<ITodoService>(new TodoService(repositoryWrapper, mapper, userContext));
        }

        public IAuthService AuthService => _authService.Value;
        public IUserService UserService => _userService.Value;
        public ITodoService TodoService => _todoService.Value;

        private static Lazy<T> CreateLazyService<T>(T service)
        {
            return new Lazy<T>(service);
        }
    }
}
