using JetBrains.Annotations;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Querying;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.Querying
{
    [UsedImplicitly]
    internal class QueryServiceFactory : IQueryServiceFactory
    {
        private readonly IDbContextFactory _dbContextFactory;

        public QueryServiceFactory(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IQueryService Create()
        {
            var dbContext = _dbContextFactory.Create();
            return new QueryService(dbContext);
        }
    }
}