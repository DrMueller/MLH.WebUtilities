using JetBrains.Annotations;
using Lamar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.Data.Services;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.AppFactories
{
    // Achtung: Die Klasse ist nutzlos, da in ConfigureContainer Lamar noch nicht bereit ist. Aber nice to have als Beispiel
    [PublicAPI]
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}