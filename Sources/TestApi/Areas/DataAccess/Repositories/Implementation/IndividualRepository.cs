using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DbContexts.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.Repositories.Implementation
{
    [UsedImplicitly]
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

        public async Task<Individual> SaveAsync(Individual individual)
        {
            await using var dbContext = _dbContextFactory.Create();

            if (individual.Id.Equals(default))
            {
                await dbContext.AddAsync(individual);
            }
            else
            {
                dbContext.Update(individual);
            }

            await dbContext.SaveChangesAsync();
            return await dbContext.Set<Individual>().SingleAsync(f => f.Id == individual.Id);
        }
    }
}