using System.Threading.Tasks;
using Lamar;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;
using NUnit.Framework;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingAreas.Areas.Domain.UnitOfWorks
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
        public async Task UpdatingData_WithoutSaving_DoesNotSaveData()
        {
            // Arrange & Act
            var uowFactory = _container.GetInstance<IUnitOfWorkFactory>();

            using (var uow = uowFactory.Create())
            {
                var indRepo = uow.CreateRepository<IIndividualRepository>();
                var orgRepo = uow.CreateRepository<IOrganisationRepository>();

                await indRepo.UpsertAsync(new Individual());
                await orgRepo.UpsertAsync(new Organisation());
            }

            // Assert
            using (var uow = uowFactory.Create())
            {
                var indRepo = uow.CreateRepository<IIndividualRepository>();
                var orgRepo = uow.CreateRepository<IOrganisationRepository>();

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
                var indRepo = uow.CreateRepository<IIndividualRepository>();
                var orgRepo = uow.CreateRepository<IOrganisationRepository>();

                await indRepo.UpsertAsync(new Individual());
                await orgRepo.UpsertAsync(new Organisation());

                await uow.SaveAsync();
            }

            // Assert
            using (var uow = uowFactory.Create())
            {
                var indRepo = uow.CreateRepository<IIndividualRepository>();
                var orgRepo = uow.CreateRepository<IOrganisationRepository>();

                var actualIndividuals = await indRepo.LoadAllAsync();
                var actualOrganisations = await orgRepo.LoadAllAsync();

                CollectionAssert.IsNotEmpty(actualIndividuals);
                CollectionAssert.IsNotEmpty(actualOrganisations);
            }
        }
    }
}