using CoreWebApiBoilerPlate.Infrastructure;
using CoreWebApiBoilerPlate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Controllers.Base
{
    public abstract class ApiControllerBase : Controller
    {
      
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Any logic that you want to execute before the action method
            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Claims.Where(x => x.Type == "Id").SingleOrDefault().Value;
                if (string.IsNullOrEmpty(currentUserId))
                    throw new UnauthorizedAccessException();
                Constants.CurrentUserId = Convert.ToInt32(currentUserId);
            }

        }
        protected async Task<IActionResult> CreateSuccessResponse<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var respModel = new DefaultResponseModel<T>();
            respModel.Data = value;
            respModel.Success = true;
            respModel.Message = "Success";
            return StatusCode((int)statusCode, respModel);
        }

        protected async Task<IActionResult> CreateErrorResponse(HttpStatusCode httpStatusCode, Errors errorMessage = null)
        {
            var respModel = new DefaultResponseModel<string>();
            respModel.Success = false;
            respModel.Message = "Failed";
            var errList = new List<Errors>();
            if (errorMessage == null)
            {
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Any())
                    {
                        var key = item.Key;
                        foreach (var er in item.Value.Errors)
                        {
                            var err = new Errors(key, er.ErrorMessage);
                            errList.Add(err);
                        }
                    }
                }
            }
            else
            {
                errList.Add(errorMessage);
            }
            respModel.Errors = errList;
            return StatusCode((int)httpStatusCode, respModel);

        }

        protected async Task<IActionResult> CreateErrorResponse(HttpStatusCode httpStatusCode, List<Errors> errorMessages)
        {
            var respModel = new DefaultResponseModel<string>();
            respModel.Success = false;
            respModel.Message = "Failed";
            respModel.Errors = errorMessages;
            return StatusCode((int)httpStatusCode, respModel);

        }

        protected async Task<IActionResult> DataNotFound(string customMessage = "")
        {
            var errMessage = string.IsNullOrEmpty(customMessage) ? "The resource that you are looking for is either null or empty." : customMessage;
            return await CreateErrorResponse(HttpStatusCode.NotFound, new Errors("Data Not Found", errMessage));
        }
    }
}
