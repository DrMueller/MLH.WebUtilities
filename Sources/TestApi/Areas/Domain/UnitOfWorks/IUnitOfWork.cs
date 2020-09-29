using System;
using System.Threading.Tasks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        TRepo CreateRepository<TRepo>()
            where TRepo : IRepository;

        Task SaveAsync();
    }
}