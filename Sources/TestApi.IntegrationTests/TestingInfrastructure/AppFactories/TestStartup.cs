using JetBrains.Annotations;
using Lamar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;
using Moq;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.AppFactories
{
    [PublicAPI]
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void RegisterMocks(ServiceRegistry services)
        {
            services.For<IOrganisationRepository>().Use(Mock.Of<IOrganisationRepository>());
            services.For<ILogger>().Use(Mock.Of<ILogger>());
        }
    }
}