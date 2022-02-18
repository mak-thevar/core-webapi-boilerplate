using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.Middlewares
{
    public class HttpRequestInformation
    {
        public object Host { get; set; }
        public object Path { get; set; }
        public object QueryString { get; set; }
        public string CurrentUser { get; set; }
        public string RequestBody { get; set; }
    }

    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                ILogger loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .ForContext<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {

            await LogRequest(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            var userName = context.User.Identity.IsAuthenticated ? context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value : "NOT-LOGGEDIN";
            var httpReqInfo = new HttpRequestInformation
            {
                CurrentUser = userName,
                Host = context.Request.Host.Value,
                Path = context.Request.Path.Value,
                QueryString = context.Request.QueryString.Value,
                RequestBody = ReadStreamInChunks(requestStream)
            };
            _logger.Information("Http Request Information: {@httpReqInfo}", httpReqInfo);
            context.Request.Body.Position = 0;

            await _next(context);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }

    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
