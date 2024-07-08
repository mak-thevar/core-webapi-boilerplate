using CoreWebApiBoilerPlate.Application.DTO;
using CoreWebApiBoilerPlate.Common.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace CoreWebApiBoilerPlate.WebApi.Infrastructure.Middlewares
{
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var statusCode = error switch
                {
                    AppException => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };
                response.StatusCode = statusCode;

                var controllerName = context.Request.RouteValues.ContainsKey("controller")
                    ? context.Request.RouteValues["controller"]
                    : string.Empty;

                _logger.LogError(error, "An error occurred in controller {Controller}", controllerName);

                var result = new ApiResponseModel<string>
                {
                    Succeeded = false,
                    StatusCode = (HttpStatusCode)statusCode,
                    Errors = BuildErrorList(error)
                };

                await response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy
                        {
                            ProcessDictionaryKeys = true
                        }
                    }
                }));
            }
        }

        private static List<string> BuildErrorList(Exception error)
        {
            var errorList = new List<string> { error.Message };

            var innerEx = error.InnerException;
            while (innerEx != null)
            {
                errorList.Add(innerEx.Message);
                innerEx = innerEx.InnerException;
            }

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                errorList.Add(error.StackTrace ?? string.Empty);
            }

            return errorList;
        }
    }
}
