using AutoMapper;
using CoreWebApiBoilerPlate.BusinessLogic.Repository;
using CoreWebApiBoilerPlate.Core;
using CoreWebApiBoilerPlate.DataLayer.Entities;
using CoreWebApiBoilerPlate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ApiBaseController
    {
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;

        public UsersController(IMapper mapper, IRepositoryWrapper wrapper)
        {
            this.mapper = mapper;
            repository = wrapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<List<UserResponseModel>>))]
        public async Task<IActionResult> Get()
        {
            var items = await repository.UserRepository.GetAllAsync();
            var usersList = repository.UserRepository.GetQueryable().AsNoTracking()
                            .Include(x => x.Role);
            var result = mapper.Map<List<UserResponseModel>>(usersList.ToList());
            return CreateSuccessResponse(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<UserResponseModel>))]
        [ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await repository.UserRepository.GetQueryable().AsNoTracking()
                            .Include(x => x.Role)
                            .SingleOrDefaultAsync(x => x.Id == id);
            if (user is null)
                return DataNotFound();
            var result = mapper.Map<UserResponseModel>(user);
            return CreateSuccessResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> AddUser([FromBody] NewUserRequestModel model)
        {
            var user = mapper.Map<User>(model);
            var userCreated = await repository.UserRepository.AddAsync(user);
            await repository.SaveAsync();
            return CreateSuccessResponse($"User created successfully with Id {userCreated.Id}");
        }



        [HttpPut("{id}")]
        [ProducesResponseType(400, Type = typeof(ApiResponseModel<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequestModel model)
        {
            var dbUser = await repository.UserRepository.GetByIdAsync(id);
            if (dbUser is null)
                return DataNotFound();
           
            dbUser.ModifiedOn = DateTime.UtcNow;
            dbUser.Name = model.Name;
            dbUser.EmailId = model.EmailId;
            //dbUser.RoleId = model.RoleId;
            dbUser.Username = model.Username;


            var rowsAffected = await repository.SaveAsync();
            if (rowsAffected > 0)
                return CreateSuccessResponse($"User: {id}, Updated successfully!");
            return CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new List<string> { $"User: {id}, Not Updated!" });

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var isDeleted = await repository.UserRepository.DeleteAsync(id);
            await repository.SaveAsync();
            return CreateSuccessResponse($"User with Id : {id} deleted successfully.");
        }

        [HttpPost("role")]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<RoleResponseModel>))]
        public async Task<IActionResult> AddRole([FromBody] RoleRequestModel model)
        {
            var role = mapper.Map<Role>(model);
            role.CreatedById = Constants.CurrentUserId;


            var created = await repository.UserRepository.CreateRoleAsync(role);
            await repository.SaveAsync();

            return CreateSuccessResponse($"Role created successfully with Id : {created.Id}");
        }

        [HttpGet("role")]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<List<RoleResponseModel>>))]
        public async Task<IActionResult> GetRole()
        {
            var roleList = await repository.UserRepository.GetRolesAsync();
            var result = mapper.Map<List<RoleResponseModel>>(roleList);
            return CreateSuccessResponse(result);
        }

        [HttpGet("role/{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<RoleResponseModel>))]
        [ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await repository.UserRepository.GetRolesByIdAsync(id);
            if (role is null)
                return DataNotFound();

            var result = mapper.Map<RoleResponseModel>(role);
            return CreateSuccessResponse(result);
        }

        [HttpPut("role/{id}")]
        [ProducesResponseType(400, Type = typeof(ApiResponseModel<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> UpdateRole(int id, RoleRequestModel roleRequest)
        {
            var dbRole = await repository.UserRepository.GetRolesByIdAsync(id);
            if (dbRole is null)
                return DataNotFound();
            dbRole.ModifiedById = Constants.CurrentUserId;
            dbRole.ModifiedOn = DateTime.UtcNow;
            dbRole.Description = roleRequest.Description;



            var rowsAffected = await repository.SaveAsync();
            if (rowsAffected > 0)
                return CreateSuccessResponse($"Role: {id}, Updated successfully!");
            return CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new List<string> { $"Role: {id}, Not Updated!" });
        }

        [HttpDelete("role/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var dbRole = await repository.UserRepository.DeleteRole(id);
            await repository.SaveAsync();
            if (dbRole)
                return CreateSuccessResponse($"Role: {id}, Deleted successfully!");
            return CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new List<string> { $"Role: {id}, Not Deleted!" });

        }
    }


}
