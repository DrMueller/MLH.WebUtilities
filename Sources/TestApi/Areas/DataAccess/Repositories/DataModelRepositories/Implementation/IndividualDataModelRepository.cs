using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DataAccess.EntityFramework.Areas.DataModelRepositories.Implementation;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.Repositories.DataModelRepositories.Implementation
{
    public class IndividualDataModelRepository : EntityFrameworkDataModelRepository<IndividualDataModel, long>, IIndividualDataModelRepository
    {
        public IndividualDataModelRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}