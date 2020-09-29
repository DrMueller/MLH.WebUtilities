using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceLocator _serviceLocator;
        private DbContext _dbContext;

        public UnitOfWork(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public TRepo CreateRepository<TRepo>() where TRepo : IRepository
        {
            var repository = _serviceLocator.GetService<TRepo>();

            if (!(repository is RepositoryBase repoBase))
            {
                throw new ArgumentException($"{nameof(TRepo)} does not implement RepositoryBase");
            }

            repoBase.Initialize(_dbContext);

            return repository;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        internal void Initialize(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}