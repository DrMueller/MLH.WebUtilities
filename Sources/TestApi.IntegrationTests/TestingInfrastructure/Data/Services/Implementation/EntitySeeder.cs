using System;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.Data.Services.Implementation
{
    public class EntitySeeder : IEntitySeeder
    {
        public const int CreatedIndividuals = 6;
        private readonly IDbContextFactory _dbContextFactory;

        private bool _isSeeded;
        private readonly object _lock = new object();

        public EntitySeeder(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void AssureTestDataIsSeeded()
        {
            if (_isSeeded)
            {
                return;
            }

            lock (_lock)
            {
                if (_isSeeded)
                {
                    return;
                }

                using var dbContext = _dbContextFactory.Create();
                for (var i = 0; i < CreatedIndividuals; i++)
                {
                    dbContext.Add(
                        new Individual
                        {
                            Birthdate = new DateTime(2000 + i, 1 + i, 3 + i),
                            FirstName = "Matthias " + i,
                            LastName = "Müller " + i
                        });
                }

                dbContext.SaveChanges();
                _isSeeded = true;
            }
        }
    }
}