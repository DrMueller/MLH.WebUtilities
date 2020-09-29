using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DbContexts.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.Repositories.Implementation
{
    public class IndividualRepository : IIndividualRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public IndividualRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IReadOnlyCollection<Individual>> LoadAllIndividualsAsync()
        {
            await using var dbContext = _dbContextFactory.Create();
            return await dbContext.Set<Individual>().ToListAsync();
        }
    }
}