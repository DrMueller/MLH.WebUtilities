using Mmu.Mlh.DataAccess.Areas.DatabaseAccess;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Services;
using Mmu.Mlh.DataAccess.Areas.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.Repositories.Implementation
{
    public class IndividualRepository : RepositoryBase<Individual, IndividualDataModel, long>, IIndividualRepository
    {
        public IndividualRepository(IDataModelRepository<IndividualDataModel, long> dataModelRepository, IDataModelAdapter<IndividualDataModel, Individual, long> dataModelAdapter) : base(dataModelRepository, dataModelAdapter)
        {
        }
    }
}