using Lamar;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks
{
    internal class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IContainer _container;
        private readonly IDbContextFactory _dbContextFactory;

        public UnitOfWorkFactory(
            IContainer container,
            IDbContextFactory dbContextFactory)
        {
            _container = container;
            _dbContextFactory = dbContextFactory;
        }

        public IUnitOfWork Create()
        {
            var dbContext = _dbContextFactory.Create();
            var unitOfWork = _container.GetInstance<UnitOfWork>();
            unitOfWork.Initialize(dbContext);

            return unitOfWork;
        }
    }
}