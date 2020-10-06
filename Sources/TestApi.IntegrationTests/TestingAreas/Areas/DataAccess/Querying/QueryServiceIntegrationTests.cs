using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lamar;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Querying;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingAreas.Areas.DataAccess.Querying
{
    [TestFixture]
    public class QueryServiceIntegrationTests
    {
        private IContainer _container;
        private IUnitOfWorkFactory _uowFactory;

        [SetUp]
        public void Align()
        {
            _container = new Container(
                cfg =>
                {
                    cfg.Scan(
                        scanner =>
                        {
                            scanner.AssembliesFromApplicationBaseDirectory();
                            scanner.LookForRegistries();
                        });
                });

            _uowFactory = _container.GetInstance<IUnitOfWorkFactory>();
        }

        [Test]
        public void Mocking_Queries_throws_exception()
        {
            var queryFactoryMock = new Mock<IQueryServiceFactory>();
            var queryServiceMock = new Mock<IQueryService>();

            queryFactoryMock.Setup(f => f.Create()).Returns(queryServiceMock.Object);

            // Arrange
            var individuals = new List<Individual>
            {
                new Individual
                {
                    Birthdate = new DateTime(2020, 03, 03),
                    FirstName = "Matthias",
                    LastName = "Müller"
                },
                new Individual
                {
                    Birthdate = new DateTime(2010, 05, 05),
                    FirstName = "Stefanie",
                    LastName = "Meyer"
                }
            };

            queryServiceMock.Setup(f => f.Query<Individual>()).Returns(individuals.AsQueryable());

            Assert.ThrowsAsync<InvalidOperationException>(() => queryServiceMock.Object.Query<Individual>().ToListAsync());
        }

        [Test]
        public async Task Querying_Individuals_queries_Individuals()
        {
            // Arrange
            var individuals = new List<Individual>
            {
                new Individual
                {
                    Birthdate = new DateTime(2020, 03, 03),
                    FirstName = "Matthias",
                    LastName = "Müller"
                },
                new Individual
                {
                    Birthdate = new DateTime(2010, 05, 05),
                    FirstName = "Stefanie",
                    LastName = "Meyer"
                }
            };

            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();

                foreach (var ind in individuals)
                {
                    await indRepo.UpsertAsync(ind);
                }

                await uow.SaveAsync();
            }

            var queryServiceFactory = _container.GetInstance<IQueryServiceFactory>();

            using (var sut = queryServiceFactory.Create())
            {
                // Act
                var actualIndividuals = await sut.Query<Individual>().ToListAsync();

                // Assert
                for (var i = 0; i < individuals.Count; i++)
                {
                    var actualIndividual = actualIndividuals.ElementAt(i);
                    var expectedIndividual = actualIndividuals.ElementAt(i);

                    Assert.AreEqual(
                        expectedIndividual.Id,
                        actualIndividual.Id);
                    Assert.AreEqual(
                        expectedIndividual.Birthdate,
                        actualIndividual.Birthdate);
                    Assert.AreEqual(
                        expectedIndividual.FirstName,
                        actualIndividual.FirstName);
                    Assert.AreEqual(
                        expectedIndividual.LastName,
                        actualIndividual.LastName);
                }
            }
        }

        [Test]
        public async Task Querying_WithJoins_work()
        {
            // Arrange
            var organisation = new Organisation
            {
                Addresses = new List<Address>
                {
                    new Address(),
                    new Address()
                }
            };

            using (var uow = _uowFactory.Create())
            {
                var orgRepo = uow.GetRepository<IOrganisationRepository>();
                await orgRepo.UpsertAsync(organisation);
                await uow.SaveAsync();
            }

            var queryServiceFactory = _container.GetInstance<IQueryServiceFactory>();

            using (var sut = queryServiceFactory.Create())
            {
                // Act
                var qry = from adr in sut.Query<Address>()
                    join org in sut.Query<Organisation>() on adr.OrganisationId equals org.Id
                    select org;

                var actualResultCount = await qry.CountAsync();

                // Assert
                Assert.AreEqual(organisation.Addresses.Count, actualResultCount);
            }
        }
    }
}