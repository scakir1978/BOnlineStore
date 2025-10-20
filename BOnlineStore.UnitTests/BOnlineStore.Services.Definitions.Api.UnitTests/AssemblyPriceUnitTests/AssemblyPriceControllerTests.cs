using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Controllers;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Shared.Dtos;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BOnlineStore.Services.Definitions.Api.UnitTests.AssemblyPriceUnitTests
{
    public class AssemblyPriceControllerTests
    {
        private readonly Mock<IAssemblyPriceService> _mockAssemblyPriceService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AssemblyPriceController _controller;

        public AssemblyPriceControllerTests()
        {
            _mockAssemblyPriceService = new Mock<IAssemblyPriceService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AssemblyPriceController(_mockAssemblyPriceService.Object, _mockMapper.Object);
        }

        [Fact]
        public void Load_ShouldReturnSuccessResult_WhenDataLoaded()
        {
            // Arrange
            var loadOptions = new DataSourceLoadOptionsBase();
            var assemblyPrices = new List<AssemblyPrice>
            {
                new AssemblyPrice(Guid.NewGuid(), "1", "region1", "glass1", 100.00m, 80.00m),
                new AssemblyPrice(Guid.NewGuid(), "2", "region2", "glass2", 150.00m, 120.00m)
            }.AsQueryable();

            var assemblyPriceDtos = new List<AssemblyPriceDto>
            {
                new AssemblyPriceDto("1", "region1", "glass1", 100.00m, 80.00m),
                new AssemblyPriceDto("2", "region2", "glass2", 150.00m, 120.00m)
            }.AsQueryable();

            _mockAssemblyPriceService.Setup(x => x.Load(null)).Returns(assemblyPrices);
            _mockMapper.Setup(x => x.ProjectTo<AssemblyPriceDto>(assemblyPrices, null, null)).Returns(assemblyPriceDtos);

            // Act
            var result = _controller.Load(loadOptions);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the LoadResult in a Response<LoadResult>
            objectResult.Value.Should().BeOfType<Response<LoadResult>>();
            
            var response = objectResult.Value.As<Response<LoadResult>>();
            response.Result.Should().BeOfType<LoadResult>();
            response.IsSucceed.Should().BeTrue();
            
            loadOptions.StringToLower.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccessResult_WhenAssemblyPricesExist()
        {
            // Arrange
            var assemblyPriceDtos = new List<AssemblyPriceDto>
            {
                new AssemblyPriceDto("1", "region1", "glass1", 100.00m, 80.00m),
                new AssemblyPriceDto("2", "region2", "glass2", 150.00m, 120.00m)
            };

            _mockAssemblyPriceService.Setup(x => x.GetAsync()).ReturnsAsync(assemblyPriceDtos);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();

            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();

            // The controller wraps the LoadResult in a Response<LoadResult>
            objectResult.Value.Should().BeOfType<Response<List<AssemblyPriceDto>>>();

            var response = objectResult.Value.As<Response<List<AssemblyPriceDto>>>();
            response.Result.Should().BeOfType<List<AssemblyPriceDto>>();
            response.IsSucceed.Should().BeTrue();

            _mockAssemblyPriceService.Verify(x => x.GetAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSuccessResult_WhenAssemblyPriceExists()
        {
            // Arrange
            var assemblyPriceId = "1";
            var assemblyPriceDto = new AssemblyPriceDto("1", "region1", "glass1", 100.00m, 80.00m);

            _mockAssemblyPriceService.Setup(x => x.GetByIdAsync(assemblyPriceId)).ReturnsAsync(assemblyPriceDto);

            // Act
            var result = await _controller.GetByIdAsync(assemblyPriceId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the result in a Response<AssemblyPriceDto>
            objectResult.Value.Should().BeOfType<Response<AssemblyPriceDto>>();
            
            var response = objectResult.Value.As<Response<AssemblyPriceDto>>();
            response.Result.Should().BeOfType<AssemblyPriceDto>();
            response.IsSucceed.Should().BeTrue();
            
            _mockAssemblyPriceService.Verify(x => x.GetByIdAsync(assemblyPriceId), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResult_WhenAssemblyPriceCreated()
        {
            // Arrange
            var createDto = new AssemblyPriceCreateDto("region1", "glass1", 100.00m, 80.00m);
            var createdDto = new AssemblyPriceDto("1", "region1", "glass1", 100.00m, 80.00m);

            _mockAssemblyPriceService.Setup(x => x.AddAsync(createDto)).ReturnsAsync(createdDto);

            // Act
            var result = await _controller.CreateAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the result in a Response<AssemblyPriceDto>
            objectResult.Value.Should().BeOfType<Response<AssemblyPriceDto>>();
            
            var response = objectResult.Value.As<Response<AssemblyPriceDto>>();
            response.Result.Should().BeOfType<AssemblyPriceDto>();
            response.IsSucceed.Should().BeTrue();
            
            _mockAssemblyPriceService.Verify(x => x.AddAsync(createDto), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccessResult_WhenAssemblyPriceUpdated()
        {
            // Arrange
            var assemblyPriceId = "1";
            var updateDto = new AssemblyPriceUpdateDto("region1", "glass1", 120.00m, 90.00m);
            var updatedDto = new AssemblyPriceDto("1", "region1", "glass1", 120.00m, 90.00m);

            _mockAssemblyPriceService.Setup(x => x.UpdateAsync(assemblyPriceId, updateDto)).ReturnsAsync(updatedDto);

            // Act
            var result = await _controller.UpdateAsync(assemblyPriceId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the result in a Response<AssemblyPriceDto>
            objectResult.Value.Should().BeOfType<Response<AssemblyPriceDto>>();
            
            var response = objectResult.Value.As<Response<AssemblyPriceDto>>();
            response.Result.Should().BeOfType<AssemblyPriceDto>();
            response.IsSucceed.Should().BeTrue();
            
            _mockAssemblyPriceService.Verify(x => x.UpdateAsync(assemblyPriceId, updateDto), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessResult_WhenAssemblyPriceDeleted()
        {
            // Arrange
            var assemblyPriceId = "1";
            var deletedDto = new AssemblyPriceDto("1", "region1", "glass1", 100.00m, 80.00m);

            _mockAssemblyPriceService.Setup(x => x.DeleteAsync(assemblyPriceId)).ReturnsAsync(deletedDto);

            // Act
            var result = await _controller.DeleteAsync(assemblyPriceId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the result in a Response<AssemblyPriceDto>
            objectResult.Value.Should().BeOfType<Response<AssemblyPriceDto>>();
            
            var response = objectResult.Value.As<Response<AssemblyPriceDto>>();
            response.Result.Should().BeOfType<AssemblyPriceDto>();
            response.IsSucceed.Should().BeTrue();
            
            _mockAssemblyPriceService.Verify(x => x.DeleteAsync(assemblyPriceId), Times.Once);
        }
    }
}
