using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.Data.Services;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.AppFactories
{
    public class TestAppFactory : WebApplicationFactory<TestStartup>
    {
        protected override void ConfigureClient(HttpClient client)
        {
            var entitySeeder = base.Services.GetService<IEntitySeeder>();
            if (entitySeeder == null)
            {
                throw new Exception("tra");
            }

            entitySeeder.AssureTestDataIsSeeded();

            base.ConfigureClient(client);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Debug.WriteLine("WebApplicationFactory.ConfigureWebHost");

            builder.ConfigureTestContainer<object>(
                container =>
                {
                    // Never called :shrugging:
                    Debug.WriteLine("builder.ConfigureTestContainer");
                    Debugger.Break();
                });

            builder.ConfigureServices(
                services =>
                {
                    Debug.WriteLine("builder.ConfigureServices");
                });

            builder.ConfigureTestServices(
                services =>
                {
                    Debug.WriteLine("builder.ConfigureTestServices");
                });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            return base.CreateHost(builder);
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .UseLamar()
                .ConfigureWebHostDefaults(
                    x =>
                    {
                        x.UseStartup<TestStartup>().UseTestServer();
                    });
            return builder;
        }
    }
}