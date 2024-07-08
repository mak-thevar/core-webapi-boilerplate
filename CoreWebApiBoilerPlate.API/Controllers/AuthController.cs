using CoreWebApiBoilerPlate.Application.DTO.Request;
using CoreWebApiBoilerPlate.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApiBoilerPlate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IServiceManager serviceManager)
        {
            this._authService = serviceManager.AuthService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginModel)
        {
            var result = await _authService.Login(loginModel);
            return CreateSuccessResponse(result);
        }
    }
}
