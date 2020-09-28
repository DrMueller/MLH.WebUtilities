using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.Adapters;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModelRepositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModels;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.Repositories.Implementation
{
    public class IndividualRepository : IIndividualRepository
    {
        private readonly IDataModelAdapter<Individual, IndividualDataModel> _adapter;
        private readonly IDataModelRepository<IndividualDataModel> _repo;

        public IndividualRepository(IDataModelRepository<IndividualDataModel> repo, IDataModelAdapter<Individual, IndividualDataModel> adapter)
        {
            _repo = repo;
            _adapter = adapter;
        }

        public async Task<IReadOnlyCollection<Individual>> LoadAllIndividualsAsync()
        {
            var dataModels = await _repo.LoadAllAsync();
            var result = dataModels.Select(dm => _adapter.Adapt(dm)).ToList();

            return result;
        }
    }
}