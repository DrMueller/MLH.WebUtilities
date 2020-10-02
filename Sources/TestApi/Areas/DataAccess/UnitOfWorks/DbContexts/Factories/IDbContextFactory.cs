using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Contexts;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Factories
{
    public interface IDbContextFactory
    {
        IDbContext Create();
    }
}