using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DbContexts.Contexts;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DbContexts.Factories.Implementation
{
    public class DbContextFactory : IDbContextFactory
    {
        public DbContext Create()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("TestDb")
                .Options;

            return new AppDbContext(options);
        }
    }
}