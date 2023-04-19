using CoreWebApiBoilerPlate.BusinessLogicLayer.DTO;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CoreWebApiBoilerPlate.Controllers
{
    public abstract class ApiBaseController : Controller
    {


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Claims.Where(x => x.Type == JwtClaimTypes.Id).SingleOrDefault()?.Value;
                var currentUserName = User.Claims.Where(x => x.Type == JwtClaimTypes.PreferredUserName).SingleOrDefault()?.Value;
                var currentRoleId = User.Claims.Where(x => x.Type == "RoleId").SingleOrDefault()?.Value;
                if (currentUserId is null || currentUserName is null)
                    throw new UnauthorizedAccessException();

                Constants.CurrentUserId = Convert.ToInt32(currentUserId);
                Constants.CurrentUserName = currentUserName;
                Constants.CurrentRoleId = Convert.ToInt32(currentRoleId);

            }
            if (!ModelState.IsValid)
            {

                var errors = new List<string>();

                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Any())
                    {
                        var key = item.Key;
                        foreach (var er in item.Value.Errors)
                        {
                            errors.Add($"{key} : {er.ErrorMessage}");
                        }
                    }
                }
                context.Result = CreateErrorResponse(HttpStatusCode.BadRequest, errors);
            }
        }

        protected IActionResult CreateSuccessResponse<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var respModel = new ApiResponseModel<T>
            {
                Result = value,
                Succeeded = true,
                StatusCode = statusCode
            };
            return StatusCode((int)statusCode, respModel);
        }

        protected IActionResult CreateErrorResponse(HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, List<string>? errors = default)
        {
            var respModel = new ApiResponseModel<string>
            {
                Result = string.Empty,
                Succeeded = false,
                StatusCode = httpStatusCode,
                Errors = errors
            };

            return StatusCode((int)httpStatusCode, respModel);
        }


        protected IActionResult DataNotFound(string customMessage = "")
        {
            var errMessage = string.IsNullOrEmpty(customMessage) ? "The resource that you are looking for is either null or empty." : customMessage;
            return CreateErrorResponse(HttpStatusCode.NotFound, new List<string> { errMessage });
        }
    }
}
