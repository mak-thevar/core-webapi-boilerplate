using CoreWebApiBoilerPlate.Application.DTO;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CoreWebApiBoilerPlate.WebApi.Controllers
{
    public abstract class ApiBaseController : Controller
    {
        // Property for current user info
        protected CurrentUserInfo CurrentUser { get; private set; } = new CurrentUserInfo();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Retrieve user information if authenticated
            if (User.Identity.IsAuthenticated)
            {
                CurrentUser = ExtractCurrentUserInfo();

                if (CurrentUser.UserId == null || CurrentUser.UserName == null)
                {
                    context.Result = CreateErrorResponse(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized access." });
                    return;
                }
            }

            // Validate ModelState and return a formatted error response
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                context.Result = CreateErrorResponse(HttpStatusCode.BadRequest, errors);
            }
        }

        // Method to extract user information from JWT claims
        private CurrentUserInfo ExtractCurrentUserInfo()
        {
            var currentUserId = User.Claims.SingleOrDefault(x => x.Type == JwtClaimTypes.Id)?.Value;
            var currentUserName = User.Claims.SingleOrDefault(x => x.Type == JwtClaimTypes.PreferredUserName)?.Value;
            var currentRoleId = User.Claims.SingleOrDefault(x => x.Type == "RoleId")?.Value;

            return new CurrentUserInfo
            {
                UserId = currentUserId != null ? Convert.ToInt32(currentUserId) : (int?)null,
                UserName = currentUserName,
                RoleId = currentRoleId != null ? Convert.ToInt32(currentRoleId) : (int?)null
            };
        }

        // Generic success response
        protected IActionResult CreateSuccessResponse<T>(T? value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var responseModel = new ApiResponseModel<T>(true, value, new List<string>(), statusCode);
            if (value is null)
                statusCode = HttpStatusCode.NoContent;
            return StatusCode((int)statusCode, responseModel);
        }

        protected IActionResult CreateSuccessResponse()
        {
            return CreateSuccessResponse<object>(null);
        }

        // Generic error response
        protected IActionResult CreateErrorResponse(HttpStatusCode statusCode = HttpStatusCode.BadRequest, IEnumerable<string>? errors = null)
        {
            var responseModel = new ApiResponseModel<string>(false, string.Empty, errors ?? new List<string>(), statusCode);
            return StatusCode((int)statusCode, responseModel);
        }

        // Specific "Not Found" response
        protected IActionResult DataNotFound(string customMessage = "")
        {
            var errorMessage = string.IsNullOrEmpty(customMessage)
                ? "The resource that you are looking for is either null or empty."
                : customMessage;

            return CreateErrorResponse(HttpStatusCode.NotFound, new List<string> { errorMessage });
        }

        // Class to encapsulate current user information
        protected class CurrentUserInfo
        {
            public int? UserId { get; set; }
            public string? UserName { get; set; }
            public int? RoleId { get; set; }
        }
    }
}
