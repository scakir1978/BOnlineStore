using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Controllers;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace BOnlineStore.UnitTests.IdentityService.TenantUnitTests
{
    public class TenantControllerIntegrationTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly TenantController _controller;
        private readonly ITenantService _tenantService;

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

            _tenantService = new TenantManager(_context, mapper);
            _controller = new TenantController(_tenantService);
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
        public async Task GetAllTenants_WithData_ReturnsOkWithTenantList()
        {
            // Arrange
            await SeedTestDataAsync();

            // Act
            var result = _controller.GetAllTenants();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>().Subject;
            var tenants = okResult.Value as List<TenantDto>;

            tenants.Should().NotBeNull();
            tenants.Should().HaveCount(2);
            tenants.Should().Contain(t => t.Name == "Test Firma 1");
            tenants.Should().Contain(t => t.Name == "Test Firma 2");
        }

        [Fact]
        public void GetTenantById_ExistingId_ReturnsOkWithTenant()
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
            _context.SaveChanges();

            // Act
            var result = _controller.GetTenantById(tenant.Id);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>().Subject;
            var returnedTenant = okResult.Value as TenantDto;

            returnedTenant.Should().NotBeNull();
            returnedTenant.Id.Should().Be(tenant.Id);
            returnedTenant.Name.Should().Be(tenant.Name);
        }

        [Fact]
        public void GetTenantById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = _controller.GetTenantById(nonExistingId);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.NotFoundObjectResult>();
        }

        [Fact]
        public void GetTenantByName_ExistingName_ReturnsOkWithTenant()
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
            _context.SaveChanges();

            // Act
            var result = _controller.GetTenantByName(tenant.Name);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>().Subject;
            var returnedTenant = okResult.Value as TenantDto;

            returnedTenant.Should().NotBeNull();
            returnedTenant.Name.Should().Be(tenant.Name);
        }

        [Fact]
        public async Task CreateTenant_ValidData_ReturnsCreatedWithTenant()
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
            var result = await _controller.CreateTenant(tenantCreateDto);

            // Assert
            result.Should().NotBeNull();
            var createdResult = result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.CreatedAtActionResult>().Subject;
            var createdTenant = createdResult.Value as TenantDto;

            createdTenant.Should().NotBeNull();
            createdTenant.Name.Should().Be(tenantCreateDto.Name);
            createdTenant.Adress.StateOrCityName.Should().Be("Ýzmir");
            createdTenant.TaxInformation.TaxNumber.Should().Be("1111111111");

            // Verify action and route values
            createdResult.ActionName.Should().Be(nameof(TenantController.GetTenantById));
            createdResult.RouteValues["id"].Should().Be(createdTenant.Id);
        }

        [Fact]
        public async Task CreateTenant_DuplicateName_ThrowsException()
        {
            // Arrange
            var existingTenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Duplicate Test Firma Integration",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };
            _context.Tenant.Add(existingTenant);
            await _context.SaveChangesAsync();

            var duplicateTenantDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = existingTenant.Name, // Duplicate name
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            // Act & Assert
            var result = await _controller.CreateTenant(duplicateTenantDto);
            result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateTenant_ValidData_ReturnsOkWithUpdatedTenant()
        {
            // Arrange
            var existingTenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Original Update Firma Integration",
                CreateDateTime = DateTime.Now.AddDays(-5),
                UpdateDateTime = DateTime.Now.AddDays(-1)
            };
            _context.Tenant.Add(existingTenant);
            await _context.SaveChangesAsync();

            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = existingTenant.Id,
                Name = "Güncellenmiþ Firma Adý Integration",
                Adress = new Adress
                {
                    Adress1 = "Güncellenmiþ Adres",
                    CountryName = "Türkiye",
                    StateOrCityName = "Bursa",
                    CityOrCountyName = "Nilüfer",
                    PostalCode = 16000
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "2222222222",
                    TaxAdministration = "Nilüfer Vergi Dairesi"
                }
            };

            // Act
            var result = await _controller.UpdateTenant(existingTenant.Id, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>().Subject;
            var updatedTenant = okResult.Value as TenantDto;

            updatedTenant.Should().NotBeNull();
            updatedTenant.Id.Should().Be(existingTenant.Id);
            updatedTenant.Name.Should().Be("Güncellenmiþ Firma Adý Integration");
            updatedTenant.Adress.StateOrCityName.Should().Be("Bursa");
            updatedTenant.TaxInformation.TaxNumber.Should().Be("2222222222");
        }

        [Fact]
        public async Task UpdateTenant_MismatchedIds_ReturnsBadRequest()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var differentId = Guid.NewGuid();

            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = differentId, // Different from URL
                Name = "Test Firma"
            };

            // Act
            var result = await _controller.UpdateTenant(existingId, tenantUpdateDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteTenant_ExistingTenant_ReturnsNoContent()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "To Delete Firma Integration",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };
            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteTenant(tenant.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Microsoft.AspNetCore.Mvc.NoContentResult>();

            // Verify tenant is actually deleted
            var deletedTenant = await _context.Tenant.FindAsync(tenant.Id);
            deletedTenant.Should().BeNull();
        }

        [Fact]
        public async Task DeleteTenant_NonExistingTenant_ReturnsBadRequest()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteTenant(nonExistingId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>();
        }

        [Fact]
        public async Task IsAnyTenantExist_WithTenants_ReturnsOkWithTrue()
        {
            // Arrange
            await SeedTestDataAsync();

            // Act
            var result = _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>().Subject;
            var exists = (bool)okResult.Value;
            exists.Should().BeTrue();
        }

        [Fact]
        public void IsAnyTenantExist_WithoutTenants_ReturnsOkWithFalse()
        {
            // Arrange - empty database

            // Act
            var result = _controller.IsAnyTenantExist();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>().Subject;
            var exists = (bool)okResult.Value;
            exists.Should().BeFalse();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}