using AutoMapper;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BOnlineStore.UnitTests.IdentityService.TenantUnitTests
{
    public class TenantServiceIntegrationTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly TenantManager _tenantService;

        public TenantServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            
            // Create a real AutoMapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                // Adress mapping
                cfg.CreateMap<Adress, Adress>();
                
                // TaxInformation mapping
                cfg.CreateMap<TaxInformation, TaxInformation>();
                
                cfg.CreateMap<TenantCreateDto, Tenant>()
                   .ForMember(dest => dest.Users, opt => opt.Ignore());
                
                cfg.CreateMap<TenantUpdateDto, Tenant>()
                   .ForMember(dest => dest.CreateDateTime, opt => opt.Ignore())
                   .ForMember(dest => dest.UpdateDateTime, opt => opt.Ignore())
                   .ForMember(dest => dest.Users, opt => opt.Ignore());
                
                cfg.CreateMap<Tenant, TenantDto>()
                   .ForMember(dest => dest.CreateDateTime, opt => opt.MapFrom(src => src.CreateDateTime ?? DateTime.MinValue))
                   .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => src.UpdateDateTime ?? DateTime.MinValue));
                
                cfg.CreateMap<TenantDto, Tenant>()
                   .ForMember(dest => dest.Users, opt => opt.Ignore());
            });
            
            _mapper = mapperConfig.CreateMapper();
            _tenantService = new TenantManager(_context, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ValidTenant_ReturnsCreatedTenant()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Integration Test Firma",
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

            // Act
            var result = await _tenantService.CreateAsync(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(tenantCreateDto.Name);
            result.Adress.Should().NotBeNull();
            result.Adress.StateOrCityName.Should().Be("Ýstanbul");
            result.TaxInformation.TaxNumber.Should().Be("1234567890");

            // Verify in database
            var dbTenant = await _context.Tenant.FindAsync(result.Id);
            dbTenant.Should().NotBeNull();
            dbTenant.Name.Should().Be(tenantCreateDto.Name);
        }

        [Fact]
        public async Task CreateAsync_DuplicateName_ThrowsException()
        {
            // Arrange
            var tenant1 = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Duplicate Name Test",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            var tenant2 = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Duplicate Name Test", // Same name
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            // Act
            await _tenantService.CreateAsync(tenant1);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _tenantService.CreateAsync(tenant2));
            
            exception.Message.Should().Be("Girilen þirket sistemde mevcut");
        }

        [Fact]
        public async Task UpdateAsync_ExistingTenant_ReturnsUpdatedTenant()
        {
            // Arrange
            var originalTenant = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Original Name",
                CreateDateTime = DateTime.Now.AddDays(-1),
                UpdateDateTime = DateTime.Now.AddDays(-1)
            };

            var createdTenant = await _tenantService.CreateAsync(originalTenant);

            var updateDto = new TenantUpdateDto
            {
                Id = createdTenant.Id,
                Name = "Updated Name",
                Adress = new Adress
                {
                    Adress1 = "Updated Address",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ankara"
                }
            };

            // Act
            var result = await _tenantService.UpdateAsync(updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(createdTenant.Id);
            result.Name.Should().Be("Updated Name");
            result.Adress.StateOrCityName.Should().Be("Ankara");
        }

        [Fact]
        public async Task DeleteAsync_ExistingTenant_ReturnsTrue()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "To Be Deleted",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            var createdTenant = await _tenantService.CreateAsync(tenantCreateDto);

            // Act
            var result = await _tenantService.DeleteAsync(createdTenant.Id);

            // Assert
            result.Should().BeTrue();

            // Verify deletion
            var deletedTenant = await _context.Tenant.FindAsync(createdTenant.Id);
            deletedTenant.Should().BeNull();
        }

        [Fact]
        public void FindById_ExistingTenant_ReturnsTenant()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Find By Id Test",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(tenant);
            _context.SaveChanges();

            // Act
            var result = _tenantService.FindById(tenant.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(tenant.Id);
            result.Name.Should().Be(tenant.Name);
        }

        [Fact]
        public void FindByName_ExistingTenant_ReturnsTenant()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Find By Name Test",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(tenant);
            _context.SaveChanges();

            // Act
            var result = _tenantService.FindByName(tenant.Name);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(tenant.Id);
            result.Name.Should().Be(tenant.Name);
        }

        [Fact]
        public void Tenants_WithData_ReturnsAllTenants()
        {
            // Arrange
            var tenant1 = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Tenant 1",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            var tenant2 = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Tenant 2",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.AddRange(tenant1, tenant2);
            _context.SaveChanges();

            // Act
            var result = _tenantService.Tenants().ToList();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(t => t.Name == "Tenant 1");
            result.Should().Contain(t => t.Name == "Tenant 2");
        }

        [Fact]
        public void IsAnyTenantExist_WithTenants_ReturnsTrue()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Exists Test",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(tenant);
            _context.SaveChanges();

            // Act
            var result = _tenantService.IsAnyTenantExist();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsAnyTenantExist_WithoutTenants_ReturnsFalse()
        {
            // Arrange - empty database

            // Act
            var result = _tenantService.IsAnyTenantExist();

            // Assert
            result.Should().BeFalse();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}