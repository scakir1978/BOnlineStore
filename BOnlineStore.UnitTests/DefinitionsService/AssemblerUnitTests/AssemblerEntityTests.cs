using BOnlineStore.Services.Definitions.Api.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace BOnlineStore.UnitTests.DefinitionsService.AssemblerUnitTests
{
    public class AssemblerEntityTests
    {
        [Fact]
        public void Constructor_WithParameters_ShouldCreateAssemblerWithCorrectValues()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var id = "1";
            var code = "ASM001";
            var name = "Test Assembler";

            // Act
            var assembler = new Assembler(tenantId, id, code, name);

            // Assert
            assembler.Should().NotBeNull();
            assembler.Id.Should().Be(id);
            assembler.Code.Should().Be(code);
            assembler.Name.Should().Be(name);
            assembler.TenantId.Should().Be(tenantId);
        }

        [Fact]
        public void Constructor_Default_ShouldCreateEmptyAssembler()
        {
            // Act
            var assembler = new Assembler();

            // Assert
            assembler.Should().NotBeNull();
            assembler.Code.Should().Be("");
            assembler.Name.Should().Be("");
        }

        [Fact]
        public void UpdateAssembler_ShouldUpdateCodeAndName()
        {
            // Arrange
            var assembler = new Assembler();
            var newCode = "ASM002";
            var newName = "Updated Assembler";

            // Act
            assembler.UpdateAssembler(newCode, newName);

            // Assert
            assembler.Code.Should().Be(newCode);
            assembler.Name.Should().Be(newName);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("ASM001", "")]
        [InlineData("", "Test Assembler")]
        [InlineData("ASM001", "Test Assembler")]
        public void UpdateAssembler_WithVariousInputs_ShouldSetCorrectValues(string code, string name)
        {
            // Arrange
            var assembler = new Assembler();

            // Act
            assembler.UpdateAssembler(code, name);

            // Assert
            assembler.Code.Should().Be(code);
            assembler.Name.Should().Be(name);
        }
    }
}