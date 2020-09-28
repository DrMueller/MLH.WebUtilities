using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModelRepositories.Implementation
{
    public class DataModelRepository<TDataModel> : IDataModelRepository<TDataModel>
        where TDataModel : class
    {
        private readonly DbContext _dbContext;

        public DataModelRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<TDataModel>> LoadAllAsync()
        {
            return await _dbContext.Set<TDataModel>().ToListAsync();
        }
    }
}