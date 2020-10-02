using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Contexts;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Contexts.Implementation;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Factories.Implementation
{
    public class DbContextFactory : IDbContextFactory
    {
        public IDbContext Create()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("TestDb")
                .Options;

            return new AppDbContext(options);
        }
    }
}