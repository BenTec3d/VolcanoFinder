using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VolcanoFinder.API.Controllers;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Models.Entities;
using VolcanoFinder.API.Models.Metadata;
using VolcanoFinder.API.Profiles;
using VolcanoFinder.API.Services;

namespace VolcanoFinder.Tests
{
    public class RegionsControllerTests
    {

        private readonly Mock<IVolcanoFinderRepository> _volcanoFinderRepositoryMock;
        private readonly RegionsController _regionsController;
        private static readonly Volcano _testVolcano = new("TestVolcano1", "TestPicture1")
        {
            Id = 1,
            CountryAlpha2 = "TV",
            LastEruption = new DateTime(2000,1,2),
            Active = true
        };
        private static readonly Region _testRegion = new("TestRegion1")
        {
            Id = 1,
            Volcanoes = new List<Volcano>()
                    {
                        _testVolcano
                    }
        };
        private readonly List<Region> _regionsToReturn = new()
            {
                _testRegion,
                new Region("TestRegion2")
                {
                    Id = 2,
                    Volcanoes = new List<Volcano>()
                    {
                        _testVolcano,
                        new Volcano("TestVolcano2", "TestVolcano2")
                        {
                            Id = 2
                        }
                    }
                },
                new Region("TestRegion3")
                {
                    Id = 3,
                    Volcanoes = new List<Volcano>()
                    {
                        _testVolcano,
                        new Volcano("TestVolcano2", "TestVolcano2")
                        {
                            Id = 2
                        },
                        new Volcano("TestVolcano3", "TestVolcano3")
                        {
                            Id = 3
                        }
                    }
                }
            };

        public RegionsControllerTests()
        {
            _volcanoFinderRepositoryMock = new Mock<IVolcanoFinderRepository>();
            _volcanoFinderRepositoryMock.Setup(x => x.GetRegionsAsync(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((_regionsToReturn, new PaginationMetadata(2, 10, 1)));
            _volcanoFinderRepositoryMock.Setup(x => x.GetRegionAsync(It.Is<int>(x => x > 0), It.IsAny<bool>()))
                .ReturnsAsync((_testRegion));

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RegionProfile>();
                cfg.AddProfile<VolcanoProfile>();
            });
            var mapper = new Mapper(mapperConfiguration);

            _regionsController = new RegionsController(_volcanoFinderRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task GetRegions_GetRegionsWithVolcanoes_MustReturnOkObjectResultWithCorrectCollectionOfRegionsWithCorrectVolcanoes()
        {
            // Act
            var result = await _regionsController.GetRegions(true) as ObjectResult;

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var dtos = Assert.IsAssignableFrom<IEnumerable<RegionDto>>(okObjectResult.Value);
            var testRegion = dtos.First();
            Assert.Equal(_testRegion.Id, testRegion.Id);
            Assert.Equal(_testRegion.Name, testRegion.Name);
            Assert.Equal(_testRegion.Volcanoes.Count, testRegion.Volcanoes.Count);
            var testVolcano = dtos.First().Volcanoes.First();
            Assert.Equal(_testVolcano.Id, testVolcano.Id);
            Assert.Equal(_testVolcano.Name, testVolcano.Name);
            Assert.Equal(_testVolcano.Picture, testVolcano.Picture);
            Assert.Equal(_testVolcano.CountryAlpha2, testVolcano.CountryAlpha2);
            Assert.Equal(_testVolcano.LastEruption, testVolcano.LastEruption);
            Assert.Equal(_testVolcano.Active, testVolcano.Active);
        }

        [Fact]
        public async Task GetRegions_GetRegionsWithVolcanoes_MustReturnOkObjectResultWithCorrectAmountOfRegionWithoutVolcanoes()
        {
            // Act
            var result = await _regionsController.GetRegions() as ObjectResult;

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var dtos = Assert.IsAssignableFrom<IEnumerable<RegionWithoutVolcanoesDto>>(okObjectResult.Value);
            var testRegion = dtos.First();
            Assert.Equal(_testRegion.Id, testRegion.Id);
            Assert.Equal(_testRegion.Name, testRegion.Name);
        }

        [Fact]
        public async Task GetRegions_PageSizeLargerThanMaxPageSize_MustSetPageSizeToMaxPageSize()
        {
            // Act
            var result = await _regionsController.GetRegions(false, 1, 30) as ObjectResult;

            // Assert
            _volcanoFinderRepositoryMock.Verify(x => x.GetRegionsAsync(false, 1, 20));
        }

        [Fact]
        public async Task GetRegions_PageSizeSmallerThanZero_MustSetPageSizeToMaxPageSize()
        {
            // Act
            var result = await _regionsController.GetRegions(false, 1, -10) as ObjectResult;

            // Assert
            _volcanoFinderRepositoryMock.Verify(x => x.GetRegionsAsync(false, 1, 20));
        }

        [Fact]
        public async Task GetRegion_GetRegionWithVolcanoes_MustReturnOkObjectResultWithCorrectRegionWithCorrectVolcanoes()
        {
            // Act
            var result = await _regionsController.GetRegion(1, true) as ObjectResult;

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsAssignableFrom<RegionDto>(okObjectResult.Value);
            var testRegion = dto;
            Assert.Equal(_testRegion.Id, testRegion.Id);
            Assert.Equal(_testRegion.Name, testRegion.Name);
            Assert.Equal(_testRegion.Volcanoes.Count, testRegion.Volcanoes.Count);
            var testVolcano = dto.Volcanoes.First();
            Assert.Equal(_testVolcano.Id, testVolcano.Id);
            Assert.Equal(_testVolcano.Name, testVolcano.Name);
            Assert.Equal(_testVolcano.Picture, testVolcano.Picture);
            Assert.Equal(_testVolcano.CountryAlpha2, testVolcano.CountryAlpha2);
            Assert.Equal(_testVolcano.LastEruption, testVolcano.LastEruption);
            Assert.Equal(_testVolcano.Active, testVolcano.Active);
        }

        [Fact]
        public async Task GetRegion_GetRegionWithoutVolcanoes_MustReturnOkObjectResultWithCorrectRegionWithoutVolcanoes()
        {
            // Act
            var result = await _regionsController.GetRegion(1, true) as ObjectResult;

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsAssignableFrom<RegionDto>(okObjectResult.Value);
            var testRegion = dto;
            Assert.Equal(_testRegion.Id, testRegion.Id);
            Assert.Equal(_testRegion.Name, testRegion.Name);
            Assert.Equal(_testRegion.Volcanoes.Count, testRegion.Volcanoes.Count);
        }

        [Fact]
        public async Task GetRegion_GetRegionWithInvalidId_MustReturnNotFoundStatusCodeResult()
        {
            // Act
            var result = await _regionsController.GetRegion(-1);

            // Assert
            var statusCodeResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

    }
}
