using CoreWebApiBoilerPlate.Entity;
using CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces;
using CoreWebApiBoilerPlate.Infrastructure.Repository;
using HelpDeskBBQN.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Data.Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        private readonly DefaultContext defaultContext;

        public PostRepository(DefaultContext defaultContext) : base(defaultContext)
        {
            this.defaultContext = defaultContext;
        }
    }
}
