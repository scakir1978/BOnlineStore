using BOnlineStore.Services.Definitions.Api.Dtos;
using FluentAssertions;
using Xunit;

namespace BOnlineStore.Services.Definitions.Api.UnitTests.AssemblerUnitTests
{
    public class AssemblerDtoTests
    {
        [Fact]
        public void AssemblerDto_Constructor_ShouldSetProperties()
        {
            // Arrange
            var id = "1";
            var code = "ASM001";
            var name = "Test Assembler";

            // Act
            var dto = new AssemblerDto(id, code, name);

            // Assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(id);
            dto.Code.Should().Be(code);
            dto.Name.Should().Be(name);
        }

        [Fact]
        public void AssemblerCreateDto_Constructor_ShouldSetProperties()
        {
            // Arrange
            var code = "ASM001";
            var name = "Test Assembler";

            // Act
            var dto = new AssemblerCreateDto(code, name);

            // Assert
            dto.Should().NotBeNull();
            dto.Code.Should().Be(code);
            dto.Name.Should().Be(name);
            dto.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void AssemblerUpdateDto_Constructor_ShouldSetProperties()
        {
            // Arrange
            var code = "ASM001";
            var name = "Test Assembler";

            // Act
            var dto = new AssemblerUpdateDto(code, name);

            // Assert
            dto.Should().NotBeNull();
            dto.Code.Should().Be(code);
            dto.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("ASM001", "Test Assembler")]
        public void AssemblerCreateDto_WithVariousInputs_ShouldSetCorrectValues(string code, string name)
        {
            // Act
            var dto = new AssemblerCreateDto(code, name);

            // Assert
            dto.Code.Should().Be(code);
            dto.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("ASM001", "Test Assembler")]
        public void AssemblerUpdateDto_WithVariousInputs_ShouldSetCorrectValues(string code, string name)
        {
            // Act
            var dto = new AssemblerUpdateDto(code, name);

            // Assert
            dto.Code.Should().Be(code);
            dto.Name.Should().Be(name);
        }

        [Fact]
        public void AssemblerDto_Properties_ShouldAllowGetAndSet()
        {
            // Arrange
            var dto = new AssemblerDto("1", "ASM001", "Test Assembler");
            var newCode = "ASM002";
            var newName = "Updated Assembler";

            // Act
            dto.Code = newCode;
            dto.Name = newName;

            // Assert
            dto.Code.Should().Be(newCode);
            dto.Name.Should().Be(newName);
        }
    }
}
