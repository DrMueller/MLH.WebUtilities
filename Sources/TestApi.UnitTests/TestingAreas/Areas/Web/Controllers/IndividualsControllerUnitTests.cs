using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;
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
            var uowFactoryMock = new Mock<IUnitOfWorkFactory>();
            var uowMock = new Mock<IUnitOfWork>();
            _individualRepoMock = new Mock<IIndividualRepository>();

            uowFactoryMock.Setup(f => f.Create()).Returns(uowMock.Object);
            uowMock.Setup(f => f.GetRepository<IIndividualRepository>()).Returns(_individualRepoMock.Object);

            _sut = new IndividualsController(
                uowFactoryMock.Object,
                _mapperMock.Object);
        }

        [Test]
        public async Task FetchingAllIndividuals_ReturnsIndividuals()
        {
            // Arrange
            var individuals = new List<Individual>
            {
                new Individual
                {
                    Birthdate = DateTime.MinValue,
                    FirstName = "Test Firstname",
                    LastName = "Test Lastname"
                },
                new Individual
                {
                    Birthdate = DateTime.MinValue,
                    FirstName = "Test Firstname",
                    LastName = "Test Lastname"
                },
                new Individual
                {
                    Birthdate = DateTime.MinValue,
                    FirstName = "Test Firstname",
                    LastName = "Test Lastname"
                }
            };

            _individualRepoMock.Setup(f => f.LoadAllAsync())
                .Returns(Task.FromResult<IReadOnlyCollection<Individual>>(individuals));

            _mapperMock
                .Setup(f => f.Map<IndividualDto>(It.IsAny<object>()))
                .Returns(new IndividualDto());

            // Act
            var actualResult = await _sut.GetAllAsync();

            // Assert
            if (actualResult.Result is OkObjectResult okResult)
            {
                var dtos = okResult.Value as List<IndividualDto>;

                Assert.AreEqual(individuals.Count, dtos?.Count ?? 0);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task FetchingAllIndividuals_WithoutErrors_ReturnsOk()
        {
            // Arrange
            _individualRepoMock.Setup(f => f.LoadAllAsync())
                .Returns(Task.FromResult<IReadOnlyCollection<Individual>>(new List<Individual>()));

            // Act
            var actualResult = await _sut.GetAllAsync();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actualResult.Result);
        }
    }
}