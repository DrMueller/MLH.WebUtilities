using Mmu.Mlh.DomainExtensions.Areas.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories
{
    public interface IIndividualRepository : IRepository<Individual, long>
    {
    }
}