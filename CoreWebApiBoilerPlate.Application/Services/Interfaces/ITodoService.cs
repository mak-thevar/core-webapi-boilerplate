using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreWebApiBoilerPlate.Application.DTO;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoResponseModel>> GetAllAsync();
        Task<TodoResponseModel?> GetByIdForCurrentUserAsync(int id);
    }

    public class TodoService : ITodoService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public TodoService(IRepositoryWrapper repositoryWrapper, IMapper mapper, IUserContext userContext)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<IEnumerable<TodoResponseModel>> GetAllAsync()
        {
            var query = _repositoryWrapper.TodoRepository
                            .GetQueryable()
                            .Include(x => x.Comments).AsQueryable();

            query = _userContext.IsAdmin ?
                             query : query.Where(x => x.CreatedById == _userContext.CurrentUserId);

            var result = query.ProjectTo<TodoResponseModel>(_mapper.ConfigurationProvider).AsEnumerable();

            return result;
        }

        public async Task<TodoResponseModel?> GetByIdForCurrentUserAsync(int id)
        {
            var query = _repositoryWrapper.TodoRepository
                            .GetQueryable()
                            .Where(x => x.Id == id)
                            .Include(x => x.Comments).AsQueryable();

            query = _userContext.IsAdmin ?
                             query : query.Where(x => x.CreatedById == _userContext.CurrentUserId);

            var result = await query.ProjectTo<TodoResponseModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            return result;
        }
    }
}
