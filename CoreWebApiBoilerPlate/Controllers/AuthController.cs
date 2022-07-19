using AutoMapper;
using CoreWebApiBoilerPlate.BusinessLogic.Repository;
using CoreWebApiBoilerPlate.Core;
using CoreWebApiBoilerPlate.Models;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        private readonly IRepositoryWrapper repository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthController(IRepositoryWrapper repository, IConfiguration configuration, IMapper mapper)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginModel)
        {
            var hashedPass = EasyEncryption.MD5.ComputeMD5Hash(loginModel.Password);
            var user = await this.repository.UserRepository.GetQueryable().Include(o=>o.Role).SingleOrDefaultAsync(x => x.Username == loginModel.UserName && x.Password == hashedPass);
            
            if(user is null)
                return Unauthorized("Invalid Username or Password!");


            var token = JWT.GenerateToken(new Dictionary<string, string> { 
                { ClaimTypes.Role, user.Role.Description  },
                { "RoleId", user.Role.Id.ToString()  },
                {JwtClaimTypes.PreferredUserName, user.Name },
                { JwtClaimTypes.Id, user.Id.ToString() },
                { JwtClaimTypes.Email, user.EmailId}
            },configuration["JWT:Key"]);

            var userResp = mapper.Map<UserResponseModel>(user);
            return CreateSuccessResponse(new { AuthToken = token, UserData =  userResp});
        }
    }

    public class LoginRequestModel
    {
        [Required]
        [StringLength(100, MinimumLength =3)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = null!;
    }
}
