using Microsoft.EntityFrameworkCore;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Factories
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}