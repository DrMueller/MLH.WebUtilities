using Lamar;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.DependencyInjection
{
    public class TestApiIntegrationTestsServiceCollection : ServiceRegistry
    {
        public TestApiIntegrationTestsServiceCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<TestApiIntegrationTestsServiceCollection>();
                    scanner.WithDefaultConventions();
                });
        }
    }
}