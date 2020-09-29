using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories
{
    public interface IIndividualRepository : IRepository
    {
        public Task<IReadOnlyCollection<Individual>> LoadAllAsync();

        public Task<Individual> LoadByIdAsync(long individualId);

        public Task UpsertAsync(Individual individual);
    }
}