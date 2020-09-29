using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories
{
    public interface IOrganisationRepository : IRepository
    {
        Task<IReadOnlyCollection<Organisation>> LoadAllAsync();

        Task UpsertAsync(Organisation organisation);
    }
}