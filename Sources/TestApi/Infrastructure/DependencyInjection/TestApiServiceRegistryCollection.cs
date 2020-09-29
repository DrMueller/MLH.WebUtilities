using JetBrains.Annotations;
using Lamar;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DbContexts.Contexts;

namespace Mmu.Mlh.WebUtilities.TestApi.Infrastructure.DependencyInjection
{
    [UsedImplicitly]
    public class TestApiServiceRegistryCollection : ServiceRegistry
    {
        public TestApiServiceRegistryCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<TestApiServiceRegistryCollection>();
                    scanner.WithDefaultConventions();
                });

            For<DbContext>().Use<AppDbContext>().Transient();
        }
    }
}