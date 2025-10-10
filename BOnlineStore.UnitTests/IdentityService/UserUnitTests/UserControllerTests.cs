using AutoMapper;
using BOnlineStore.IdentityServer.Business.UserService;
using BOnlineStore.IdentityServer.Controllers;
using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.IdentityServer.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BOnlineStore.UnitTests.IdentityService.UserUnitTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object);
        }

        #region CreateUser Tests

        [Fact]
        public async Task CreateUser_ValidUserCreateDto_ReturnsCreatedResult()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Email = "test@example.com",
                Password = "Test123!",
                TenantId = Guid.NewGuid(),
                Name = "Test User"
            };

            var expectedUser = new UserDto
            {
                Id = Guid.NewGuid().ToString(),
                Email = userCreateDto.Email,
                Name = userCreateDto.Name
            };

            var identityResult = IdentityResult.Success;

            _mockUserService
                .Setup(x => x.CreateAsync(It.IsAny<UserCreateDto>()))
                .ReturnsAsync((expectedUser, identityResult));

            // Act
            var result = await _controller.CreateUser(userCreateDto);

            // Assert
            result.Should().NotBeNull();
            var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.Value.Should().BeEquivalentTo(expectedUser);
            createdResult.ActionName.Should().Be(nameof(UserController.GetUserById));
        }

        [Fact]
        public async Task CreateUser_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var userCreateDto = new UserCreateDto(); // Invalid DTO (missing required fields)
            _controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = await _controller.CreateUser(userCreateDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateUser_ServiceReturnsFailure_ReturnsBadRequest()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Email = "test@example.com",
                Password = "Test123!",
                TenantId = Guid.NewGuid()
            };

            var identityError = new IdentityError
            {
                Code = "DuplicateEmail",
                Description = "Email already exists"
            };
            var identityResult = IdentityResult.Failed(identityError);

            _mockUserService
                .Setup(x => x.CreateAsync(It.IsAny<UserCreateDto>()))
                .ReturnsAsync((null, identityResult));

            // Act
            var result = await _controller.CreateUser(userCreateDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        #endregion

        #region UpdateUser Tests

        [Fact]
        public async Task UpdateUser_ValidUserUpdateDto_ReturnsOkResult()
        {
            // Arrange
            var userUpdateDto = new UserUpdateDto
            {
                Id = Guid.NewGuid().ToString(),
                Email = "updated@example.com",
                Name = "Updated User"
            };

            var expectedUser = new UserDto
            {
                Id = userUpdateDto.Id,
                Email = userUpdateDto.Email,
                Name = userUpdateDto.Name
            };

            var identityResult = IdentityResult.Success;

            _mockUserService
                .Setup(x => x.UpdateAsync(It.IsAny<UserUpdateDto>()))
                .ReturnsAsync((expectedUser, identityResult));

            // Act
            var result = await _controller.UpdateUser(userUpdateDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task UpdateUser_ServiceReturnsFailure_ReturnsBadRequest()
        {
            // Arrange
            var userUpdateDto = new UserUpdateDto
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@example.com"
            };

            var identityError = new IdentityError
            {
                Code = "UserNotFound",
                Description = "User not found"
            };
            var identityResult = IdentityResult.Failed(identityError);

            _mockUserService
                .Setup(x => x.UpdateAsync(It.IsAny<UserUpdateDto>()))
                .ReturnsAsync((null, identityResult));

            // Act
            var result = await _controller.UpdateUser(userUpdateDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        #endregion

        #region GetUserById Tests

        [Fact]
        public async Task GetUserById_ExistingUserId_ReturnsOkResult()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var expectedUser = new UserDto
            {
                Id = userId,
                Email = "test@example.com",
                Name = "Test User"
            };

            _mockUserService
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task GetUserById_NonExistingUserId_ReturnsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();

            _mockUserService
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync((UserDto)null);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region GetUserByEmail Tests

        [Fact]
        public async Task GetUserByEmail_ExistingEmail_ReturnsOkResult()
        {
            // Arrange
            var email = "test@example.com";
            var expectedUser = new UserDto
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Name = "Test User"
            };

            _mockUserService
                .Setup(x => x.GetByEmailAsync(email))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _controller.GetUserByEmail(email);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task GetUserByEmail_NonExistingEmail_ReturnsNotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";

            _mockUserService
                .Setup(x => x.GetByEmailAsync(email))
                .ReturnsAsync((UserDto)null);

            // Act
            var result = await _controller.GetUserByEmail(email);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion

        #region DeleteUser Tests

        [Fact]
        public async Task DeleteUser_ExistingUserId_ReturnsNoContent()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var identityResult = IdentityResult.Success;

            _mockUserService
                .Setup(x => x.DeleteAsync(userId))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteUser_ServiceReturnsFailure_ReturnsBadRequest()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var identityError = new IdentityError
            {
                Code = "UserNotFound",
                Description = "User not found"
            };
            var identityResult = IdentityResult.Failed(identityError);

            _mockUserService
                .Setup(x => x.DeleteAsync(userId))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        #endregion

        #region GetUsersByTenantId Tests

        [Fact]
        public async Task GetUsersByTenantId_ValidTenantId_ReturnsOkResult()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var expectedUsers = new List<UserDto>
            {
                new UserDto { Id = "1", Email = "user1@example.com", TenantId = tenantId },
                new UserDto { Id = "2", Email = "user2@example.com", TenantId = tenantId }
            };

            _mockUserService
                .Setup(x => x.GetUsersByTenantIdAsync(tenantId))
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _controller.GetUsersByTenantId(tenantId);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedUsers);
        }

        [Fact]
        public async Task GetUsersByTenantId_NoUsersFound_ReturnsEmptyList()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var expectedUsers = new List<UserDto>();

            _mockUserService
                .Setup(x => x.GetUsersByTenantIdAsync(tenantId))
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _controller.GetUsersByTenantId(tenantId);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedUsers);
        }

        #endregion

        #region GetAllUsers Tests

        [Fact]
        public async Task GetAllUsers_HasUsers_ReturnsOkResult()
        {
            // Arrange
            var expectedUsers = new List<UserDto>
            {
                new UserDto { Id = "1", Email = "user1@example.com" },
                new UserDto { Id = "2", Email = "user2@example.com" }
            };

            _mockUserService
                .Setup(x => x.GetAllUsersAsync())
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedUsers);
        }

        #endregion

        #region ChangePassword Tests

        [Fact]
        public async Task ChangePassword_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                CurrentPassword = "OldPassword123!",
                NewPassword = "NewPassword123!"
            };

            var identityResult = IdentityResult.Success;

            _mockUserService
                .Setup(x => x.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ChangePassword_ServiceReturnsFailure_ReturnsBadRequest()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                CurrentPassword = "WrongPassword",
                NewPassword = "NewPassword123!"
            };

            var identityError = new IdentityError
            {
                Code = "PasswordMismatch",
                Description = "Current password is incorrect"
            };
            var identityResult = IdentityResult.Failed(identityError);

            _mockUserService
                .Setup(x => x.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        #endregion

        #region ResetPassword Tests

        [Fact]
        public async Task ResetPassword_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new ResetPasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                NewPassword = "NewPassword123!"
            };

            var identityResult = IdentityResult.Success;

            _mockUserService
                .Setup(x => x.ResetPasswordAsync(request.UserId, request.NewPassword))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.ResetPassword(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ResetPassword_ServiceReturnsFailure_ReturnsBadRequest()
        {
            // Arrange
            var request = new ResetPasswordRequest
            {
                UserId = "non-existent-user",
                NewPassword = "NewPassword123!"
            };

            var identityError = new IdentityError
            {
                Code = "UserNotFound",
                Description = "User not found"
            };
            var identityResult = IdentityResult.Failed(identityError);

            _mockUserService
                .Setup(x => x.ResetPasswordAsync(request.UserId, request.NewPassword))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.ResetPassword(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        #endregion
    }
}