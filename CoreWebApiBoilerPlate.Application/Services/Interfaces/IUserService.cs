using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreWebApiBoilerPlate.Application.DTO.Request;
using CoreWebApiBoilerPlate.Application.DTO.Response;
using CoreWebApiBoilerPlate.Common.Exceptions;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> AddUserAsync(NewUserRequestDTO requestDTO);
        Task DeleteUser(int id);
        Task<IEnumerable<UserResponseDTO>> GetAllAsync();

        Task<UserResponseDTO?> GetByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, NewUserRequestDTO model);
    }

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public UserService(IMapper mapper, IRepositoryWrapper repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        public async Task<UserResponseDTO> AddUserAsync(NewUserRequestDTO requestDTO)
        {
            var user = _mapper.Map<User>(requestDTO);
            //Setting default Normal role for new user
            user.RoleId = 2;
            var userCreated = await _repository.UserRepository.AddAsync(user);
            await _repository.SaveAsync();
            var result = _mapper.Map<UserResponseDTO>(user);
            return result;
        }

        public async Task DeleteUser(int id)
        {
            User? dbUser = await FindExistingUser(id);
            await _repository.UserRepository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
        {
            var users = await _repository.UserRepository.GetQueryable().AsNoTracking()
                .Include(x => x.Role)
                .ProjectTo<UserResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return users;
        }

        public async Task<UserResponseDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.UserRepository.GetByIdAsync(id,nameof(User.Role));
            var result = _mapper.Map<UserResponseDTO?>(user);
            return result;
        }

        public async Task<bool> UpdateUserAsync(int id,NewUserRequestDTO model)
        {
            User? dbUser = await FindExistingUser(id);

            _mapper.Map(model, dbUser);

            var updatedRow = await _repository.SaveAsync();
            if (updatedRow <= 0)
                throw new AppException("Error while updating the user");
            return true;
        }

        private async Task<User?> FindExistingUser(int id)
        {
            var dbUser = await _repository.UserRepository.GetByIdAsync(id);
            if (dbUser is null)
                throw new AppException($"User with id - {id} not found");
            return dbUser;
        }
    }
}