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
    public class TenantServiceIntegrationTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IStringLocalizer<Language>> _mockStringLocalizer;
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

            _tenantService = new TenantManager(_context, _mapper, _mockStringLocalizer.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidTenant_ReturnsSuccessResponse()
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
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Result.Should().NotBeNull();
            result.Result.Name.Should().Be(tenantCreateDto.Name);
            result.Result.Adress.Should().NotBeNull();
            result.Result.Adress.StateOrCityName.Should().Be("Ýstanbul");
            result.Result.TaxInformation.TaxNumber.Should().Be("1234567890");

            // Verify in database
            var dbTenant = await _context.Tenant.FindAsync(result.Result.Id);
            dbTenant.Should().NotBeNull();
            dbTenant.Name.Should().Be(tenantCreateDto.Name);
        }

        [Fact]
        public async Task CreateAsync_DuplicateName_ReturnsFailResponse()
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
            var result = await _tenantService.CreateAsync(tenant2);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().Message.Should().Contain("Girilen þirket sistemde mevcut");
        }

        [Fact]
        public async Task UpdateAsync_ExistingTenant_ReturnsSuccessResponse()
        {
            // Arrange
            var originalTenant = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Original Name",
                CreateDateTime = DateTime.Now.AddDays(-1),
                UpdateDateTime = DateTime.Now.AddDays(-1)
            };

            var createdResult = await _tenantService.CreateAsync(originalTenant);

            var updateDto = new TenantUpdateDto
            {
                Id = createdResult.Result.Id,
                Name = "Updated Name",
                Adress = new Adress
                {
                    Adress1 = "Updated Address",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ankara"
                }
            };

            // Act
            var result = await _tenantService.UpdateAsync(createdResult.Result.Id, updateDto);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            result.Result.Id.Should().Be(createdResult.Result.Id);
            result.Result.Name.Should().Be("Updated Name");
            result.Result.Adress.StateOrCityName.Should().Be("Ankara");
        }

        [Fact]
        public async Task DeleteAsync_ExistingTenant_ReturnsSuccessResponse()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "To Be Deleted",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            var createdResult = await _tenantService.CreateAsync(tenantCreateDto);

            // Act
            var result = await _tenantService.DeleteAsync(createdResult.Result.Id);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();

            // Verify deletion
            var deletedTenant = await _context.Tenant.FindAsync(createdResult.Result.Id);
            deletedTenant.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingTenant_ReturnsSuccessResponse()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Get By Id Test",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _tenantService.GetByIdAsync(tenant.Id);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            result.Result.Id.Should().Be(tenant.Id);
            result.Result.Name.Should().Be(tenant.Name);
        }

        [Fact]
        public async Task GetByNameAsync_ExistingTenant_ReturnsSuccessResponse()
        {
            // Arrange
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Get By Name Test",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _tenantService.GetByNameAsync(tenant.Name);

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            result.Result.Id.Should().Be(tenant.Id);
            result.Result.Name.Should().Be(tenant.Name);
        }

        [Fact]
        public async Task GetAllAsync_WithData_ReturnsSuccessResponseWithAllTenants()
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
            await _context.SaveChangesAsync();

            // Act
            var result = await _tenantService.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().NotBeNull();
            result.Result.Should().HaveCount(2);
            result.Result.Should().Contain(t => t.Name == "Tenant 1");
            result.Result.Should().Contain(t => t.Name == "Tenant 2");
        }

        [Fact]
        public async Task IsAnyTenantExistAsync_WithTenants_ReturnsSuccessResponseWithTrue()
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
            await _context.SaveChangesAsync();

            // Act
            var result = await _tenantService.IsAnyTenantExistAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().BeTrue();
        }

        [Fact]
        public async Task IsAnyTenantExistAsync_WithoutTenants_ReturnsSuccessResponseWithFalse()
        {
            // Arrange - empty database

            // Act
            var result = await _tenantService.IsAnyTenantExistAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSucceed.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Should().BeFalse();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
