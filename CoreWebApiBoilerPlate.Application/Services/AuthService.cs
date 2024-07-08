using AutoMapper;
using CoreWebApiBoilerPlate.Application.DTO.Request;
using CoreWebApiBoilerPlate.Application.DTO.Response;
using CoreWebApiBoilerPlate.Application.Services.Interfaces;
using CoreWebApiBoilerPlate.Common;
using CoreWebApiBoilerPlate.Common.Exceptions;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Application.Services
{

    public class AuthService : IAuthService
    {
        
        private readonly IRepositoryWrapper repositoryWrapper;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthService(IRepositoryWrapper repositoryWrapper, IConfiguration configuration, IMapper mapper)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {

            var user = await repositoryWrapper.UserRepository.GetQueryable()
                .Include(o => o.Role)
                .SingleOrDefaultAsync(x => x.Username == loginRequestDTO.UserName);

            if (user is null)
                throw new AuthException("Invalid username.");

            var isValidUser = new PasswordHasher<LoginRequestDTO>().VerifyHashedPassword(loginRequestDTO, user.Password, loginRequestDTO.Password);
            
            if (isValidUser != PasswordVerificationResult.Success) 
            {
                throw new AuthException("Invalid password.");
            }

            var token = JWT.GenerateToken(new Dictionary<string, string> {
                { ClaimTypes.Role, user.Role.Description  },
                { "RoleId", user.Role.Id.ToString()  },
                {JwtClaimTypes.PreferredUserName, user.Name },
                { JwtClaimTypes.Id, user.Id.ToString() },
                { JwtClaimTypes.Email, user.EmailId}
            }, configuration["JWT:Key"]);

            var userResp = mapper.Map<UserResponseDTO>(user);
            return new LoginResponseDTO (  token, userResp );
        }
    }
}
