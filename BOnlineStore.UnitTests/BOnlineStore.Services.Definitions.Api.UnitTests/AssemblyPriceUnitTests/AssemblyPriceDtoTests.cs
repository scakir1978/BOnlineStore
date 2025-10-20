using BOnlineStore.Services.Definitions.Api.Dtos;
using FluentAssertions;
using Xunit;

namespace BOnlineStore.Services.Definitions.Api.UnitTests.AssemblyPriceUnitTests
{
    public class AssemblyPriceDtoTests
    {
        [Fact]
        public void AssemblyPriceDto_Constructor_ShouldSetProperties()
        {
            // Arrange
            var id = "1";
            var regionId = "region1";
            var glassId = "glass1";
            var dealerPrice = 100.00m;
            var assemblerPrice = 80.00m;

            // Act
            var dto = new AssemblyPriceDto(id, regionId, glassId, dealerPrice, assemblerPrice);

            // Assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(id);
            dto.RegionId.Should().Be(regionId);
            dto.GlassId.Should().Be(glassId);
            dto.DealerPrice.Should().Be(dealerPrice);
            dto.AssemblerPrice.Should().Be(assemblerPrice);
        }

        [Fact]
        public void AssemblyPriceCreateDto_Constructor_ShouldSetProperties()
        {
            // Arrange
            var regionId = "region1";
            var glassId = "glass1";
            var dealerPrice = 100.00m;
            var assemblerPrice = 80.00m;

            // Act
            var dto = new AssemblyPriceCreateDto(regionId, glassId, dealerPrice, assemblerPrice);

            // Assert
            dto.Should().NotBeNull();
            dto.RegionId.Should().Be(regionId);
            dto.GlassId.Should().Be(glassId);
            dto.DealerPrice.Should().Be(dealerPrice);
            dto.AssemblerPrice.Should().Be(assemblerPrice);
            dto.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void AssemblyPriceUpdateDto_Constructor_ShouldSetProperties()
        {
            // Arrange
            var regionId = "region1";
            var glassId = "glass1";
            var dealerPrice = 100.00m;
            var assemblerPrice = 80.00m;

            // Act
            var dto = new AssemblyPriceUpdateDto(regionId, glassId, dealerPrice, assemblerPrice);

            // Assert
            dto.Should().NotBeNull();
            dto.RegionId.Should().Be(regionId);
            dto.GlassId.Should().Be(glassId);
            dto.DealerPrice.Should().Be(dealerPrice);
            dto.AssemblerPrice.Should().Be(assemblerPrice);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("region1", "glass1", 100.00, 80.00)]
        [InlineData("region2", "glass2", 150.50, 120.75)]
        public void AssemblyPriceCreateDto_WithVariousInputs_ShouldSetCorrectValues(
            string? regionId, string? glassId, double? dealerPrice, double? assemblerPrice)
        {
            // Act
            var dto = new AssemblyPriceCreateDto(regionId, glassId, (decimal?)dealerPrice, (decimal?)assemblerPrice);

            // Assert
            dto.RegionId.Should().Be(regionId);
            dto.GlassId.Should().Be(glassId);
            dto.DealerPrice.Should().Be((decimal?)dealerPrice);
            dto.AssemblerPrice.Should().Be((decimal?)assemblerPrice);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("region1", "glass1", 100.00, 80.00)]
        [InlineData("region2", "glass2", 150.50, 120.75)]
        public void AssemblyPriceUpdateDto_WithVariousInputs_ShouldSetCorrectValues(
            string? regionId, string? glassId, double? dealerPrice, double? assemblerPrice)
        {
            // Act
            var dto = new AssemblyPriceUpdateDto(regionId, glassId, (decimal?)dealerPrice, (decimal?)assemblerPrice);

            // Assert
            dto.RegionId.Should().Be(regionId);
            dto.GlassId.Should().Be(glassId);
            dto.DealerPrice.Should().Be((decimal?)dealerPrice);
            dto.AssemblerPrice.Should().Be((decimal?)assemblerPrice);
        }

        [Fact]
        public void AssemblyPriceDto_Properties_ShouldAllowGetAndSet()
        {
            // Arrange
            var dto = new AssemblyPriceDto("1", "region1", "glass1", 100.00m, 80.00m);
            var newRegionId = "region2";
            var newGlassId = "glass2";
            var newDealerPrice = 150.00m;
            var newAssemblerPrice = 120.00m;

            // Act
            dto.RegionId = newRegionId;
            dto.GlassId = newGlassId;
            dto.DealerPrice = newDealerPrice;
            dto.AssemblerPrice = newAssemblerPrice;

            // Assert
            dto.RegionId.Should().Be(newRegionId);
            dto.GlassId.Should().Be(newGlassId);
            dto.DealerPrice.Should().Be(newDealerPrice);
            dto.AssemblerPrice.Should().Be(newAssemblerPrice);
        }

        [Fact]
        public void AssemblyPriceCreateDto_WithNullValues_ShouldAcceptNulls()
        {
            // Act
            var dto = new AssemblyPriceCreateDto(null, null, null, null);

            // Assert
            dto.RegionId.Should().BeNull();
            dto.GlassId.Should().BeNull();
            dto.DealerPrice.Should().BeNull();
            dto.AssemblerPrice.Should().BeNull();
        }

        [Fact]
        public void AssemblyPriceUpdateDto_WithNullValues_ShouldAcceptNulls()
        {
            // Act
            var dto = new AssemblyPriceUpdateDto(null, null, null, null);

            // Assert
            dto.RegionId.Should().BeNull();
            dto.GlassId.Should().BeNull();
            dto.DealerPrice.Should().BeNull();
            dto.AssemblerPrice.Should().BeNull();
        }
    }
}
