using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Controllers;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BOnlineStore.IdentityServer.UnitTests.TenantUnitTests
{
    public class TenantControllerTests
    {
        private readonly Mock<ITenantService> _mockTenantService;
        private readonly TenantController _controller;

        public TenantControllerTests()
        {
            _mockTenantService = new Mock<ITenantService>();
            _controller = new TenantController(_mockTenantService.Object);
        }

        #region GetAllTenants Tests

        [Fact]
        public void GetAllTenants_HasTenants_ReturnsOkResultWithTenantList()
        {
            // Arrange
            var expectedTenants = new List<TenantDto>
            {
                new TenantDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Firma 1",
                    CreateDateTime = DateTime.Now.AddDays(-10),
                    UpdateDateTime = DateTime.Now.AddDays(-5)
                },
                new TenantDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Firma 2",
                    CreateDateTime = DateTime.Now.AddDays(-20),
                    UpdateDateTime = DateTime.Now.AddDays(-1)
                }
            };

            _mockTenantService
                .Setup(x => x.Tenants())
                .Returns(expectedTenants.AsQueryable());

            // Act
            var result = _controller.GetAllTenants();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var tenants = okResult.Value as List<TenantDto>;
            tenants.Should().HaveCount(2);
            tenants.Should().BeEquivalentTo(expectedTenants);
        }

        [Fact]
        public void GetAllTenants_NoTenants_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            var expectedTenants = new List<TenantDto>();

            _mockTenantService
                .Setup(x => x.Tenants())
                .Returns(expectedTenants.AsQueryable());

            // Act
            var result = _controller.GetAllTenants();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var tenants = okResult.Value as List<TenantDto>;
            tenants.Should().BeEmpty();
        }

        [Fact]
        public void GetAllTenants_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            _mockTenantService
                .Setup(x => x.Tenants())
                .Throws(new Exception("Database baðlantý hatasý"));

            // Act
            var result = _controller.GetAllTenants();

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
            statusResult.Value.Should().Be("Firmalar getirilirken hata oluþtu: Database baðlantý hatasý");
        }

        #endregion

        #region GetTenantById Tests

        [Fact]
        public void GetTenantById_ExistingId_ReturnsOkResultWithTenant()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var expectedTenant = new TenantDto
            {
                Id = tenantId,
                Name = "Test Firma",
                CreateDateTime = DateTime.Now.AddDays(-10),
                UpdateDateTime = DateTime.Now.AddDays(-5),
                Adress = new Adress
                {
                    Adress1 = "Test Adres 1",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ýstanbul"
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "1234567890",
                    TaxAdministration = "Test Vergi Dairesi"
                }
            };

            _mockTenantService
                .Setup(x => x.FindById(tenantId))
                .Returns(expectedTenant);

            // Act
            var result = _controller.GetTenantById(tenantId);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedTenant);
        }

        [Fact]
        public void GetTenantById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var tenantId = Guid.NewGuid();

            _mockTenantService
                .Setup(x => x.FindById(tenantId))
                .Returns((TenantDto)null);

            // Act
            var result = _controller.GetTenantById(tenantId);

            // Assert
            result.Should().NotBeNull();
            var notFoundResult = result.Result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("Firma bulunamadý");
        }

        [Fact]
        public void GetTenantById_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var tenantId = Guid.NewGuid();

            _mockTenantService
                .Setup(x => x.FindById(tenantId))
                .Throws(new Exception("Database eriþim hatasý"));

            // Act
            var result = _controller.GetTenantById(tenantId);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
            statusResult.Value.Should().Be("Firma getirilirken hata oluþtu: Database eriþim hatasý");
        }

        #endregion

        #region GetTenantByName Tests

        [Fact]
        public void GetTenantByName_ExistingName_ReturnsOkResultWithTenant()
        {
            // Arrange
            var tenantName = "Test Firma";
            var expectedTenant = new TenantDto
            {
                Id = Guid.NewGuid(),
                Name = tenantName,
                CreateDateTime = DateTime.Now.AddDays(-10),
                UpdateDateTime = DateTime.Now.AddDays(-5)
            };

            _mockTenantService
                .Setup(x => x.FindByName(tenantName))
                .Returns(expectedTenant);

            // Act
            var result = _controller.GetTenantByName(tenantName);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedTenant);
        }

        [Fact]
        public void GetTenantByName_NonExistingName_ReturnsNotFound()
        {
            // Arrange
            var tenantName = "Olmayan Firma";

            _mockTenantService
                .Setup(x => x.FindByName(tenantName))
                .Returns((TenantDto)null);

            // Act
            var result = _controller.GetTenantByName(tenantName);

            // Assert
            result.Should().NotBeNull();
            var notFoundResult = result.Result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("Firma bulunamadý");
        }

        [Fact]
        public void GetTenantByName_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var tenantName = "Test Firma";

            _mockTenantService
                .Setup(x => x.FindByName(tenantName))
                .Throws(new Exception("Arama hatasý"));

            // Act
            var result = _controller.GetTenantByName(tenantName);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
            statusResult.Value.Should().Be("Firma getirilirken hata oluþtu: Arama hatasý");
        }

        #endregion

        #region CreateTenant Tests

        [Fact]
        public async Task CreateTenant_ValidTenantCreateDto_ReturnsCreatedResult()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Yeni Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Adress = new Adress
                {
                    Adress1 = "Test Adres",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ýstanbul",
                    PostalCode = 34000
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "1234567890",
                    TaxAdministration = "Test Vergi Dairesi"
                }
            };

            var expectedTenant = new TenantDto
            {
                Id = tenantCreateDto.Id,
                Name = tenantCreateDto.Name,
                CreateDateTime = tenantCreateDto.CreateDateTime,
                UpdateDateTime = tenantCreateDto.UpdateDateTime,
                Adress = tenantCreateDto.Adress,
                TaxInformation = tenantCreateDto.TaxInformation
            };

            _mockTenantService
                .Setup(x => x.CreateAsync(It.IsAny<TenantCreateDto>()))
                .ReturnsAsync(expectedTenant);

            // Act
            var result = await _controller.CreateTenant(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.Value.Should().BeEquivalentTo(expectedTenant);
            createdResult.ActionName.Should().Be(nameof(TenantController.GetTenantById));
            createdResult.RouteValues["id"].Should().Be(expectedTenant.Id);
        }

        [Fact]
        public async Task CreateTenant_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto(); // Invalid DTO
            _controller.ModelState.AddModelError("Name", "Firma adý gereklidir");

            // Act
            var result = await _controller.CreateTenant(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateTenant_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Name = "Mevcut Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _mockTenantService
                .Setup(x => x.CreateAsync(It.IsAny<TenantCreateDto>()))
                .ThrowsAsync(new Exception("Girilen þirket sistemde mevcut"));

            // Act
            var result = await _controller.CreateTenant(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("Girilen þirket sistemde mevcut");
        }

        #endregion

        #region UpdateTenant Tests

        [Fact]
        public async Task UpdateTenant_ValidTenantUpdateDto_ReturnsOkResult()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = tenantId,
                Name = "Güncellenmiþ Firma",
                Adress = new Adress
                {
                    Adress1 = "Güncellenmiþ Adres",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ankara"
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "9876543210",
                    TaxAdministration = "Yeni Vergi Dairesi"
                }
            };

            var expectedTenant = new TenantDto
            {
                Id = tenantUpdateDto.Id,
                Name = tenantUpdateDto.Name,
                Adress = tenantUpdateDto.Adress,
                TaxInformation = tenantUpdateDto.TaxInformation,
                UpdateDateTime = DateTime.Now
            };

            _mockTenantService
                .Setup(x => x.UpdateAsync(It.IsAny<TenantUpdateDto>()))
                .ReturnsAsync(expectedTenant);

            // Act
            var result = await _controller.UpdateTenant(tenantId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedTenant);
        }

        [Fact]
        public async Task UpdateTenant_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var tenantUpdateDto = new TenantUpdateDto { Id = tenantId };
            _controller.ModelState.AddModelError("Name", "Firma adý gereklidir");

            // Act
            var result = await _controller.UpdateTenant(tenantId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateTenant_MismatchedIds_ReturnsBadRequest()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var differentId = Guid.NewGuid();
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = differentId,
                Name = "Test Firma"
            };

            // Act
            var result = await _controller.UpdateTenant(tenantId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("URL'deki ID ile gönderilen ID eþleþmiyor");
        }

        [Fact]
        public async Task UpdateTenant_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = tenantId,
                Name = "Olmayan Firma"
            };

            _mockTenantService
                .Setup(x => x.UpdateAsync(It.IsAny<TenantUpdateDto>()))
                .ThrowsAsync(new Exception("Güncellenecek þirket sistemde bulunamadý"));

            // Act
            var result = await _controller.UpdateTenant(tenantId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("Güncellenecek þirket sistemde bulunamadý");
        }

        #endregion

        #region DeleteTenant Tests

        [Fact]
        public async Task DeleteTenant_ExistingTenant_ReturnsNoContent()
        {
            // Arrange
            var tenantId = Guid.NewGuid();

            _mockTenantService
                .Setup(x => x.DeleteAsync(tenantId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteTenant(tenantId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteTenant_ServiceReturnsFalse_ReturnsBadRequest()
        {
            // Arrange
            var tenantId = Guid.NewGuid();

            _mockTenantService
                .Setup(x => x.DeleteAsync(tenantId))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteTenant(tenantId);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("Firma silinemedi");
        }

        [Fact]
        public async Task DeleteTenant_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var tenantId = Guid.NewGuid();

            _mockTenantService
                .Setup(x => x.DeleteAsync(tenantId))
                .ThrowsAsync(new Exception("Silinecek þirket sistemde bulunamadý."));

            // Act
            var result = await _controller.DeleteTenant(tenantId);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("Silinecek þirket sistemde bulunamadý.");
        }

        #endregion

        #region IsAnyTenantExist Tests

        [Fact]
        public void IsAnyTenantExist_TenantsExist_ReturnsOkResultWithTrue()
        {
            // Arrange
            _mockTenantService
                .Setup(x => x.IsAnyTenantExist())
                .Returns(true);

            // Act
            var result = _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(true);
        }

        [Fact]
        public void IsAnyTenantExist_NoTenantsExist_ReturnsOkResultWithFalse()
        {
            // Arrange
            _mockTenantService
                .Setup(x => x.IsAnyTenantExist())
                .Returns(false);

            // Act
            var result = _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(false);
        }

        [Fact]
        public void IsAnyTenantExist_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            _mockTenantService
                .Setup(x => x.IsAnyTenantExist())
                .Throws(new Exception("Database baðlantý hatasý"));

            // Act
            var result = _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
            statusResult.Value.Should().Be("Firma varlýk kontrolü yapýlýrken hata oluþtu: Database baðlantý hatasý");
        }

        #endregion
    }
}
