using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Dtos;
using Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingInfrastructure.AppFactories;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingAreas.Areas.Web.Controllers
{
    [TestFixture]
    public class IndividualsControllerIntegrationTests
    {
        private TestAppFactory _appFactory;

        [OneTimeSetUp]
        public void AlignAll()
        {
            _appFactory = new TestAppFactory();
        }

        [Test]
        public async Task GetAll_Should_Get_All_Individuals()
        {
            // Arrange
            var client = _appFactory.CreateClient();

            // Act
            var response = await client.GetAsync("api/individuals");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var json = await response.Content.ReadAsStringAsync();
            var individuals = JsonConvert.DeserializeObject<List<IndividualDto>>(json);

            Assert.AreNotEqual(0, individuals.Count);
        }
    }
}