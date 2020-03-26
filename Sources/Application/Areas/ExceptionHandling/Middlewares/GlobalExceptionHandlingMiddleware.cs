using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Models;
using Newtonsoft.Json;

namespace Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Middlewares
{
    internal class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Global exception handler")]
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
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
