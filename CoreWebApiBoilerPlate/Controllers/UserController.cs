using AutoMapper;
using CoreWebApiBoilerPlate.Application.DTO;
using CoreWebApiBoilerPlate.Application.Services.Interfaces;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiBoilerPlate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApiBaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<List<UserResponseDTO>>))]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await this.userService.GetAll();
            return CreateSuccessResponse(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<UserResponseDTO>))]
        [ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await this.userService.GetById(id);
            if (result is null)
                return DataNotFound();
            return CreateSuccessResponse(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> AddUser([FromBody] UserRequestDTO model)
        {
            var result = await this.userService.AddUserAsync(model);
            return CreateSuccessResponse($"User created successfully with Id {result.Id}", System.Net.HttpStatusCode.Created);
        }



        //[HttpPut("{id}")]
        //[ProducesResponseType(400, Type = typeof(ApiResponseModel<string>))]
        //[ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        //[ProducesResponseType(200, Type = typeof(ApiResponseModel<string>))]
        //public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequestDTO model)
        //{
        //    var dbUser = await repository.UserRepository.GetByIdAsync(id);
        //    if (dbUser is null)
        //        return DataNotFound();

        //    dbUser.ModifiedOn = DateTime.UtcNow;
        //    dbUser.Name = model.Name;
        //    dbUser.EmailId = model.EmailId;
        //    //dbUser.RoleId = model.RoleId;
        //    dbUser.Username = model.Username;


        //    var rowsAffected = await repository.SaveAsync();
        //    if (rowsAffected > 0)
        //        return CreateSuccessResponse($"User: {id}, Updated successfully!");
        //    return CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new List<string> { $"User: {id}, Not Updated!" });

        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(200, Type = typeof(ApiResponseModel<string>))]
        //[ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var isDeleted = await repository.UserRepository.DeleteAsync(id);
        //    await repository.SaveAsync();
        //    return CreateSuccessResponse($"User with Id : {id} deleted successfully.");
        //}

        //[HttpPost("role")]
        //[ProducesResponseType(200, Type = typeof(ApiResponseModel<RoleResponseModel>))]
        //public async Task<IActionResult> AddRole([FromBody] RoleRequestModel model)
        //{
        //    var role = mapper.Map<Role>(model);

        //    var created = await repository.UserRepository.CreateRoleAsync(role);
        //    await repository.SaveAsync();

        //    return CreateSuccessResponse($"Role created successfully with Id : {created.Id}");
        //}

        //[HttpGet("role")]
        //[ProducesResponseType(200, Type = typeof(ApiResponseModel<List<RoleResponseModel>>))]
        //public async Task<IActionResult> GetRole()
        //{
        //    var roleList = await repository.UserRepository.GetRolesAsync();
        //    var result = mapper.Map<List<RoleResponseModel>>(roleList);
        //    return CreateSuccessResponse(result);
        //}

        //[HttpGet("role/{id}")]
        //[ProducesResponseType(200, Type = typeof(ApiResponseModel<RoleResponseModel>))]
        //[ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        //public async Task<IActionResult> GetRoleById(int id)
        //{
        //    var role = await repository.UserRepository.GetRolesByIdAsync(id);
        //    if (role is null)
        //        return DataNotFound();

        //    var result = mapper.Map<RoleResponseModel>(role);
        //    return CreateSuccessResponse(result);
        //}

        //[HttpPut("role/{id}")]
        //[ProducesResponseType(400, Type = typeof(ApiResponseModel<string>))]
        //[ProducesResponseType(404, Type = typeof(ApiResponseModel<string>))]
        //[ProducesResponseType(200, Type = typeof(ApiResponseModel<string>))]
        //public async Task<IActionResult> UpdateRole(int id, RoleRequestModel roleRequest)
        //{
        //    var dbRole = await repository.UserRepository.GetRolesByIdAsync(id);
        //    if (dbRole is null)
        //        return DataNotFound();

        //    dbRole.Description = roleRequest.Description;



        //    var rowsAffected = await repository.SaveAsync();
        //    if (rowsAffected > 0)
        //        return CreateSuccessResponse($"Role: {id}, Updated successfully!");
        //    return CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new List<string> { $"Role: {id}, Not Updated!" });
        //}

        //[HttpDelete("role/{id}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    var dbRole = await repository.UserRepository.DeleteRole(id);
        //    await repository.SaveAsync();
        //    if (dbRole)
        //        return CreateSuccessResponse($"Role: {id}, Deleted successfully!");
        //    return CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new List<string> { $"Role: {id}, Not Deleted!" });

        //}
    }


}
