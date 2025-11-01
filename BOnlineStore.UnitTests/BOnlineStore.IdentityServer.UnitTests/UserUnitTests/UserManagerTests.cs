using AutoMapper;
using BOnlineStore.IdentityServer.Business.UserService;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Shared.Constansts;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using System.Security.Claims;
using Xunit;

namespace BOnlineStore.IdentityServer.UnitTests.UserUnitTests
{
    public class UserManagerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IStringLocalizer<Language>> _mockStringLocalizer;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly UserManager _userManager;
        private readonly Guid _testTenantId = Guid.NewGuid();

        public UserManagerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _mockStringLocalizer = new Mock<IStringLocalizer<Language>>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            // Mock UserManager
            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            // Setup HttpContext with TenantId claim
            var claims = new List<Claim>
            {
                new Claim(GlobalConstants.tenantId, _testTenantId.ToString())
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            // Setup default localizer behavior
            _mockStringLocalizer
                .Setup(x => x[It.IsAny<string>()])
                .Returns((string key) => new LocalizedString(key, key));

            // Setup specific localization keys
            SetupLocalizationKeys();

            _userManager = new UserManager(
                _mockUserManager.Object, 
                _context, 
                _mockMapper.Object, 
                _mockStringLocalizer.Object,
                _mockHttpContextAccessor.Object);
        }

        private void SetupLocalizationKeys()
        {
            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.TenantNotFound])
                .Returns(new LocalizedString(IdentityServerKeys.TenantNotFound, "Kiracý bulunamadý"));

            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.UserNotFound])
                .Returns(new LocalizedString(IdentityServerKeys.UserNotFound, "Kullanýcý bulunamadý"));

            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.CreateUserError])
                .Returns(new LocalizedString(IdentityServerKeys.CreateUserError, "Kullanýcý oluþturulurken hata oluþtu: {0}"));

            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.UpdateUserError])
                .Returns(new LocalizedString(IdentityServerKeys.UpdateUserError, "Kullanýcý güncellenirken hata oluþtu: {0}"));

            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.DeleteUserError])
                .Returns(new LocalizedString(IdentityServerKeys.DeleteUserError, "Kullanýcý silinirken hata oluþtu: {0}"));

            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.ChangePasswordError])
                .Returns(new LocalizedString(IdentityServerKeys.ChangePasswordError, "Þifre deðiþtirilirken hata oluþtu: {0}"));

            _mockStringLocalizer
                .Setup(x => x[IdentityServerKeys.ResetPasswordError])
                .Returns(new LocalizedString(IdentityServerKeys.ResetPasswordError, "Þifre sýfýrlanýrken hata oluþtu: {0}"));
        }

        #region CreateAsync Tests

        [Fact]
        public async Task CreateAsync_ValidUserCreateDto_ReturnsUserDtoAndSuccess()
        {
            // Arrange
            var tenant = new Tenant { Id = _testTenantId, Name = "Test Tenant" };
            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

            var userCreateDto = new UserCreateDto
            {
                Email = "test@example.com",
                Password = "Test123!",
                Name = "Test User"
                // TenantId HttpContext'ten otomatik alýnacak
            };

            var applicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = userCreateDto.Email,
                UserName = userCreateDto.Email,
                TenantId = _testTenantId,
                Name = userCreateDto.Name
            };

            var expectedUserDto = new UserDto
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Name = applicationUser.Name,
                TenantId = _testTenantId
            };

            _mockMapper.Setup(m => m.Map<ApplicationUser>(userCreateDto)).Returns(applicationUser);
            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<ApplicationUser>())).Returns(expectedUserDto);
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var response = await _userManager.CreateAsync(userCreateDto);

            // Assert
            response.Should().NotBeNull();
            response.IsSucceed.Should().BeTrue();
            response.Result.Should().NotBeNull();
            response.Result.Email.Should().Be(userCreateDto.Email);
            response.Result.Name.Should().Be(userCreateDto.Name);
            response.Result.TenantId.Should().Be(_testTenantId);
        }

        [Fact]
        public async Task CreateAsync_NonExistentTenant_ReturnsFailure()
        {
            // Arrange
         // Tenant eklenmediði için baþarýsýz olmalý
            var userCreateDto = new UserCreateDto
            {
       Email = "test@example.com",
             Password = "Test123!",
      Name = "Test User"
            };

 // Act
            var response = await _userManager.CreateAsync(userCreateDto);

    // Assert
   response.Should().NotBeNull();
            response.IsSucceed.Should().BeFalse();
        response.Result.Should().BeNull();
            response.Errors.Should().ContainSingle(e => e.ErrorCode == "TenantNotFound");
            response.Errors.First().Message.Should().Contain("Kiracý bulunamadý");
     }

        [Fact]
      public async Task CreateAsync_UserManagerFailure_ReturnsFailure()
        {
      // Arrange
     var tenant = new Tenant { Id = _testTenantId, Name = "Test Tenant" };
       _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();

         var userCreateDto = new UserCreateDto
            {
      Email = "test@example.com",
    Password = "Test123!",
    };

   var applicationUser = new ApplicationUser
    {
    Email = userCreateDto.Email,
        UserName = userCreateDto.Email,
   TenantId = _testTenantId
            };

            var identityError = new IdentityError
       {
 Code = "DuplicateUserName",
   Description = "User name already exists"
        };

    _mockMapper.Setup(m => m.Map<ApplicationUser>(userCreateDto)).Returns(applicationUser);
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
    .ReturnsAsync(IdentityResult.Failed(identityError));

            // Act
       var response = await _userManager.CreateAsync(userCreateDto);

      // Assert
            response.Should().NotBeNull();
            response.IsSucceed.Should().BeFalse();
     response.Result.Should().BeNull();
            response.Errors.Should().Contain(e => e.ErrorCode == "DuplicateUserName");
        }

    #endregion

        #region UpdateAsync Tests

        [Fact]
        public async Task UpdateAsync_ValidUserUpdateDto_ReturnsUserDtoAndSuccess()
        {
        // Arrange
         var userId = Guid.NewGuid().ToString();
         var existingUser = new ApplicationUser
   {
     Id = userId,
Email = "old@example.com",
    UserName = "old@example.com",
      TenantId = Guid.NewGuid(),
    Name = "Old Name"
   };

        var userUpdateDto = new UserUpdateDto
   {
     Id = userId,
       Email = "new@example.com",
   Name = "New Name"
    };

       var expectedUserDto = new UserDto
       {
     Id = userId,
Email = userUpdateDto.Email,
   Name = userUpdateDto.Name
       };

_mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(existingUser);
  _mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>()))
 .ReturnsAsync(IdentityResult.Success);
    _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<ApplicationUser>())).Returns(expectedUserDto);

        // Act
     var response = await _userManager.UpdateAsync(userId, userUpdateDto);

       // Assert
       response.Should().NotBeNull();
