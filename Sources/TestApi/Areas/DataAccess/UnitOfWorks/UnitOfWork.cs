using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Lamar;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Contexts;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ConcurrentDictionary<Type, IRepository> _repos;
        private readonly IContainer _serviceLocator;
        private IDbContext _dbContext;

        public UnitOfWork(IContainer serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _repos = new ConcurrentDictionary<Type, IRepository>();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public TRepo GetRepository<TRepo>() where TRepo : IRepository
        {
            var repoType = typeof(TRepo);

            if (_repos.ContainsKey(repoType))
            {
                return (TRepo)_repos[repoType];
            }

            var repository = _serviceLocator.GetInstance<TRepo>();

            if (!(repository is RepositoryBase repoBase))
            {
                throw new ArgumentException($"{nameof(TRepo)} does not implement RepositoryBase");
            }

            repoBase.Initialize(_dbContext);

            _repos.AddOrUpdate(repoType, repository, (type, repo) => repo);

            return repository;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        internal void Initialize(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}