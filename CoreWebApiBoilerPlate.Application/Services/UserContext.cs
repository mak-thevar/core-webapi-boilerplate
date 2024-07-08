using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using IdentityModel;

namespace CoreWebApiBoilerPlate.Application.Services
{

    public interface IUserContext
    {
        int CurrentUserId { get; }
        string CurrentUserName { get; }
        bool IsAdmin { get; }
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public int CurrentUserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtClaimTypes.Id);
                return int.TryParse(userIdClaim, out int userId) ? userId : 0;
            }
        }

        public string CurrentUserName
        {
            get => _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Anonymous";
        }

        public bool IsAdmin
        {
            get => _httpContextAccessor.HttpContext?.User?.IsInRole("Admin")??false;
        }

    }
}
