using Lamar;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.Adapters;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModelRepositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DbContexts;

namespace Mmu.Mlh.WebUtilities.TestApi.Infrastructure.DependencyInjection
{
    public class TestApiServiceRegistryCollection : ServiceRegistry
    {
        public TestApiServiceRegistryCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<TestApiServiceRegistryCollection>();
                    scanner.AddAllTypesOf(typeof(IDataModelAdapter<,>));
                    scanner.AddAllTypesOf(typeof(IDataModelRepository<>));
                    scanner.WithDefaultConventions();
                });

            For<DbContext>().Use<AppDbContext>().Transient();
        }
    }
}