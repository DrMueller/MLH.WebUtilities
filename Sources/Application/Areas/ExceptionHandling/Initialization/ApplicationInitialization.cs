﻿using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Middlewares;

namespace Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Initialization
{
    [PublicAPI]
    public static class ApplicationInitialization
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            Guard.ObjectNotNull(() => app);
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            return app;
        }
    }
}