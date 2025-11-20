using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Controllers;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Shared.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;
using Xunit;

namespace BOnlineStore.IdentityServer.UnitTests.TenantUnitTests
{
    public class TenantControllerTests
    {
        private readonly Mock<ITenantService> _mockTenantService;
        private readonly Mock<IStringLocalizer<Language>> _mockStringLocalizer;
        private readonly TenantController _controller;

        public TenantControllerTests()
        {
            _mockTenantService = new Mock<ITenantService>();
            _mockStringLocalizer = new Mock<IStringLocalizer<Language>>();
            
            // Setup default localizer behavior
            _mockStringLocalizer
                .Setup(x => x[It.IsAny<string>()])
                .Returns((string key) => new LocalizedString(key, key));

            _controller = new TenantController(_mockTenantService.Object, _mockStringLocalizer.Object);
        }

        #region GetAllAsync Tests

        [Fact]
        public async Task GetAllAsync_HasTenants_ReturnsOkResultWithTenantList()
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

            var response = Response<List<TenantDto>>.Success(expectedTenants, HttpStatusCode.OK);

            _mockTenantService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            var responseValue = okResult.Value.Should().BeOfType<Response<List<TenantDto>>>().Subject;
            responseValue.Result.Should().HaveCount(2);
            responseValue.Result.Should().BeEquivalentTo(expectedTenants);
        }

        [Fact]
        public async Task GetAllAsync_NoTenants_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            var expectedTenants = new List<TenantDto>();
            var response = Response<List<TenantDto>>.Success(expectedTenants, HttpStatusCode.OK);

            _mockTenantService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            var responseValue = okResult.Value.Should().BeOfType<Response<List<TenantDto>>>().Subject;
            responseValue.Result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ServiceReturnsError_ReturnsInternalServerError()
        {
            // Arrange
            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_FETCH_ERROR", "Database baðlantý hatasý") 
            };
            var response = Response<List<TenantDto>>.Fail(errors, HttpStatusCode.InternalServerError);

            _mockTenantService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        #endregion

        #region GetByIdAsync Tests

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsOkResultWithTenant()
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

            var response = Response<TenantDto>.Success(expectedTenant, HttpStatusCode.OK);

