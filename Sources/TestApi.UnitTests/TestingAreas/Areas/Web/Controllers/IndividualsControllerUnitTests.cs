using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Controllers;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Dtos;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlh.WebUtilities.TestApi.UnitTests.TestingAreas.Areas.Web.Controllers
{
    [TestFixture]
    public class IndividualsControllerUnitTests
    {
        private readonly IndividualsController _sut;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IIndividualRepository> _individualRepoMock;

        public IndividualsControllerUnitTests()
        {
            _mapperMock = new Mock<IMapper>();
            _individualRepoMock = new Mock<IIndividualRepository>();
            _sut = new IndividualsController(
                _individualRepoMock.Object,
                _mapperMock.Object);
        }

        [Test]
        public async Task FetchingAllIndividuals_ReturnsIndividuals()
        {
            // Arrange
            var individuals = new List<Individual>
            {
                new Individual("Tra", "Trst", DateTime.MinValue, 1),
                new Individual("Tra", "Trst", DateTime.MinValue, 1),
                new Individual("Tra", "Trst", DateTime.MinValue, 1)
            };

            _individualRepoMock.Setup(f => f.LoadAllIndividualsAsync())
                .Returns(Task.FromResult<IReadOnlyCollection<Individual>>(individuals));

            _mapperMock
                .Setup(f => f.Map<IndividualDto>(It.IsAny<object>()))
                .Returns(new IndividualDto());

            // Act
            var actualResult = await _sut.GetAllAsync();

            var okResult = actualResult.Result as OkObjectResult;
            var dtos = okResult.Value as List<IndividualDto>;

            // Assert
            Assert.AreEqual(individuals.Count, dtos.Count);
        }

        [Test]
        public async Task FetchingAllIndividuals_WithoutErrors_ReturnsOk()
        {
            // Arrange
            _individualRepoMock.Setup(f => f.LoadAllIndividualsAsync())
                .Returns(Task.FromResult<IReadOnlyCollection<Individual>>(new List<Individual>()));

            // Act
            var actualResult = await _sut.GetAllAsync();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actualResult.Result);
        }
    }
}