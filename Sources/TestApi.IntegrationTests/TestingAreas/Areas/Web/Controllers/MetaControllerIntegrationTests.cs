using System.Threading.Tasks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.AppFactories;
using NUnit.Framework;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingAreas.Areas.Web.Controllers
{
    [TestFixture]
    public class MetaControllerIntegrationTests
    {
        private TestAppFactory _appFactory;

        [OneTimeSetUp]
        public void AlignAll()
        {
            _appFactory = new TestAppFactory();
        }

        [Test]
        public async Task GettingRepoName_RepoBeingMocked_GetsMockedRepoName()
        {
            // Arrange
            var client = _appFactory.CreateClient();

            // Act
            var response = await client.GetAsync("api/meta/mocked");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var orgRepoName = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("IOrganisationRepositoryProxy", orgRepoName);
        }

        [Test]
        public async Task GettingRepoName_RepoNotBeingMocked_GetsActualRepoName()
        {
            // Arrange
            var client = _appFactory.CreateClient();

            // Act
            var response = await client.GetAsync("api/meta/notmocked");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var individualRepoName = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(nameof(IndividualRepository), individualRepoName);
        }
    }
}