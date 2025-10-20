using BOnlineStore.Services.Definitions.Api.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace BOnlineStore.Services.Definitions.Api.UnitTests.AssemblyPriceUnitTests
{
    public class AssemblyPriceEntityTests
    {
        [Fact]
        public void Constructor_WithParameters_ShouldCreateAssemblyPriceWithCorrectValues()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var id = "1";
            var regionId = "region1";
            var glassId = "glass1";
            var dealerPrice = 100.00m;
            var assemblerPrice = 80.00m;

            // Act
            var assemblyPrice = new AssemblyPrice(tenantId, id, regionId, glassId, dealerPrice, assemblerPrice);

            // Assert
            assemblyPrice.Should().NotBeNull();
            assemblyPrice.Id.Should().Be(id);
            assemblyPrice.RegionId.Should().Be(regionId);
            assemblyPrice.GlassId.Should().Be(glassId);
            assemblyPrice.DealerPrice.Should().Be(dealerPrice);
            assemblyPrice.AssemblerPrice.Should().Be(assemblerPrice);
            assemblyPrice.TenantId.Should().Be(tenantId);
        }

        [Fact]
        public void Constructor_Default_ShouldCreateEmptyAssemblyPrice()
        {
            // Act
            var assemblyPrice = new AssemblyPrice();

            // Assert
            assemblyPrice.Should().NotBeNull();
            assemblyPrice.RegionId.Should().Be("");
            assemblyPrice.GlassId.Should().Be("");
            assemblyPrice.DealerPrice.Should().Be(0);
            assemblyPrice.AssemblerPrice.Should().Be(0);
        }

        [Fact]
        public void UpdateAssemblyPrice_ShouldUpdateAllProperties()
        {
            // Arrange
            var assemblyPrice = new AssemblyPrice();
            var newRegionId = "region2";
            var newGlassId = "glass2";
            var newDealerPrice = 150.00m;
            var newAssemblerPrice = 120.00m;

            // Act
            assemblyPrice.UpdateAssemblyPrice(newRegionId, newGlassId, newDealerPrice, newAssemblerPrice);

            // Assert
            assemblyPrice.RegionId.Should().Be(newRegionId);
            assemblyPrice.GlassId.Should().Be(newGlassId);
            assemblyPrice.DealerPrice.Should().Be(newDealerPrice);
            assemblyPrice.AssemblerPrice.Should().Be(newAssemblerPrice);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("region1", "glass1", 100.00, 80.00)]
        [InlineData("region2", "glass2", 150.50, 120.75)]
        public void UpdateAssemblyPrice_WithVariousInputs_ShouldSetCorrectValues(
            string? regionId, string? glassId, double? dealerPrice, double? assemblerPrice)
        {
            // Arrange
            var assemblyPrice = new AssemblyPrice();

            // Act
            assemblyPrice.UpdateAssemblyPrice(regionId, glassId, (decimal?)dealerPrice, (decimal?)assemblerPrice);

            // Assert
            assemblyPrice.RegionId.Should().Be(regionId);
            assemblyPrice.GlassId.Should().Be(glassId);
            assemblyPrice.DealerPrice.Should().Be((decimal?)dealerPrice);
            assemblyPrice.AssemblerPrice.Should().Be((decimal?)assemblerPrice);
        }

        [Fact]
        public void UpdateAssemblyPrice_WithNullValues_ShouldAcceptNulls()
        {
            // Arrange
            var assemblyPrice = new AssemblyPrice(Guid.NewGuid(), "1", "region1", "glass1", 100.00m, 80.00m);

            // Act
            assemblyPrice.UpdateAssemblyPrice(null, null, null, null);

            // Assert
            assemblyPrice.RegionId.Should().BeNull();
            assemblyPrice.GlassId.Should().BeNull();
            assemblyPrice.DealerPrice.Should().BeNull();
            assemblyPrice.AssemblerPrice.Should().BeNull();
        }

        [Fact]
        public void UpdateAssemblyPrice_MultipleTimes_ShouldKeepLatestValues()
        {
            // Arrange
            var assemblyPrice = new AssemblyPrice();

            // Act - First update
            assemblyPrice.UpdateAssemblyPrice("region1", "glass1", 100.00m, 80.00m);
            
            // Act - Second update
            assemblyPrice.UpdateAssemblyPrice("region2", "glass2", 150.00m, 120.00m);

            // Assert
            assemblyPrice.RegionId.Should().Be("region2");
            assemblyPrice.GlassId.Should().Be("glass2");
            assemblyPrice.DealerPrice.Should().Be(150.00m);
            assemblyPrice.AssemblerPrice.Should().Be(120.00m);
        }
    }
}