            _mockTenantService
                .Setup(x => x.GetByIdAsync(tenantId))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetByIdAsync(tenantId);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            var responseValue = okResult.Value.Should().BeOfType<Response<TenantDto>>().Subject;
            responseValue.Result.Should().BeEquivalentTo(expectedTenant);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsError()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_NOT_FOUND", "Firma bulunamadý") 
            };
            var response = Response<TenantDto>.Fail(errors, HttpStatusCode.NotFound);

            _mockTenantService
                .Setup(x => x.GetByIdAsync(tenantId))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetByIdAsync(tenantId);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetByIdAsync_ServiceReturnsError_ReturnsInternalServerError()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_FETCH_ERROR", "Database eriþim hatasý") 
            };
            var response = Response<TenantDto>.Fail(errors, HttpStatusCode.InternalServerError);

            _mockTenantService
                .Setup(x => x.GetByIdAsync(tenantId))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetByIdAsync(tenantId);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        #endregion

        #region GetByNameAsync Tests

        [Fact]
        public async Task GetByNameAsync_ExistingName_ReturnsOkResultWithTenant()
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

            var response = Response<TenantDto>.Success(expectedTenant, HttpStatusCode.OK);

            _mockTenantService
                .Setup(x => x.GetByNameAsync(tenantName))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetByNameAsync(tenantName);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            var responseValue = okResult.Value.Should().BeOfType<Response<TenantDto>>().Subject;
            responseValue.Result.Should().BeEquivalentTo(expectedTenant);
        }

        [Fact]
        public async Task GetByNameAsync_NonExistingName_ReturnsError()
        {
            // Arrange
            var tenantName = "Olmayan Firma";
            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_NOT_FOUND", "Firma bulunamadý") 
            };
            var response = Response<TenantDto>.Fail(errors, HttpStatusCode.NotFound);

            _mockTenantService
                .Setup(x => x.GetByNameAsync(tenantName))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetByNameAsync(tenantName);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetByNameAsync_ServiceReturnsError_ReturnsInternalServerError()
        {
            // Arrange
            var tenantName = "Test Firma";
            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_FETCH_ERROR", "Arama hatasý") 
            };
            var response = Response<TenantDto>.Fail(errors, HttpStatusCode.InternalServerError);

            _mockTenantService
                .Setup(x => x.GetByNameAsync(tenantName))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetByNameAsync(tenantName);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        public async Task CreateAsync_ValidTenantCreateDto_ReturnsCreatedResult()
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

            var response = Response<TenantDto>.Success(expectedTenant, HttpStatusCode.Created);

            _mockTenantService
                .Setup(x => x.CreateAsync(It.IsAny<TenantCreateDto>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateAsync(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(201);
            var responseValue = okResult.Value.Should().BeOfType<Response<TenantDto>>().Subject;
            responseValue.Result.Should().BeEquivalentTo(expectedTenant);
        }

        [Fact]
        public async Task CreateAsync_ServiceReturnsError_ReturnsError()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Name = "Mevcut Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_EXISTS", "Girilen þirket sistemde mevcut") 
            };
            var response = Response<TenantDto>.Fail(errors, HttpStatusCode.BadRequest);

            _mockTenantService
                .Setup(x => x.CreateAsync(It.IsAny<TenantCreateDto>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateAsync(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        public async Task UpdateAsync_ValidTenantUpdateDto_ReturnsOkResult()
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

            var response = Response<TenantDto>.Success(expectedTenant, HttpStatusCode.OK);

            _mockTenantService
                .Setup(x => x.UpdateAsync(tenantId, It.IsAny<TenantUpdateDto>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateAsync(tenantId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            var responseValue = okResult.Value.Should().BeOfType<Response<TenantDto>>().Subject;
            responseValue.Result.Should().BeEquivalentTo(expectedTenant);
        }

        [Fact]
        public async Task UpdateAsync_MismatchedIds_ReturnsError()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var differentId = Guid.NewGuid();
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = differentId,
                Name = "Test Firma"
            };

            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.TenantIdMismatch])
                .Returns(new LocalizedString(IdentityServerKeys.TenantIdMismatch, "URL'deki ID ile gönderilen ID eþleþmiyor"));

            // Act
            var result = await _controller.UpdateAsync(tenantId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdateAsync_ServiceReturnsError_ReturnsError()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = tenantId,
                Name = "Olmayan Firma"
            };

            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_NOT_FOUND", "Güncellenecek þirket sistemde bulunamadý") 
            };
            var response = Response<TenantDto>.Fail(errors, HttpStatusCode.NotFound);

            _mockTenantService
                .Setup(x => x.UpdateAsync(tenantId, It.IsAny<TenantUpdateDto>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateAsync(tenantId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        public async Task DeleteAsync_ExistingTenant_ReturnsOkResult()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var deletedTenant = new TenantDto { Id = tenantId, Name = "Deleted Tenant" };
            var response = Response<TenantDto>.Success(deletedTenant, HttpStatusCode.OK);

            _mockTenantService
                .Setup(x => x.DeleteAsync(tenantId))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteAsync(tenantId);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteAsync_ServiceReturnsError_ReturnsError()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_DELETE_FAILED", "Firma silinemedi") 
            };
            var response = Response<TenantDto>.Fail(errors, HttpStatusCode.BadRequest);

            _mockTenantService
                .Setup(x => x.DeleteAsync(tenantId))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteAsync(tenantId);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        #endregion

        #region IsAnyTenantExist Tests

        [Fact]
        public async Task IsAnyTenantExist_TenantsExist_ReturnsOkResultWithTrue()
        {
            // Arrange
            var response = Response<bool>.Success(true, HttpStatusCode.OK);

            _mockTenantService
                .Setup(x => x.IsAnyTenantExistAsync())
                .ReturnsAsync(response);

            // Act
            var result = await _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            var responseValue = okResult.Value.Should().BeOfType<Response<bool>>().Subject;
            responseValue.Result.Should().BeTrue();
        }

        [Fact]
        public async Task IsAnyTenantExist_NoTenantsExist_ReturnsOkResultWithFalse()
        {
            // Arrange
            var response = Response<bool>.Success(false, HttpStatusCode.OK);

            _mockTenantService
                .Setup(x => x.IsAnyTenantExistAsync())
                .ReturnsAsync(response);

            // Act
            var result = await _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            var responseValue = okResult.Value.Should().BeOfType<Response<bool>>().Subject;
            responseValue.Result.Should().BeFalse();
        }

        [Fact]
        public async Task IsAnyTenantExist_ServiceReturnsError_ReturnsInternalServerError()
        {
            // Arrange
            var errors = new List<Error> 
            { 
                Error.CreateError("TENANT_EXISTENCE_CHECK_ERROR", "Database baðlantý hatasý") 
            };
            var response = Response<bool>.Fail(errors, HttpStatusCode.InternalServerError);
            
            _mockTenantService
                .Setup(x => x.IsAnyTenantExistAsync())
                .ReturnsAsync(response);

            // Act
            var result = await _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        #endregion
    }
}
