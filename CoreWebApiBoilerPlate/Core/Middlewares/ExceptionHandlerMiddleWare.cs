using CoreWebApiBoilerPlate.Core.Exceptions;
using CoreWebApiBoilerPlate.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace CoreWebApiBoilerPlate.Core.Middlewares
{
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleWare>();
        }
    }

    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;

        public ExceptionHandlerMiddleWare(RequestDelegate next, Serilog.ILogger loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.ForContext<ExceptionHandlerMiddleWare>();
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
                _logger.Fatal(error, $"An error occured on Controller {(context.Request.RouteValues.ContainsKey("controller") ? context.Request.RouteValues["controller"] : "")}");
                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = new ApiResponseModel<string>();
                var errorList = new List<string>();
                if (error != null)
                {
                    var errorType = error.GetType().Name;
                    var errorMessage = error.Message;

                    //var errors = new Errors(errorType, errorMessage);
                    errorList.Add(errorMessage);
                    result.Succeeded = false;
                    var innerEx = error.InnerException;
                    while (innerEx != null)
                    {
                        errorMessage = innerEx.Message;
                        errorList.Add(errorMessage);
                        //result.Errors.Add(new Errors(innerEx.GetType().Name, errorMessage));
                        innerEx = innerEx.InnerException;
                    }

                }
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    //result.Errors.Add( new Errors("StackTrace", error?.StackTrace));
                    if (error?.StackTrace is not null)
                        errorList.Add(error.StackTrace);
                }
                result.Errors = errorList;
                result.StatusCode = (HttpStatusCode)response.StatusCode;
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
    }
}
