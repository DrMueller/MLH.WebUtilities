using Lamar;
using Mmu.Mlh.DataAccess.Areas.DatabaseAccess;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Services;
using Mmu.Mlh.DomainExtensions.Areas.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Infrastructure.DependencyInjection
{
    public class TestApiServiceRegistryCollection : ServiceRegistry
    {
        public TestApiServiceRegistryCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AddAllTypesOf(typeof(IRepository<,>));
                    scanner.AddAllTypesOf(typeof(IDataModelAdapter<,,>));
                    scanner.AddAllTypesOf(typeof(IDataModelRepository<,>));
                    scanner.WithDefaultConventions();
                });
        }
    }
}