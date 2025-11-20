using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Controllers;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace BOnlineStore.IdentityServer.UnitTests.TenantUnitTests
{
    public class TenantControllerIntegrationTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly TenantController _controller;
        private readonly ITenantService _tenantService;
        private readonly Mock<IStringLocalizer<Language>> _mockStringLocalizer;

        public TenantControllerIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            
            // Create a real mapper configuration with proper mappings
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
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
            var mapper = mapperConfig.CreateMapper();

            _mockStringLocalizer = new Mock<IStringLocalizer<Language>>();
            
            // Setup default localizer behavior
            _mockStringLocalizer
                .Setup(x => x[It.IsAny<string>()])
                .Returns((string key) => new LocalizedString(key, key));

            _tenantService = new TenantManager(_context, mapper, _mockStringLocalizer.Object);
            _controller = new TenantController(_tenantService, _mockStringLocalizer.Object);
        }

        private async Task SeedTestDataAsync()
        {
            var tenant1 = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Test Firma 1",
                CreateDateTime = DateTime.Now.AddDays(-10),
                UpdateDateTime = DateTime.Now.AddDays(-5),
                Adress = new Adress
                {
                    Adress1 = "Test Adres 1",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ýstanbul",
                    CityOrCountyName = "Kadýköy",
                    PostalCode = 34710
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "1234567890",
                    TaxAdministration = "Kadýköy Vergi Dairesi"
                }
            };

            var tenant2 = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Test Firma 2",
                CreateDateTime = DateTime.Now.AddDays(-20),
                UpdateDateTime = DateTime.Now.AddDays(-1),
                Adress = new Adress
                {
                    Adress1 = "Test Adres 2",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ankara",
                    CityOrCountyName = "Çankaya",
                    PostalCode = 06100
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "9876543210",
                    TaxAdministration = "Çankaya Vergi Dairesi"
                }
            };

            _context.Tenant.AddRange(tenant1, tenant2);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllAsync_WithData_ReturnsOkWithTenantList()
        {
            // Arrange
            await SeedTestDataAsync();

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsOkWithTenant()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Test Find Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };
            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetByIdAsync(tenant.Id);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsError()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = await _controller.GetByIdAsync(nonExistingId);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetByNameAsync_ExistingName_ReturnsOkWithTenant()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Test Name Search Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };
            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetByNameAsync(tenant.Name);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CreateAsync_ValidData_ReturnsCreatedWithTenant()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Yeni Test Firma Integration",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Adress = new Adress
                {
                    Adress1 = "Yeni Test Adres",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ýzmir",
                    CityOrCountyName = "Konak",
                    PostalCode = 35000
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "1111111111",
                    TaxAdministration = "Konak Vergi Dairesi"
                }
            };

            // Act
            var result = await _controller.CreateAsync(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            okResult.StatusCode.Should().BeOneOf(200, 201);
        }

        [Fact]
        public async Task CreateAsync_DuplicateName_ReturnsError()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Duplicate Test Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };
            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            var duplicateTenantDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Duplicate Test Firma", // Same name
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            // Act
            var result = await _controller.CreateAsync(duplicateTenantDto);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdateAsync_ValidData_ReturnsOkWithUpdatedTenant()
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
                Name = "Updated Firma",
                Adress = new Adress
                {
                    Adress1 = "Updated Adres",
                    CountryName = "Türkiye",
                    StateOrCityName = "Bursa"
                }
            };

            // Act
            var result = await _controller.UpdateAsync(existingTenant.Id, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingTenant_ReturnsError()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = existingId,
                Name = "Non-existing Firma"
            };

            // Act
            var result = await _controller.UpdateAsync(existingId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task DeleteAsync_ExistingTenant_ReturnsOk()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "To Delete Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };
            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteAsync(tenant.Id);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingTenant_ReturnsError()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteAsync(nonExistingId);

            // Assert
            result.Should().NotBeNull();
            var statusResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            statusResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task IsAnyTenantExist_WithTenants_ReturnsOkWithTrue()
        {
            // Arrange
            await SeedTestDataAsync();

            // Act
            var result = await _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            
            // Extract the Response<bool> from the result
            var response = okResult.Value as BOnlineStore.Shared.Dtos.Response<bool>;
            response.Should().NotBeNull();
            response.Result.Should().BeTrue();
        }

        [Fact]
        public async Task IsAnyTenantExist_WithoutTenants_ReturnsOkWithFalse()
        {
            // Arrange - empty database

            // Act
            var result = await _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            
            // Extract the Response<bool> from the result
            var response = okResult.Value as BOnlineStore.Shared.Dtos.Response<bool>;
            response.Should().NotBeNull();
            response.Result.Should().BeFalse();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
