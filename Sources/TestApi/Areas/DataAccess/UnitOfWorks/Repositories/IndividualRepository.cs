using System.Threading.Tasks;
using JetBrains.Annotations;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.Repositories
{
    [UsedImplicitly]
    public class IndividualRepository : RepositoryBase<Individual>, IIndividualRepository
    {
        public async Task<Individual> LoadByIdAsync(long individualId)
        {
            return await DbSet.FindAsync(individualId);
        }
    }
}