using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Mmu.Mlh.WebUtilities.TestApi.IntegrationTests.TestingAreas.Areas.Web.Controllers
{
    [TestFixture]
    public class IndividualsControllerIntegrationTests
    {
        private WebApplicationFactory<Startup> _appFactory;

        [OneTimeSetUp]
        public void AlignAll()
        {
            _appFactory = new WebApplicationFactory<Startup>();
        }

        [Test]
        public async Task Tra()
        {
            // Arrange
            var client = _appFactory.CreateClient();

            // Act
            var individuals = await client.GetAsync("api/individuals");

            // Assert
        }
    }

}
