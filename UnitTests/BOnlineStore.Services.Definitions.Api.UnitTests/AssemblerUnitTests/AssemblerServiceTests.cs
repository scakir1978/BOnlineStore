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

namespace BOnlineStore.Services.Definitions.Api.UnitTests.AssemblerUnitTests
{
    public class AssemblerServiceTests
    {
        private readonly Mock<IAssemblerRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IStringLocalizer<Language>> _mockStringLocalizer;
        private readonly Mock<IValidator<Assembler>> _mockValidator;
        private readonly AssemblerService _service;

        public AssemblerServiceTests()
        {
            _mockRepository = new Mock<IAssemblerRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockStringLocalizer = new Mock<IStringLocalizer<Language>>();
            _mockValidator = new Mock<IValidator<Assembler>>();
            
            _service = new AssemblerService(
                _mockRepository.Object,
                _mockMapper.Object,
                _mockStringLocalizer.Object,
                _mockValidator.Object);
        }

        [Fact]
        public void Load_ShouldReturnQueryable_WhenCalled()
        {
            // Arrange
            var assemblers = new List<Assembler>
            {
                new Assembler(Guid.NewGuid(), "1", "ASM001", "Assembler 1"),
                new Assembler(Guid.NewGuid(), "2", "ASM002", "Assembler 2")
            }.AsQueryable();

            _mockRepository.Setup(x => x.Load(It.IsAny<Expression<Func<Assembler, bool>>>())).Returns(assemblers);

            // Act
            var result = _service.Load();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IQueryable<Assembler>>();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAssemblerDtos_WhenAssemblersExist()
        {
            // Arrange
            var assemblers = new List<Assembler>
            {
                new Assembler(Guid.NewGuid(), "1", "ASM001", "Assembler 1"),
                new Assembler(Guid.NewGuid(), "2", "ASM002", "Assembler 2")
            };

            var assemblerDtos = new List<AssemblerDto>
            {
                new AssemblerDto("1", "ASM001", "Assembler 1"),
                new AssemblerDto("2", "ASM002", "Assembler 2")
            };

            _mockRepository.Setup(x => x.GetAsync()).ReturnsAsync(assemblers);
            _mockMapper.Setup(x => x.Map<List<AssemblerDto>>(assemblers)).Returns(assemblerDtos);

            // Act
            var result = await _service.GetAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(assemblerDtos);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAssemblerDto_WhenAssemblerExists()
        {
            // Arrange
            var assemblerId = "1";
            var assembler = new Assembler(Guid.NewGuid(), assemblerId, "ASM001", "Assembler 1");
            var assemblerDto = new AssemblerDto(assemblerId, "ASM001", "Assembler 1");

            _mockRepository.Setup(x => x.GetByIdAsync(assemblerId)).ReturnsAsync(assembler);
            _mockMapper.Setup(x => x.Map<AssemblerDto>(assembler)).Returns(assemblerDto);

            // Act
            var result = await _service.GetByIdAsync(assemblerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(assemblerDto);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAssemblerDto_WhenAssemblerCreated()
        {
            // Arrange
            var createDto = new AssemblerCreateDto("ASM001", "New Assembler");
            var assembler = new Assembler(Guid.NewGuid(), "1", "ASM001", "New Assembler");
            var assemblerDto = new AssemblerDto("1", "ASM001", "New Assembler");

            // Setup validator to return a valid result for both possible ValidateAsync calls
            var validationResult = new ValidationResult();
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<Assembler>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<Assembler>>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);

            _mockMapper.Setup(x => x.Map<Assembler>(createDto)).Returns(assembler);
            _mockRepository.Setup(x => x.AddAsync(assembler)).ReturnsAsync(assembler);
            _mockMapper.Setup(x => x.Map<AssemblerDto>(assembler)).Returns(assemblerDto);

            // Act
            var result = await _service.AddAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(assemblerDto);
            _mockRepository.Verify(x => x.AddAsync(assembler), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnAssemblerDto_WhenAssemblerUpdated()
        {
            // Arrange
            var assemblerId = "1";
            var updateDto = new AssemblerUpdateDto("ASM001", "Updated Assembler");
            var existingAssembler = new Assembler(Guid.NewGuid(), assemblerId, "ASM000", "Old Assembler");
            var updatedAssembler = new Assembler(Guid.NewGuid(), assemblerId, "ASM001", "Updated Assembler");
            var assemblerDto = new AssemblerDto(assemblerId, "ASM001", "Updated Assembler");

            // Setup validator to return a valid result for both possible ValidateAsync calls
            var validationResult = new ValidationResult();
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<Assembler>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<Assembler>>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(validationResult);

            _mockRepository.Setup(x => x.GetByIdAsync(assemblerId)).ReturnsAsync(existingAssembler);
            _mockRepository.Setup(x => x.UpdateAsync(assemblerId, It.IsAny<Assembler>())).ReturnsAsync(updatedAssembler);
            _mockMapper.Setup(x => x.Map<AssemblerDto>(updatedAssembler)).Returns(assemblerDto);

            // Act
            var result = await _service.UpdateAsync(assemblerId, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(assemblerDto);
            _mockRepository.Verify(x => x.UpdateAsync(assemblerId, It.IsAny<Assembler>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnAssemblerDto_WhenAssemblerDeleted()
        {
            // Arrange
            var assemblerId = "1";
            var assembler = new Assembler(Guid.NewGuid(), assemblerId, "ASM001", "Assembler to Delete");
            var assemblerDto = new AssemblerDto(assemblerId, "ASM001", "Assembler to Delete");

            _mockRepository.Setup(x => x.GetByIdAsync(assemblerId)).ReturnsAsync(assembler);
            _mockRepository.Setup(x => x.DeleteAsync(assemblerId)).ReturnsAsync(assembler);
            _mockMapper.Setup(x => x.Map<AssemblerDto>(assembler)).Returns(assemblerDto);

            // Act
            var result = await _service.DeleteAsync(assemblerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(assemblerDto);
            _mockRepository.Verify(x => x.DeleteAsync(assemblerId), Times.Once);
        }
    }
}
