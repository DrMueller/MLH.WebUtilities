using JetBrains.Annotations;
using Lamar;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.Querying;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Contexts;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Querying;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;

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

            For<UnitOfWork>().Use<UnitOfWork>().Transient();
            For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>().Transient();
            For<IQueryServiceFactory>().Use<QueryServiceFactory>().Singleton();
        }
    }
}