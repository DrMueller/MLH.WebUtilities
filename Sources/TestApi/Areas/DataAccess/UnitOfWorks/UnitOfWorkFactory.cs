using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks
{
    internal class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IServiceLocator _serviceLocator;

        public UnitOfWorkFactory(
            IServiceLocator serviceLocator,
            IDbContextFactory dbContextFactory)
        {
            _serviceLocator = serviceLocator;
            _dbContextFactory = dbContextFactory;
        }

        public IUnitOfWork Create()
        {
            var dbContext = _dbContextFactory.Create();
            var unitOfWork = _serviceLocator.GetService<UnitOfWork>();
            unitOfWork.Initialize(dbContext);

            return unitOfWork;
        }
    }
}