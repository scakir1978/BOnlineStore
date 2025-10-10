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

namespace BOnlineStore.UnitTests.DefinitionsService.AssemblerUnitTests
{
    public class AssemblerControllerTests
    {
        private readonly Mock<IAssemblerService> _mockAssemblerService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AssemblerController _controller;

        public AssemblerControllerTests()
        {
            _mockAssemblerService = new Mock<IAssemblerService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AssemblerController(_mockAssemblerService.Object, _mockMapper.Object);
        }

        [Fact]
        public void Load_ShouldReturnSuccessResult_WhenDataLoaded()
        {
            // Arrange
            var loadOptions = new DataSourceLoadOptionsBase();
            var assemblers = new List<Assembler>
            {
                new Assembler(Guid.NewGuid(), "1", "ASM001", "Assembler 1"),
                new Assembler(Guid.NewGuid(), "2", "ASM002", "Assembler 2")
            }.AsQueryable();

            var assemblerDtos = new List<AssemblerDto>
            {
                new AssemblerDto("1", "ASM001", "Assembler 1"),
                new AssemblerDto("2", "ASM002", "Assembler 2")
            }.AsQueryable();

            _mockAssemblerService.Setup(x => x.Load(null)).Returns(assemblers);
            _mockMapper.Setup(x => x.ProjectTo<AssemblerDto>(assemblers, null, null)).Returns(assemblerDtos);

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
        public async Task GetAllAsync_ShouldReturnSuccessResult_WhenAssemblersExist()
        {
            // Arrange
            var assemblerDtos = new List<AssemblerDto>
            {
                new AssemblerDto("1", "ASM001", "Assembler 1"),
                new AssemblerDto("2", "ASM002", "Assembler 2")
            };

            _mockAssemblerService.Setup(x => x.GetAsync()).ReturnsAsync(assemblerDtos);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();

            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();

            // The controller wraps the LoadResult in a Response<LoadResult>
            objectResult.Value.Should().BeOfType<Response<List<AssemblerDto>>>();

            var response = objectResult.Value.As<Response<List<AssemblerDto>>>();
            response.Result.Should().BeOfType<List<AssemblerDto>>();
            response.IsSucceed.Should().BeTrue();

            _mockAssemblerService.Verify(x => x.GetAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSuccessResult_WhenAssemblerExists()
        {
            // Arrange
            var assemblerId = "1";
            var assemblerDto = new AssemblerDto("1", "ASM001", "Assembler 1");

            _mockAssemblerService.Setup(x => x.GetByIdAsync(assemblerId)).ReturnsAsync(assemblerDto);

            // Act
            var result = await _controller.GetByIdAsync(assemblerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the result in a Response<AssemblerDto>
            objectResult.Value.Should().BeOfType<Response<AssemblerDto>>();
            
            var response = objectResult.Value.As<Response<AssemblerDto>>();
            response.Result.Should().BeOfType<AssemblerDto>();
            response.IsSucceed.Should().BeTrue();
            
            _mockAssemblerService.Verify(x => x.GetByIdAsync(assemblerId), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResult_WhenAssemblerCreated()
        {
            // Arrange
            var createDto = new AssemblerCreateDto("ASM001", "New Assembler");
            var createdDto = new AssemblerDto("1", "ASM001", "New Assembler");

            _mockAssemblerService.Setup(x => x.AddAsync(createDto)).ReturnsAsync(createdDto);

            // Act
            var result = await _controller.CreateAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the result in a Response<AssemblerDto>
            objectResult.Value.Should().BeOfType<Response<AssemblerDto>>();
            
            var response = objectResult.Value.As<Response<AssemblerDto>>();
            response.Result.Should().BeOfType<AssemblerDto>();
            response.IsSucceed.Should().BeTrue();
            
            _mockAssemblerService.Verify(x => x.AddAsync(createDto), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccessResult_WhenAssemblerUpdated()
        {
            // Arrange
            var assemblerId = "1";
            var updateDto = new AssemblerUpdateDto("ASM001", "Updated Assembler");
            var updatedDto = new AssemblerDto("1", "ASM001", "Updated Assembler");

            _mockAssemblerService.Setup(x => x.UpdateAsync(assemblerId, updateDto)).ReturnsAsync(updatedDto);

            // Act
            var result = await _controller.UpdateAsync(assemblerId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the result in a Response<AssemblerDto>
            objectResult.Value.Should().BeOfType<Response<AssemblerDto>>();
            
            var response = objectResult.Value.As<Response<AssemblerDto>>();
            response.Result.Should().BeOfType<AssemblerDto>();
            response.IsSucceed.Should().BeTrue();
            
            _mockAssemblerService.Verify(x => x.UpdateAsync(assemblerId, updateDto), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessResult_WhenAssemblerDeleted()
        {
            // Arrange
            var assemblerId = "1";
            var deletedDto = new AssemblerDto("1", "ASM001", "Deleted Assembler");

            _mockAssemblerService.Setup(x => x.DeleteAsync(assemblerId)).ReturnsAsync(deletedDto);

            // Act
            var result = await _controller.DeleteAsync(assemblerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            
            var objectResult = result.As<ObjectResult>();
            objectResult.Value.Should().NotBeNull();
            
            // The controller wraps the result in a Response<AssemblerDto>
            objectResult.Value.Should().BeOfType<Response<AssemblerDto>>();
            
            var response = objectResult.Value.As<Response<AssemblerDto>>();
            response.Result.Should().BeOfType<AssemblerDto>();
            response.IsSucceed.Should().BeTrue();
            
            _mockAssemblerService.Verify(x => x.DeleteAsync(assemblerId), Times.Once);
        }
    }
}