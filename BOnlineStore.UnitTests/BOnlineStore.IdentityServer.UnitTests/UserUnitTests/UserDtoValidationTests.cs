using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.IdentityServer.Models;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace BOnlineStore.IdentityServer.UnitTests.UserUnitTests
{
    public class UserDtoValidationTests
    {
        #region UserCreateDto Validation Tests

        [Fact]
        public void UserCreateDto_ValidData_PassesValidation()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Email = "test@example.com",
                Password = "Test123!",
                TenantId = Guid.NewGuid(),
                Name = "Test User",
                Locale = "tr-TR"
            };

            // Act
            var validationResults = ValidateModel(userCreateDto);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void UserCreateDto_MissingEmail_FailsValidation()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Password = "Test123!",
                TenantId = Guid.NewGuid()
            };

            // Act
            var validationResults = ValidateModel(userCreateDto);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void UserCreateDto_InvalidEmail_FailsValidation()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Email = "invalid-email",
                Password = "Test123!",
                TenantId = Guid.NewGuid()
            };

            // Act
            var validationResults = ValidateModel(userCreateDto);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void UserCreateDto_MissingPassword_FailsValidation()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Email = "test@example.com",
                TenantId = Guid.NewGuid()
            };

            // Act
            var validationResults = ValidateModel(userCreateDto);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Password"));
        }

        [Fact]
        public void UserCreateDto_ShortPassword_FailsValidation()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Email = "test@example.com",
                Password = "123", // Too short
                TenantId = Guid.NewGuid()
            };

            // Act
            var validationResults = ValidateModel(userCreateDto);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Password"));
        }

        [Theory]
        [InlineData("tr-TR")]
        [InlineData("en-US")]
        [InlineData("de-DE")]
        public void UserCreateDto_ValidLocale_PassesValidation(string locale)
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Email = "test@example.com",
                Password = "Test123!",
                TenantId = Guid.NewGuid(),
                Locale = locale
            };

            // Act
            var validationResults = ValidateModel(userCreateDto);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void UserCreateDto_TooLongLocale_FailsValidation()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                Email = "test@example.com",
                Password = "Test123!",
                TenantId = Guid.NewGuid(),
                Locale = "this-is-too-long" // More than 10 characters
            };

            // Act
            var validationResults = ValidateModel(userCreateDto);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Locale"));
        }

        #endregion

        #region UserUpdateDto Validation Tests

        [Fact]
        public void UserUpdateDto_ValidData_PassesValidation()
        {
            // Arrange
            var userUpdateDto = new UserUpdateDto
            {
                Id = Guid.NewGuid().ToString(),
                Email = "updated@example.com",
                Name = "Updated User"
            };

            // Act
            var validationResults = ValidateModel(userUpdateDto);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void UserUpdateDto_MissingId_FailsValidation()
        {
            // Arrange
            var userUpdateDto = new UserUpdateDto
            {
                Email = "updated@example.com",
                Name = "Updated User"
            };

            // Act
            var validationResults = ValidateModel(userUpdateDto);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Id"));
        }

        [Fact]
        public void UserUpdateDto_InvalidEmail_FailsValidation()
        {
            // Arrange
            var userUpdateDto = new UserUpdateDto
            {
                Id = Guid.NewGuid().ToString(),
                Email = "invalid-email",
                Name = "Updated User"
            };

            // Act
            var validationResults = ValidateModel(userUpdateDto);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("Email"));
        }

        #endregion

        #region ChangePasswordRequest Validation Tests

        [Fact]
        public void ChangePasswordRequest_ValidData_PassesValidation()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                CurrentPassword = "CurrentPassword123!",
                NewPassword = "NewPassword123!"
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void ChangePasswordRequest_MissingUserId_FailsValidation()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                CurrentPassword = "CurrentPassword123!",
                NewPassword = "NewPassword123!"
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("UserId"));
        }

        [Fact]
        public void ChangePasswordRequest_MissingCurrentPassword_FailsValidation()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                NewPassword = "NewPassword123!"
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("CurrentPassword"));
        }

        [Fact]
        public void ChangePasswordRequest_MissingNewPassword_FailsValidation()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                CurrentPassword = "CurrentPassword123!"
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("NewPassword"));
        }

        [Fact]
        public void ChangePasswordRequest_ShortNewPassword_FailsValidation()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                CurrentPassword = "CurrentPassword123!",
                NewPassword = "123" // Too short
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("NewPassword"));
        }

        #endregion

        #region ResetPasswordRequest Validation Tests

        [Fact]
        public void ResetPasswordRequest_ValidData_PassesValidation()
        {
            // Arrange
            var request = new ResetPasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                NewPassword = "NewPassword123!"
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void ResetPasswordRequest_MissingUserId_FailsValidation()
        {
            // Arrange
            var request = new ResetPasswordRequest
            {
                NewPassword = "NewPassword123!"
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("UserId"));
        }

        [Fact]
        public void ResetPasswordRequest_MissingNewPassword_FailsValidation()
        {
            // Arrange
            var request = new ResetPasswordRequest
            {
                UserId = Guid.NewGuid().ToString()
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("NewPassword"));
        }

        [Fact]
        public void ResetPasswordRequest_ShortNewPassword_FailsValidation()
        {
            // Arrange
            var request = new ResetPasswordRequest
            {
                UserId = Guid.NewGuid().ToString(),
                NewPassword = "123" // Too short
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            validationResults.Should().ContainSingle(v => v.MemberNames.Contains("NewPassword"));
        }

        #endregion

        #region Helper Methods

        private static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        #endregion
    }
}
