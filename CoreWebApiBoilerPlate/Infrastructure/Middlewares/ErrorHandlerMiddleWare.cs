using CoreWebApiBoilerPlate.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Middlewares
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.ForContext<ErrorHandlerMiddleware>();
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

                var result = new DefaultResponseModel<string>();
                if (error != null)
                {
                    var errorType = error.GetType().Name;
                    var errorMessage = error.Message;
                    var errors = new Errors(errorType, errorMessage);
                    result.Errors.Add(errors);
                    result.Message = "An Error Occurred";
                    result.Success = false;
                    var innerEx = error.InnerException;
                    while (innerEx != null)
                    {
                        errorMessage = innerEx.Message;
                        result.Errors.Add(new Errors(innerEx.GetType().Name, errorMessage));
                        innerEx = innerEx.InnerException;
                    }

                }
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    result.Errors.Add(new Errors("StackTrace", error?.StackTrace));
                }

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
