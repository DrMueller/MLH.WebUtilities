using System.Diagnostics;
using AutoMapper;
using JetBrains.Annotations;
using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mmu.Mlh.WebUtilities.Areas.ExceptionHandling.Initialization;

namespace Mmu.Mlh.WebUtilities.TestApi
{
    [PublicAPI]
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Debug.WriteLine("Configure");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();
            app.UseGlobalExceptionHandler();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        public void ConfigureContainer(ServiceRegistry services)
        {
            Debug.WriteLine("Startup.ConfigureContainer");

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.Scan(
                scanner =>
                {
                    scanner.AssembliesFromApplicationBaseDirectory();
                    scanner.LookForRegistries();
                });

            RegisterMocks(services);

            services.AddControllers();
        }

        protected virtual void RegisterMocks(ServiceRegistry services)
        {
        }
    }
}