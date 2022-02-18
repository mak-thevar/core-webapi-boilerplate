using CoreWebApiBoilerPlate.Entity;
using CoreWebApiBoilerPlate.Infrastructure.Data.Repository.Interfaces;
using CoreWebApiBoilerPlate.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Auth
{
    public class JWTAuth : IJWTAuth
    {
        private readonly IRepositoryWrapper repositoryWrapper;
        private readonly IConfiguration configuration;

        public JWTAuth(IRepositoryWrapper repositoryWrapper, IConfiguration configuration)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.configuration = configuration;
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            var result = await this.repositoryWrapper.UserRepository.FindByCondition(x => x.Username == userName && x.Password == password.SHA1());
            return result.SingleOrDefault();
        }

        public TokenResponseModel GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(configuration["JWTKey"]);
            var expiresAt = DateTime.Now.AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("Id", user.UserId.ToString())
                    }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
          
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenResponse = new TokenResponseModel
            {
                ExpiresAt = expiresAt,
                Token = tokenHandler.WriteToken(token)
            };
            return tokenResponse;
        }
    }
}
