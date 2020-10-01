using System;
using System.Linq;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Querying
{
    public interface IQueryService : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
    }
}