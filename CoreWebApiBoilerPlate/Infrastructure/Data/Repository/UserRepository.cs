using CoreWebApiBoilerPlate.Entity;
using CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces;
using HelpDeskBBQN.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly DefaultContext defaultContext;

        public UserRepository(DefaultContext defaultContext) : base(defaultContext)
        {
            this.defaultContext = defaultContext;
        }
    }
}
