using Microsoft.EntityFrameworkCore;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DbContexts.Factories
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}