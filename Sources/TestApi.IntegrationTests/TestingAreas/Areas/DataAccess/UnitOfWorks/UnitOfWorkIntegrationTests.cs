using System.Threading;
using System.Threading.Tasks;
using Lamar;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Contexts;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.DbContexts.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingAreas.Areas.DataAccess.UnitOfWorks
{
    [TestFixture]
    public class UnitOfWorkIntegrationTests
    {
        private IContainer _container;

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
        }

        [Test]
        public void Requesting_Same_Repository_Multiple_Times_Returns_same_Instance()
        {
            var uowFactory = _container.GetInstance<IUnitOfWorkFactory>();

            using var uow = uowFactory.Create();
            var indRepo1 = uow.GetRepository<IIndividualRepository>();
            var indRepo2 = uow.GetRepository<IIndividualRepository>();

            Assert.AreSame(indRepo1, indRepo2);
        }

        [Test]
        public async Task UpdatingData_WithoutSaving_DoesNotSaveData()
        {
            // Arrange & Act
            var serviceLocator = _container.GetInstance<IContainer>();
            var dbContextFactoryMock = new Mock<IDbContextFactory>();
            var dbContextMock = new Mock<IDbContext>();

            dbContextMock.Setup(f => f.Set<Individual>()).Returns(Mock.Of<DbSet<Individual>>());

            dbContextMock.Setup(f => f.Set<Organisation>()).Returns(Mock.Of<DbSet<Organisation>>());

            dbContextFactoryMock
                .Setup(f => f.Create())
                .Returns(dbContextMock.Object);

            var uowFactory = new UnitOfWorkFactory(
                serviceLocator,
                dbContextFactoryMock.Object);

            var ind = new Individual();
            var org = new Organisation();

            using var uow = uowFactory.Create();
            var indRepo = uow.GetRepository<IIndividualRepository>();
            var orgRepo = uow.GetRepository<IOrganisationRepository>();

            await indRepo.UpsertAsync(ind);
            await orgRepo.UpsertAsync(org);

            // Assert
            dbContextMock.Verify(f => f.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task UpdatingData_WithSaving_SavesData()
        {
            // Arrange & Act
            var serviceLocator = _container.GetInstance<IContainer>();
            var dbContextFactoryMock = new Mock<IDbContextFactory>();
            var dbContextMock = new Mock<IDbContext>();

            dbContextMock.Setup(f => f.Set<Individual>()).Returns(Mock.Of<DbSet<Individual>>());

            dbContextMock.Setup(f => f.Set<Organisation>()).Returns(Mock.Of<DbSet<Organisation>>());

            dbContextFactoryMock
                .Setup(f => f.Create())
                .Returns(dbContextMock.Object);

            var uowFactory = new UnitOfWorkFactory(
                serviceLocator,
                dbContextFactoryMock.Object);

            var ind = new Individual();
            var org = new Organisation();

            using var uow = uowFactory.Create();
            var indRepo = uow.GetRepository<IIndividualRepository>();
            var orgRepo = uow.GetRepository<IOrganisationRepository>();

            await indRepo.UpsertAsync(ind);
            await orgRepo.UpsertAsync(org);
            await uow.SaveAsync();

            // Assert
            dbContextMock.Verify(f => f.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}