response.IsSucceed.Should().BeTrue();
    response.Result.Should().NotBeNull();
     response.Result.Email.Should().Be(userUpdateDto.Email);
      response.Result.Name.Should().Be(userUpdateDto.Name);
}

 [Fact]
        public async Task UpdateAsync_NonExistentUser_ReturnsFailure()
     {
       // Arrange
      var userId = Guid.NewGuid().ToString();
    var userUpdateDto = new UserUpdateDto
      {
       Id = userId,
      Email = "test@example.com"
            };

   _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
     .ReturnsAsync((ApplicationUser)null);

 // Act
    var response = await _userManager.UpdateAsync(userId, userUpdateDto);

     // Assert
       response.Should().NotBeNull();
     response.IsSucceed.Should().BeFalse();
response.Result.Should().BeNull();
     response.Errors.Should().ContainSingle(e => e.ErrorCode == "UserNotFound");
   response.Errors.First().Message.Should().Contain("Kullanýcý bulunamadý");
 }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        public async Task DeleteAsync_ExistingUser_ReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var existingUser = new ApplicationUser
            {
                Id = userId,
                Email = "test@example.com",
                UserName = "test@example.com",
                TenantId = Guid.NewGuid()
            };

            var expectedUserDto = new UserDto
            {
                Id = userId,
                Email = existingUser.Email,
                UserName = existingUser.UserName,
                TenantId = existingUser.TenantId
            };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockMapper.Setup(m => m.Map<UserDto>(existingUser)).Returns(expectedUserDto);
            _mockUserManager.Setup(um => um.DeleteAsync(existingUser))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var response = await _userManager.DeleteAsync(userId);

            // Assert
            response.Should().NotBeNull();
            response.IsSucceed.Should().BeTrue();
            response.Result.Should().NotBeNull();
            response.Result.Id.Should().Be(userId);
        }

        [Fact]
        public async Task DeleteAsync_NonExistentUser_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _mockUserManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var response = await _userManager.DeleteAsync(userId);

            // Assert
            response.Should().NotBeNull();
            response.IsSucceed.Should().BeFalse();
            response.Result.Should().BeNull();
            response.Errors.Should().ContainSingle(e => e.ErrorCode == "UserNotFound");
            response.Errors.First().Message.Should().Contain("Kullanýcý bulunamadý");
        }

        #endregion

        #region GetByIdAsync Tests

        [Fact]
        public async Task GetByIdAsync_ExistingUser_ReturnsUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var existingUser = new ApplicationUser
            {
                Id = userId,
                Email = "test@example.com",
                UserName = "test@example.com",
                TenantId = Guid.NewGuid(),
                Name = "Test User"
            };

            var expectedUserDto = new UserDto
            {
                Id = userId,
                Email = existingUser.Email,
                Name = existingUser.Name
            };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockMapper.Setup(m => m.Map<UserDto>(existingUser)).Returns(expectedUserDto);

            // Act
            var result = await _userManager.GetByIdAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
            result.Email.Should().Be(existingUser.Email);
            result.Name.Should().Be(existingUser.Name);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistentUser_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _mockUserManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync((ApplicationUser)null);
            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<ApplicationUser>())).Returns((UserDto)null);

            // Act
            var result = await _userManager.GetByIdAsync(userId);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region GetByEmailAsync Tests

        [Fact]
        public async Task GetByEmailAsync_ExistingUser_ReturnsUserDto()
        {
            // Arrange
            var email = "test@example.com";
            var existingUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email,
                TenantId = Guid.NewGuid(),
                Name = "Test User"
            };

            var expectedUserDto = new UserDto
            {
                Id = existingUser.Id,
                Email = email,
                Name = existingUser.Name
            };

            _mockUserManager.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(existingUser);
            _mockMapper.Setup(m => m.Map<UserDto>(existingUser)).Returns(expectedUserDto);

            // Act
            var result = await _userManager.GetByEmailAsync(email);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(email);
            result.Name.Should().Be(existingUser.Name);
        }

        [Fact]
        public async Task GetByEmailAsync_NonExistentUser_ReturnsNull()
        {
            // Arrange
            var email = "nonexistent@example.com";
            _mockUserManager.Setup(um => um.FindByEmailAsync(email))
                .ReturnsAsync((ApplicationUser)null);
            _mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<ApplicationUser>())).Returns((UserDto)null);

            // Act
            var result = await _userManager.GetByEmailAsync(email);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region ChangePasswordAsync Tests

        [Fact]
        public async Task ChangePasswordAsync_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var currentPassword = "OldPassword123!";
            var newPassword = "NewPassword123!";

            var existingUser = new ApplicationUser
            {
                Id = userId,
                Email = "test@example.com",
                UserName = "test@example.com",
                TenantId = Guid.NewGuid()
            };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockUserManager.Setup(um => um.ChangePasswordAsync(existingUser, currentPassword, newPassword))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userManager.ChangePasswordAsync(userId, currentPassword, newPassword);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task ChangePasswordAsync_NonExistentUser_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _mockUserManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _userManager.ChangePasswordAsync(userId, "OldPass123!", "NewPass123!");

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Code == "UserNotFound");
            result.Errors.First().Description.Should().Contain("Kullanýcý bulunamadý");
        }

        #endregion

        #region ResetPasswordAsync Tests

        [Fact]
        public async Task ResetPasswordAsync_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var newPassword = "NewPassword123!";
            var resetToken = "reset-token";

            var existingUser = new ApplicationUser
            {
                Id = userId,
                Email = "test@example.com",
                UserName = "test@example.com",
                TenantId = Guid.NewGuid()
            };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockUserManager.Setup(um => um.GeneratePasswordResetTokenAsync(existingUser))
                .ReturnsAsync(resetToken);
            _mockUserManager.Setup(um => um.ResetPasswordAsync(existingUser, resetToken, newPassword))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userManager.ResetPasswordAsync(userId, newPassword);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task ResetPasswordAsync_NonExistentUser_ReturnsFailure()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _mockUserManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _userManager.ResetPasswordAsync(userId, "NewPass123!");

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Code == "UserNotFound");
            result.Errors.First().Description.Should().Contain("Kullanýcý bulunamadý");
        }

        #endregion

        #region GetUsersByTenantIdAsync Tests

        [Fact]
        public async Task GetUsersByTenantIdAsync_ExistingTenant_ReturnsUserList()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", Email = "user1@example.com", TenantId = tenantId },
                new ApplicationUser { Id = "2", Email = "user2@example.com", TenantId = tenantId }
            };

            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();

            var expectedUserDtos = new List<UserDto>
            {
                new UserDto { Id = "1", Email = "user1@example.com", TenantId = tenantId },
                new UserDto { Id = "2", Email = "user2@example.com", TenantId = tenantId }
            };

            _mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<ApplicationUser>>()))
                .Returns(expectedUserDtos);

            // Act
            var result = await _userManager.GetUsersByTenantIdAsync(tenantId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUsersByTenantIdAsync_NonExistentTenant_ReturnsEmptyList()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            _mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<ApplicationUser>>()))
                .Returns(new List<UserDto>());

            // Act
            var result = await _userManager.GetUsersByTenantIdAsync(tenantId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        #endregion

        #region GetAllUsersAsync Tests

        [Fact]
        public async Task GetAllUsersAsync_WithUsers_ReturnsUserList()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", Email = "user1@example.com", TenantId = Guid.NewGuid() },
                new ApplicationUser { Id = "2", Email = "user2@example.com", TenantId = Guid.NewGuid() }
            };

            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();

            var expectedUserDtos = new List<UserDto>
            {
                new UserDto { Id = "1", Email = "user1@example.com" },
                new UserDto { Id = "2", Email = "user2@example.com" }
            };

            _mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<ApplicationUser>>()))
                .Returns(expectedUserDtos);

            // Act
            var result = await _userManager.GetAllUsersAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllUsersAsync_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            _mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<ApplicationUser>>()))
                .Returns(new List<UserDto>());

            // Act
            var result = await _userManager.GetAllUsersAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
