using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository UserRepository { get; }
        IPostRepository PostRepository { get; }
        Task Save();
    }
}
