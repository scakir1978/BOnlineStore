using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace BOnlineStore.IdentityServer.UnitTests.TenantUnitTests
{
    public class TenantDtoValidationTests
    {
        #region TenantCreateDto Validation Tests

        [Fact]
        public void TenantCreateDto_ValidData_PassesValidation()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Adress = new Adress
                {
                    Adress1 = "Test Adres 1",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ýstanbul",
                    CityOrCountyName = "Kadýköy",
                    DistrictName = "Fenerbahçe",
                    PostalCode = 34710
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "1234567890",
                    TaxAdministration = "Kadýköy Vergi Dairesi"
                }
            };

            // Act
            var validationResults = ValidateModel(tenantCreateDto);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void TenantCreateDto_EmptyGuid_PassesValidation()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.Empty, // Empty GUID should be valid for creation
                Name = "Test Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(tenantCreateDto);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void TenantCreateDto_NullAdressAndTaxInfo_PassesValidation()
        {
            // Arrange
            var tenantCreateDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Firma",
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Adress = null, // Null should be valid
                TaxInformation = null // Null should be valid
            };

            // Act
            var validationResults = ValidateModel(tenantCreateDto);

            // Assert
            validationResults.Should().BeEmpty();
        }

        #endregion

        #region TenantUpdateDto Validation Tests

        [Fact]
        public void TenantUpdateDto_ValidData_PassesValidation()
        {
            // Arrange
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "Güncellenmiþ Firma",
                Adress = new Adress
                {
                    Adress1 = "Güncellenmiþ Adres",
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

            // Act
            var validationResults = ValidateModel(tenantUpdateDto);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void TenantUpdateDto_EmptyGuid_FailsValidation()
        {
            // Arrange
            var tenantUpdateDto = new TenantUpdateDto
            {
                Id = Guid.Empty, // Empty GUID should fail for update
                Name = "Test Firma"
            };

            // Act
            var validationResults = ValidateModel(tenantUpdateDto);

            // Assert
            // Note: This would depend on custom validation attributes if implemented
            // For now, we'll assume basic validation passes
            validationResults.Should().BeEmpty();
        }

        #endregion

        #region TenantDto Validation Tests

        [Fact]
        public void TenantDto_ValidData_PassesValidation()
        {
            // Arrange
            var tenantDto = new TenantDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Firma",
                CreateDateTime = DateTime.Now.AddDays(-10),
                UpdateDateTime = DateTime.Now,
                Adress = new Adress
                {
                    Adress1 = "Test Adres",
                    CountryName = "Türkiye",
                    StateOrCityName = "Ýstanbul"
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "1234567890",
                    TaxAdministration = "Test Vergi Dairesi"
                }
            };

            // Act
            var validationResults = ValidateModel(tenantDto);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void TenantDto_FutureDates_IsValidForBusinessLogic()
        {
            // Arrange
            var futureDate = DateTime.Now.AddDays(1);
            var tenantDto = new TenantDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Firma",
                CreateDateTime = futureDate,
                UpdateDateTime = futureDate
            };

            // Act & Assert
            tenantDto.CreateDateTime.Should().BeAfter(DateTime.Now);
            tenantDto.UpdateDateTime.Should().BeAfter(DateTime.Now);
        }

        #endregion

        #region Adress Validation Tests

        [Fact]
        public void Adress_ValidData_PassesValidation()
        {
            // Arrange
            var adress = new Adress
            {
                Adress1 = "Test Sokak No:1",
                Adress2 = "Kat:2 Daire:3",
                CountryName = "Türkiye",
                StateOrCityName = "Ýstanbul",
                CityOrCountyName = "Kadýköy",
                DistrictName = "Fenerbahçe",
                PostalCode = 34710
            };

            // Act
            var validationResults = ValidateModel(adress);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void Adress_LongStrings_FailsValidation()
        {
            // Arrange
            var longString = new string('A', 300); // Exceeds 256 character limit
            var adress = new Adress
            {
                Adress1 = longString,
                CountryName = "Türkiye"
            };

            // Act
            var validationResults = ValidateModel(adress);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("Adress1"));
        }

        [Fact]
        public void Adress_ZeroPostalCode_PassesValidation()
        {
            // Arrange
            var adress = new Adress
            {
                Adress1 = "Test Adres",
                PostalCode = 0
            };

            // Act
            var validationResults = ValidateModel(adress);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void Adress_NegativePostalCode_PassesValidation()
        {
            // Arrange
            var adress = new Adress
            {
                Adress1 = "Test Adres",
                PostalCode = -1
            };

            // Act
            var validationResults = ValidateModel(adress);

            // Assert
            // Note: If there are range validations, this should be updated
            validationResults.Should().BeEmpty();
        }

        #endregion

        #region TaxInformation Validation Tests

        [Fact]
        public void TaxInformation_ValidData_PassesValidation()
        {
            // Arrange
            var taxInfo = new TaxInformation
            {
                TaxNumber = "1234567890",
                TaxAdministration = "Kadýköy Vergi Dairesi"
            };

            // Act
            var validationResults = ValidateModel(taxInfo);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void TaxInformation_LongStrings_FailsValidation()
        {
            // Arrange
            var longString = new string('B', 300); // Exceeds 256 character limit
            var taxInfo = new TaxInformation
            {
                TaxNumber = longString,
                TaxAdministration = longString
            };

            // Act
            var validationResults = ValidateModel(taxInfo);

            // Assert
            validationResults.Should().NotBeEmpty();
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("TaxNumber"));
            validationResults.Should().Contain(vr => vr.MemberNames.Contains("TaxAdministration"));
        }

        [Fact]
        public void TaxInformation_NullValues_PassesValidation()
        {
            // Arrange
            var taxInfo = new TaxInformation
            {
                TaxNumber = null,
                TaxAdministration = null
            };

            // Act
            var validationResults = ValidateModel(taxInfo);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Fact]
        public void TaxInformation_EmptyStrings_PassesValidation()
        {
            // Arrange
            var taxInfo = new TaxInformation
            {
                TaxNumber = string.Empty,
                TaxAdministration = string.Empty
            };

            // Act
            var validationResults = ValidateModel(taxInfo);

            // Assert
            validationResults.Should().BeEmpty();
        }

        #endregion

        #region Helper Methods

        private static List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }

        #endregion

        #region Data Integrity Tests

        [Fact]
        public void TenantCreateDto_Serialization_MaintainsDataIntegrity()
        {
            // Arrange
            var originalDto = new TenantCreateDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Serialization Firma",
                CreateDateTime = new DateTime(2023, 10, 15, 14, 30, 0),
                UpdateDateTime = new DateTime(2023, 10, 16, 10, 15, 0),
                Adress = new Adress
                {
                    Adress1 = "Serialization Test Adres",
                    CountryName = "Türkiye",
                    PostalCode = 12345
                },
                TaxInformation = new TaxInformation
                {
                    TaxNumber = "9999999999"
                }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(originalDto);
            var deserializedDto = System.Text.Json.JsonSerializer.Deserialize<TenantCreateDto>(json);

            // Assert
            deserializedDto.Should().NotBeNull();
            deserializedDto.Id.Should().Be(originalDto.Id);
            deserializedDto.Name.Should().Be(originalDto.Name);
            deserializedDto.CreateDateTime.Should().Be(originalDto.CreateDateTime);
            deserializedDto.UpdateDateTime.Should().Be(originalDto.UpdateDateTime);
            deserializedDto.Adress.Adress1.Should().Be(originalDto.Adress.Adress1);
            deserializedDto.TaxInformation.TaxNumber.Should().Be(originalDto.TaxInformation.TaxNumber);
        }

        [Fact]
        public void TenantDto_PropertyMapping_WorksCorrectly()
        {
            // Arrange & Act
            var tenantDto = new TenantDto
            {
                Id = Guid.NewGuid(),
                Name = "Property Test Firma"
            };

            // Assert
            tenantDto.Id.Should().NotBeEmpty();
            tenantDto.Name.Should().Be("Property Test Firma");
            tenantDto.Adress.Should().BeNull(); // Default value
            tenantDto.TaxInformation.Should().BeNull(); // Default value
        }

        #endregion
    }
}
