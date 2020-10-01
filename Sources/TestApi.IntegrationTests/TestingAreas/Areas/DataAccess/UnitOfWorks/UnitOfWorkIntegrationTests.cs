using System.Threading.Tasks;
using Lamar;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;
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
            var containerConfig = ContainerConfiguration.CreateFromAssembly(typeof(UnitOfWorkIntegrationTests).Assembly);
            _container = ServiceProvisioningInitializer.CreateContainer(containerConfig);
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
            var uowFactory = _container.GetInstance<IUnitOfWorkFactory>();

            using (var uow = uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();
                var orgRepo = uow.GetRepository<IOrganisationRepository>();

                await indRepo.UpsertAsync(new Individual());
                await orgRepo.UpsertAsync(new Organisation());
            }

            // Assert
            using (var uow = uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();
                var orgRepo = uow.GetRepository<IOrganisationRepository>();

                var actualIndividuals = await indRepo.LoadAllAsync();
                var actualOrganisations = await orgRepo.LoadAllAsync();

                CollectionAssert.IsEmpty(actualIndividuals);
                CollectionAssert.IsEmpty(actualOrganisations);
            }
        }

        [Test]
        public async Task UpdatingData_WithSaving_SavesData()
        {
            // Arrange & Act
            var uowFactory = _container.GetInstance<IUnitOfWorkFactory>();

            using (var uow = uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();
                var orgRepo = uow.GetRepository<IOrganisationRepository>();

                await indRepo.UpsertAsync(new Individual());
                await orgRepo.UpsertAsync(new Organisation());

                await uow.SaveAsync();
            }

            // Assert
            using (var uow = uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();
                var orgRepo = uow.GetRepository<IOrganisationRepository>();

                var actualIndividuals = await indRepo.LoadAllAsync();
                var actualOrganisations = await orgRepo.LoadAllAsync();

                CollectionAssert.IsNotEmpty(actualIndividuals);
                CollectionAssert.IsNotEmpty(actualOrganisations);
            }
        }
    }
}