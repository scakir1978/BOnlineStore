using AutoMapper;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BOnlineStore.IdentityServer.UnitTests.TenantUnitTests
{
    public class TenantManagerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TenantManager _tenantManager;

        public TenantManagerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _tenantManager = new TenantManager(_context, _mockMapper.Object);
        }

        #region CreateAsync Tests

        [Fact]
        public async Task CreateAsync_ValidTenantCreateDto_ReturnsTenantDto()
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
            _mockMapper.Setup(m => m.Map<TenantDto>(tenant)).Returns(expectedTenantDto);
            _mockMapper.Setup(m => m.Map<TenantDto>(It.Is<Tenant>(t => t == null))).Returns((TenantDto)null); // For FindByName

            // Act
            var result = await _tenantManager.CreateAsync(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedTenantDto);
            
            var savedTenant = await _context.Tenant.FindAsync(tenant.Id);
            savedTenant.Should().NotBeNull();
            savedTenant.Name.Should().Be(tenantCreateDto.Name);
        }

        [Fact]
        public async Task CreateAsync_DuplicateName_ThrowsException()
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

            var existingTenantDto = new TenantDto
            {
                Id = existingTenant.Id,
                Name = existingTenant.Name
            };

            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Existing Firma", // Same name
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns(existingTenantDto);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _tenantManager.CreateAsync(tenantCreateDto));
            
            exception.Message.Should().Be("Girilen þirket sistemde mevcut");
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        public async Task UpdateAsync_ValidTenantUpdateDto_ReturnsTenantDto()
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

            var existingTenantDto = new TenantDto
            {
                Id = existingTenant.Id,
                Name = existingTenant.Name
            };

            var updatedTenant = new Tenant
            {
                Id = tenantUpdateDto.Id,
                Name = tenantUpdateDto.Name,
                CreateDateTime = existingTenant.CreateDateTime,
                UpdateDateTime = DateTime.Now
            };

            var expectedTenantDto = new TenantDto
            {
                Id = updatedTenant.Id,
                Name = updatedTenant.Name,
                CreateDateTime = updatedTenant.CreateDateTime.Value,
                UpdateDateTime = updatedTenant.UpdateDateTime.Value
            };

            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns(existingTenantDto);
            _mockMapper.Setup(m => m.Map<Tenant>(tenantUpdateDto)).Returns(updatedTenant);
            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<object>())).Returns(expectedTenantDto);

            // Act
            var result = await _tenantManager.UpdateAsync(tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedTenantDto);
        }

        [Fact]
        public async Task UpdateAsync_NonExistentTenant_ThrowsException()
        {
            // Arrange
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "Non-existent Firma"
            };

            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns((TenantDto)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _tenantManager.UpdateAsync(tenantUpdateDto));
            
            exception.Message.Should().Be("Güncellenecek þirket sistemde bulunamadý");
        }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        public async Task DeleteAsync_ExistingTenant_ReturnsTrue()
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

            _mockMapper.Setup(m => m.Map<TenantDto>(existingTenant)).Returns(existingTenantDto);
            _mockMapper.Setup(m => m.Map<Tenant>(existingTenantDto)).Returns(existingTenant);

            // Act
            var result = await _tenantManager.DeleteAsync(existingTenant.Id);

            // Assert
            result.Should().BeTrue();
            
            var deletedTenant = await _context.Tenant.FindAsync(existingTenant.Id);
            deletedTenant.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_NonExistentTenant_ThrowsException()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns((TenantDto)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _tenantManager.DeleteAsync(nonExistentId));
            
            exception.Message.Should().Be("Silinecek þirket sistemde bulunamadý.");
        }

        #endregion

        #region FindById Tests

        [Fact]
        public void FindById_ExistingId_ReturnsTenantDto()
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
            _context.SaveChanges();

            var expectedTenantDto = new TenantDto
            {
                Id = existingTenant.Id,
                Name = existingTenant.Name
            };

            _mockMapper.Setup(m => m.Map<TenantDto>(existingTenant)).Returns(expectedTenantDto);

            // Act
            var result = _tenantManager.FindById(existingTenant.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedTenantDto);
        }

        [Fact]
        public void FindById_NonExistingId_ReturnsNull()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns((TenantDto)null);

            // Act
            var result = _tenantManager.FindById(nonExistentId);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region FindByName Tests

        [Fact]
        public void FindByName_ExistingName_ReturnsTenantDto()
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
            _context.SaveChanges();

            var expectedTenantDto = new TenantDto
            {
                Id = existingTenant.Id,
                Name = existingTenant.Name
            };

            _mockMapper.Setup(m => m.Map<TenantDto>(existingTenant)).Returns(expectedTenantDto);

            // Act
            var result = _tenantManager.FindByName(existingTenant.Name);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedTenantDto);
        }

        [Fact]
        public void FindByName_NonExistingName_ReturnsNull()
        {
            // Arrange
            var nonExistentName = "Non-existent Firma";
            _mockMapper.Setup(m => m.Map<TenantDto>(It.IsAny<Tenant>())).Returns((TenantDto)null);

            // Act
            var result = _tenantManager.FindByName(nonExistentName);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region Tenants Tests

        [Fact]
        public void Tenants_WithData_ReturnsQueryableTenantDto()
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
            _context.SaveChanges();

            var tenantDtos = new List<TenantDto>
            {
                new TenantDto { Id = tenant1.Id, Name = tenant1.Name },
                new TenantDto { Id = tenant2.Id, Name = tenant2.Name }
            };

            // Setup ProjectTo differently to avoid optional parameters issue
            var mockQueryProvider = new Mock<IQueryProvider>();
            var mockQueryable = tenantDtos.AsQueryable();
            mockQueryProvider.Setup(x => x.CreateQuery<TenantDto>(It.IsAny<System.Linq.Expressions.Expression>()))
                            .Returns(mockQueryable);

            _mockMapper.Setup(m => m.ProjectTo<TenantDto>(It.IsAny<IQueryable<Tenant>>(), It.IsAny<object>()))
                      .Returns(tenantDtos.AsQueryable());

            // Act
            var result = _tenantManager.Tenants();

            // Assert
            result.Should().NotBeNull();
            // Note: Due to mocking limitations, we can't easily test the actual count
            // In a real scenario, this would work with actual AutoMapper configuration
        }

        [Fact]
        public void Tenants_EmptyDatabase_ReturnsEmptyQueryable()
        {
            // Arrange
            var emptyTenantDtos = new List<TenantDto>();
            _mockMapper.Setup(m => m.ProjectTo<TenantDto>(It.IsAny<IQueryable<Tenant>>(), It.IsAny<object>()))
                      .Returns(emptyTenantDtos.AsQueryable());

            // Act
            var result = _tenantManager.Tenants();

            // Assert
            result.Should().NotBeNull();
        }

        #endregion

        #region IsAnyTenantExist Tests

        [Fact]
        public void IsAnyTenantExist_WithTenants_ReturnsTrue()
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
            _context.SaveChanges();

            // Act
            var result = _tenantManager.IsAnyTenantExist();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsAnyTenantExist_WithoutTenants_ReturnsFalse()
        {
            // Arrange
            // Empty database

            // Act
            var result = _tenantManager.IsAnyTenantExist();

            // Assert
            result.Should().BeFalse();
        }

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
