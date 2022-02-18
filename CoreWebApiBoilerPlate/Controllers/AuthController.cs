using CoreWebApiBoilerPlate.Controllers.Base;
using CoreWebApiBoilerPlate.Infrastructure.Auth;
using CoreWebApiBoilerPlate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IJWTAuth jWTAuth;

        public AuthController(IJWTAuth jWTAuth)
        {
            this.jWTAuth = jWTAuth;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = await jWTAuth.Authenticate(login.UserName, login.Password);
            if (user == null)
                return await CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new Errors("Login Failed", "Invalid username or password."));
            var token = jWTAuth.GenerateToken(user);
            return await CreateSuccessResponse(token);
        }
    }
}
