using AutoMapper;
using CoreWebApiBoilerPlate.Application.DTO;
using CoreWebApiBoilerPlate.Application.DTO.Request;
using CoreWebApiBoilerPlate.Application.Services.Interfaces;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Repository.Interfaces;
using CoreWebApiBoilerPlate.WebApi.Infrastructure;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CoreWebApiBoilerPlate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginModel)
        {
            var result = await authService.Login(loginModel);
            return CreateSuccessResponse(result);
        }
    }
}
