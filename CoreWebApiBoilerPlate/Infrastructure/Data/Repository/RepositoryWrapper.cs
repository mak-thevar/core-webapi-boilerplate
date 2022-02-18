using CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces;
using CoreWebApiBoilerPlate.Infrastructure.Repository;
using HelpDeskBBQN.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Data.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DefaultContext defaultContext;

        public RepositoryWrapper(DefaultContext defaultContext)
        {
            this.defaultContext = defaultContext;
            this._userRepository = new UserRepository(defaultContext);
            this._postRepository = new PostRepository(defaultContext);
        }


        private IUserRepository _userRepository;
        private IPostRepository _postRepository;

        public IUserRepository UserRepository => _userRepository;

        public IPostRepository PostRepository => _postRepository;
        public async Task Save()
        {
            await this.defaultContext.SaveChangesAsync();
        }
    }
}
