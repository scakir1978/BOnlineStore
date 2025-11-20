using AutoMapper;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Shared.Dtos;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;
using Xunit;

namespace BOnlineStore.IdentityServer.UnitTests.TenantUnitTests
{
    public class TenantManagerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IStringLocalizer<Language>> _mockStringLocalizer;
        private readonly TenantManager _tenantManager;

        public TenantManagerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _mockStringLocalizer = new Mock<IStringLocalizer<Language>>();
            
            // Setup default localizer behavior
            _mockStringLocalizer
                .Setup(x => x[It.IsAny<string>()])
                .Returns((string key) => new LocalizedString(key, key));
            
            // Setup specific localization keys
            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.TenantAlreadyExists])
                .Returns(new LocalizedString(IdentityServerKeys.TenantAlreadyExists, "Girilen þirket sistemde mevcut"));
            
            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.TenantNotFoundForDelete])
                .Returns(new LocalizedString(IdentityServerKeys.TenantNotFoundForDelete, "Silinecek þirket sistemde bulunamadý"));
            
            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.TenantNotFoundForUpdate])
                .Returns(new LocalizedString(IdentityServerKeys.TenantNotFoundForUpdate, "Güncellenecek þirket sistemde bulunamadý"));
            
            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.TenantNotFound])
                .Returns(new LocalizedString(IdentityServerKeys.TenantNotFound, "Tenant bulunamadý"));

            _tenantManager = new TenantManager(_context, _mockMapper.Object, _mockStringLocalizer.Object);
        }

        #region CreateAsync Tests

        [Fact]
        public async Task CreateAsync_ValidTenantCreateDto_ReturnsSuccessResponse()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            var tenant = new Tenant
            {
                Id = tenantCreateDto.Id,
                Name = tenantCreateDto.Name,
                CreateDateTime = tenantCreateDto.CreateDateTime,
                UpdateDateTime = tenantCreateDto.UpdateDateTime
            };

            var expectedTenantDto = new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                CreateDateTime = tenant.CreateDateTime.Value,
                UpdateDateTime = tenant.UpdateDateTime.Value
            };

            _mockMapper.Setup(m => m.Map<Tenant>(tenantCreateDto)).Returns(tenant);
            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns(expectedTenantDto);

            // Act
            var result = await _tenantManager.CreateAsync(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Result.Should().BeEquivalentTo(expectedTenantDto);
            
            var savedTenant = await _context.Tenant.FindAsync(tenant.Id);
            savedTenant.Should().NotBeNull();
            savedTenant.Name.Should().Be(tenantCreateDto.Name);
        }

        [Fact]
        public async Task CreateAsync_DuplicateName_ReturnsFailResponse()
        {
            // Arrange
            var existingTenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Existing Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(existingTenant);
            await _context.SaveChangesAsync();

            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Existing Firma", // Same name
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            var tenant = new Tenant
            {
                Id = tenantCreateDto.Id,
                Name = tenantCreateDto.Name,
                CreateDateTime = tenantCreateDto.CreateDateTime,
                UpdateDateTime = tenantCreateDto.UpdateDateTime
            };

            _mockMapper.Setup(m => m.Map<Tenant>(tenantCreateDto)).Returns(tenant);

            // Act
            var result = await _tenantManager.CreateAsync(tenantCreateDto);
            
            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().Message.Should().Contain("Girilen þirket sistemde mevcut");
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        public async Task UpdateAsync_ValidTenantUpdateDto_ReturnsSuccessResponse()
        {
            // Arrange
            var existingTenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Original Firma",
                CreateDateTime = DateTime.Now.AddDays(-10),
                UpdateDateTime = DateTime.Now.AddDays(-5)
            };

            _context.Tenant.Add(existingTenant);
            await _context.SaveChangesAsync();

            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = existingTenant.Id,
                Name = "Updated Firma"
            };

            var expectedTenantDto = new TenantDto
            {
                Id = existingTenant.Id,
                Name = "Updated Firma",
                CreateDateTime = existingTenant.CreateDateTime.Value,
                UpdateDateTime = DateTime.Now
            };

            // Mock mapper to update the existing entity
            _mockMapper.Setup(m => m.Map(tenantUpdateDto, existingTenant))
                      .Callback<TenantUpdateDto, Tenant>((dto, tenant) => 
                      {
                          tenant.Name = dto.Name;
                          if (dto.Adress != null) tenant.Adress = dto.Adress;
                          if (dto.TaxInformation != null) tenant.TaxInformation = dto.TaxInformation;
                      })
                      .Returns(existingTenant);
            
            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns(expectedTenantDto);

            // Act
            var result = await _tenantManager.UpdateAsync(existingTenant.Id, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            result.Result.Name.Should().Be("Updated Firma");
        }

        [Fact]
        public async Task UpdateAsync_NonExistentTenant_ReturnsFailResponse()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = nonExistentId,
                Name = "Non-existent Firma"
            };

            // Act
            var result = await _tenantManager.UpdateAsync(nonExistentId, tenantUpdateDto);
            
            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().Message.Should().Contain("Tenant bulunamadý");
        }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        public async Task DeleteAsync_ExistingTenant_ReturnsSuccessResponse()
        {
            // Arrange
            var existingTenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "To Delete Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(existingTenant);
            await _context.SaveChangesAsync();

            var existingTenantDto = new TenantDto
            {
                Id = existingTenant.Id,
                Name = existingTenant.Name
            };

            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns(existingTenantDto);

            // Act
            var result = await _tenantManager.DeleteAsync(existingTenant.Id);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            
            var deletedTenant = await _context.Tenant.FindAsync(existingTenant.Id);
            deletedTenant.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_NonExistentTenant_ReturnsFailResponse()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _tenantManager.DeleteAsync(nonExistentId);
            
            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().Message.Should().Contain("Tenant bulunamadý");
        }

        #endregion

        #region GetByIdAsync Tests

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsSuccessResponse()
        {
            // Arrange
            var existingTenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Find Test Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(existingTenant);
            await _context.SaveChangesAsync();

            var expectedTenantDto = new TenantDto
            {
                Id = existingTenant.Id,
                Name = existingTenant.Name
            };

            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns(expectedTenantDto);

            // Act
            var result = await _tenantManager.GetByIdAsync(existingTenant.Id);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().BeEquivalentTo(expectedTenantDto);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsFailResponse()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _tenantManager.GetByIdAsync(nonExistentId);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        #endregion

        #region GetByNameAsync Tests

        [Fact]
        public async Task GetByNameAsync_ExistingName_ReturnsSuccessResponse()
        {
            // Arrange
            var existingTenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Search Test Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(existingTenant);
            await _context.SaveChangesAsync();

            var expectedTenantDto = new TenantDto
            {
                Id = existingTenant.Id,
                Name = existingTenant.Name
            };

            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns(expectedTenantDto);

            // Act
            var result = await _tenantManager.GetByNameAsync(existingTenant.Name);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().BeEquivalentTo(expectedTenantDto);
        }

        [Fact]
        public async Task GetByNameAsync_NonExistingName_ReturnsFailResponse()
        {
            // Arrange
            var nonExistentName = "Non-existent Firma";

            // Act
            var result = await _tenantManager.GetByNameAsync(nonExistentName);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        #endregion

        #region GetAllAsync Tests

        [Fact]
        public async Task GetAllAsync_WithData_ReturnsSuccessResponseWithList()
        {
            // Arrange
            var tenant1 = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Firma 1",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            var tenant2 = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Firma 2",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.AddRange(tenant1, tenant2);
            await _context.SaveChangesAsync();

            var tenantDtos = new List<TenantDto>
            {
                new TenantDto { Id = tenant1.Id, Name = tenant1.Name },
                new TenantDto { Id = tenant2.Id, Name = tenant2.Name }
            };

            // Mock Map for List<TenantDto> instead of ProjectTo
            _mockMapper.Setup(m => m.Map<List<TenantDto>>(It.IsAny<List<Tenant>>()))
                      .Returns(tenantDtos);

            // Act
            var result = await _tenantManager.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            result.Result.Should().HaveCount(2);
            result.Result.Should().Contain(t => t.Name == "Firma 1");
            result.Result.Should().Contain(t => t.Name == "Firma 2");
        }

        [Fact]
        public async Task GetAllAsync_EmptyDatabase_ReturnsSuccessResponseWithEmptyList()
        {
            // Arrange
            var emptyTenantDtos = new List<TenantDto>();
            
            // Mock Map for empty List<TenantDto>
            _mockMapper.Setup(m => m.Map<List<TenantDto>>(It.IsAny<List<Tenant>>()))
                      .Returns(emptyTenantDtos);

            // Act
            var result = await _tenantManager.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            result.Result.Should().BeEmpty();
        }

        #endregion

        #region IsAnyTenantExistAsync Tests

        [Fact]
        public async Task IsAnyTenantExistAsync_WithTenants_ReturnsSuccessResponseWithTrue()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Exist Test Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _tenantManager.IsAnyTenantExistAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().BeTrue();
        }

        [Fact]
        public async Task IsAnyTenantExistAsync_WithoutTenants_ReturnsSuccessResponseWithFalse()
        {
            // Arrange
            // Empty database

            // Act
            var result = await _tenantManager.IsAnyTenantExistAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().BeFalse();
        }

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
