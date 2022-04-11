using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Models;
using Newtonsoft.Json;

namespace Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Middlewares
{
    internal class GlobalExceptionHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(
            ILogger logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Global exception handler")]
        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "Convention for middlewares")]
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);

                var response = httpContext.Response;
                response.ContentType = MediaTypeNames.Application.Json;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var serverException = ServerException.CreateFromException(exception);
                var serializedServerError = JsonConvert.SerializeObject(serverException);

                await response.WriteAsync(serializedServerError);
            }
        }
    }
}