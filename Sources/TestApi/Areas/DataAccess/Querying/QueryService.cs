using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Contexts;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Querying;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.Querying
{
    internal class QueryService : IQueryService
    {
        private readonly IDbContext _dbContext;

        public QueryService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            var set = _dbContext.Set<T>().AsNoTracking();

            return set;
        }
    }
}