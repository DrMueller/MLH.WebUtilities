using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModelRepositories
{
    public interface IDataModelRepository<TDataModel>
    where TDataModel: class
    {
        Task<IReadOnlyCollection<TDataModel>> LoadAllAsync();
    }
}