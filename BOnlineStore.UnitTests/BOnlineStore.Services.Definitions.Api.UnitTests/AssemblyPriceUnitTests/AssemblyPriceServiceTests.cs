using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using BOnlineStore.Services.Definitions.Api.Services;
using BOnlineStore.Localization;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BOnlineStore.Services.Definitions.Api.UnitTests.AssemblyPriceUnitTests
{
    public class AssemblyPriceServiceTests
    {
        private readonly Mock<IAssemblyPriceRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IStringLocalizer<Language>> _mockStringLocalizer;
        private readonly Mock<IValidator<AssemblyPrice>> _mockValidator;
        private readonly AssemblyPriceService _service;

        public AssemblyPriceServiceTests()
        {
            _mockRepository = new Mock<IAssemblyPriceRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockStringLocalizer = new Mock<IStringLocalizer<Language>>();
            _mockValidator = new Mock<IValidator<AssemblyPrice>>();
            
            _service = new AssemblyPriceService(
                _mockRepository.Object,
                _mockMapper.Object,
                _mockStringLocalizer.Object,
                _mockValidator.Object);
        }

        [Fact]
        public void Load_ShouldReturnQueryable_WhenCalled()
        {
            // Arrange
            var assemblyPrices = new List<AssemblyPrice>
            {
                new AssemblyPrice(Guid.NewGuid(), "1", "region1", "glass1", 100.00m, 80.00m),
                new AssemblyPrice(Guid.NewGuid(), "2", "region2", "glass2", 150.00m, 120.00m)
            }.AsQueryable();

            _mockRepository.Setup(x => x.Load(It.IsAny<Expression<Func<AssemblyPrice, bool>>>())).Returns(assemblyPrices);

            // Act
            var result = _service.Load();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IQueryable<AssemblyPrice>>();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAssemblyPriceDtos_WhenAssemblyPricesExist()
        {
            // Arrange
            var assemblyPrices = new List<AssemblyPrice>
            {
                new AssemblyPrice(Guid.NewGuid(), "1", "region1", "glass1", 100.00m, 80.00m),
                new AssemblyPrice(Guid.NewGuid(), "2", "region2", "glass2", 150.00m, 120.00m)
            };

            var assemblyPriceDtos = new List<AssemblyPriceDto>
            {
                new AssemblyPriceDto("1", "region1", "glass1", 100.00m, 80.00m),
                new AssemblyPriceDto("2", "region2", "glass2", 150.00m, 120.00m)
            };

            _mockRepository.Setup(x => x.GetAsync()).ReturnsAsync(assemblyPrices);
            _mockMapper.Setup(x => x.Map<List<AssemblyPriceDto>>(assemblyPrices)).Returns(assemblyPriceDtos);

            // Act
            var result = await _service.GetAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(assemblyPriceDtos);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAssemblyPriceDto_WhenAssemblyPriceExists()
        {
            // Arrange
            var assemblyPriceId = "1";
            var assemblyPrice = new AssemblyPrice(Guid.NewGuid(), assemblyPriceId, "region1", "glass1", 100.00m, 80.00m);
            var assemblyPriceDto = new AssemblyPriceDto(assemblyPriceId, "region1", "glass1", 100.00m, 80.00m);

            _mockRepository.Setup(x => x.GetByIdAsync(assemblyPriceId)).ReturnsAsync(assemblyPrice);
            _mockMapper.Setup(x => x.Map<AssemblyPriceDto>(assemblyPrice)).Returns(assemblyPriceDto);

            // Act
            var result = await _service.GetByIdAsync(assemblyPriceId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(assemblyPriceDto);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAssemblyPriceDto_WhenAssemblyPriceCreated()
        {
            // Arrange
            var createDto = new AssemblyPriceCreateDto("region1", "glass1", 100.00m, 80.00m);
            var assemblyPrice = new AssemblyPrice(Guid.NewGuid(), "1", "region1", "glass1", 100.00m, 80.00m);
            var assemblyPriceDto = new AssemblyPriceDto("1", "region1", "glass1", 100.00m, 80.00m);

            // Setup validator to return a valid result for both possible ValidateAsync calls
            var validationResult = new ValidationResult();
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<AssemblyPrice>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AssemblyPrice>>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);

            _mockMapper.Setup(x => x.Map<AssemblyPrice>(createDto)).Returns(assemblyPrice);
            _mockRepository.Setup(x => x.AddAsync(assemblyPrice)).ReturnsAsync(assemblyPrice);
            _mockMapper.Setup(x => x.Map<AssemblyPriceDto>(assemblyPrice)).Returns(assemblyPriceDto);

            // Act
            var result = await _service.AddAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(assemblyPriceDto);
            _mockRepository.Verify(x => x.AddAsync(assemblyPrice), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnAssemblyPriceDto_WhenAssemblyPriceUpdated()
        {
            // Arrange
            var assemblyPriceId = "1";
            var updateDto = new AssemblyPriceUpdateDto("region1", "glass1", 120.00m, 90.00m);
            var existingAssemblyPrice = new AssemblyPrice(Guid.NewGuid(), assemblyPriceId, "region1", "glass1", 100.00m, 80.00m);
            var updatedAssemblyPrice = new AssemblyPrice(Guid.NewGuid(), assemblyPriceId, "region1", "glass1", 120.00m, 90.00m);
            var assemblyPriceDto = new AssemblyPriceDto(assemblyPriceId, "region1", "glass1", 120.00m, 90.00m);

            // Setup validator to return a valid result for both possible ValidateAsync calls
            var validationResult = new ValidationResult();
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<AssemblyPrice>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AssemblyPrice>>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);

            _mockRepository.Setup(x => x.GetByIdAsync(assemblyPriceId)).ReturnsAsync(existingAssemblyPrice);
            _mockRepository.Setup(x => x.UpdateAsync(assemblyPriceId, It.IsAny<AssemblyPrice>())).ReturnsAsync(updatedAssemblyPrice);
            _mockMapper.Setup(x => x.Map<AssemblyPriceDto>(updatedAssemblyPrice)).Returns(assemblyPriceDto);

            // Act
            var result = await _service.UpdateAsync(assemblyPriceId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(assemblyPriceDto);
            _mockRepository.Verify(x => x.UpdateAsync(assemblyPriceId, It.IsAny<AssemblyPrice>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnAssemblyPriceDto_WhenAssemblyPriceDeleted()
        {
            // Arrange
            var assemblyPriceId = "1";
            var assemblyPrice = new AssemblyPrice(Guid.NewGuid(), assemblyPriceId, "region1", "glass1", 100.00m, 80.00m);
            var assemblyPriceDto = new AssemblyPriceDto(assemblyPriceId, "region1", "glass1", 100.00m, 80.00m);

            _mockRepository.Setup(x => x.GetByIdAsync(assemblyPriceId)).ReturnsAsync(assemblyPrice);
            _mockRepository.Setup(x => x.DeleteAsync(assemblyPriceId)).ReturnsAsync(assemblyPrice);
            _mockMapper.Setup(x => x.Map<AssemblyPriceDto>(assemblyPrice)).Returns(assemblyPriceDto);

            // Act
            var result = await _service.DeleteAsync(assemblyPriceId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(assemblyPriceDto);
            _mockRepository.Verify(x => x.DeleteAsync(assemblyPriceId), Times.Once);
        }

        [Fact]
        public async Task AddAsync_WithNullValues_ShouldCreateAssemblyPrice()
        {
            // Arrange
            var createDto = new AssemblyPriceCreateDto(null, null, null, null);
            var assemblyPrice = new AssemblyPrice(Guid.NewGuid(), "1", null, null, null, null);
            var assemblyPriceDto = new AssemblyPriceDto("1", null, null, null, null);

            var validationResult = new ValidationResult();
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<AssemblyPrice>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AssemblyPrice>>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);

            _mockMapper.Setup(x => x.Map<AssemblyPrice>(createDto)).Returns(assemblyPrice);
            _mockRepository.Setup(x => x.AddAsync(assemblyPrice)).ReturnsAsync(assemblyPrice);
            _mockMapper.Setup(x => x.Map<AssemblyPriceDto>(assemblyPrice)).Returns(assemblyPriceDto);

            // Act
            var result = await _service.AddAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.RegionId.Should().BeNull();
            result.GlassId.Should().BeNull();
            result.DealerPrice.Should().BeNull();
            result.AssemblerPrice.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_WithNullValues_ShouldUpdateAssemblyPrice()
        {
            // Arrange
            var assemblyPriceId = "1";
            var updateDto = new AssemblyPriceUpdateDto(null, null, null, null);
            var existingAssemblyPrice = new AssemblyPrice(Guid.NewGuid(), assemblyPriceId, "region1", "glass1", 100.00m, 80.00m);
            var updatedAssemblyPrice = new AssemblyPrice(Guid.NewGuid(), assemblyPriceId, null, null, null, null);
            var assemblyPriceDto = new AssemblyPriceDto(assemblyPriceId, null, null, null, null);

            var validationResult = new ValidationResult();
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<AssemblyPrice>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<AssemblyPrice>>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);

            _mockRepository.Setup(x => x.GetByIdAsync(assemblyPriceId)).ReturnsAsync(existingAssemblyPrice);
            _mockRepository.Setup(x => x.UpdateAsync(assemblyPriceId, It.IsAny<AssemblyPrice>())).ReturnsAsync(updatedAssemblyPrice);
            _mockMapper.Setup(x => x.Map<AssemblyPriceDto>(updatedAssemblyPrice)).Returns(assemblyPriceDto);

            // Act
            var result = await _service.UpdateAsync(assemblyPriceId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.RegionId.Should().BeNull();
            result.GlassId.Should().BeNull();
            result.DealerPrice.Should().BeNull();
            result.AssemblerPrice.Should().BeNull();
        }
    }
}
