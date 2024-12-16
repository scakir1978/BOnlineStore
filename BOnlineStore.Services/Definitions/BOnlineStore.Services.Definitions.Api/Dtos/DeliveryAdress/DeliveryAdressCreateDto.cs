using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class DeliveryAdressCreateDto : EntityDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public string? CustomerName { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// Ülke Id
        /// </summary>
        public string? CountryId { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// Bölge Id
        /// </summary>
        public string? RegionId { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// Şehir Id
        /// </summary>
        public string? CityId { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// İlçe Id
        /// </summary>
        public string? CountyId { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// Mahalle Id
        /// </summary>
        public string? DistrictId { get; private set; }

        /// <summary>
        /// Adres
        /// </summary>
        public string? Adress { get; private set; }

        /// <summary>
        /// Posta Kodu
        /// </summary>
        public string? PostalCode { get; private set; }

        /// <summary>
        /// Cep Telefon Numarası
        /// </summary>
        public string? CellPhoneNumber { get; private set; }

        /// <summary>
        /// Telefon Numarası
        /// </summary>
        public string? TelephoneNumber { get; private set; }

        /// <summary>
        /// E-Mail Adresi
        /// </summary>
        public string? EMail { get; private set; }

        public DeliveryAdressCreateDto(string code, string name, string customerName, string? countryId,
                                       string? regionId, string? cityId, string? countyId, string? districtId,
                                       string? adress, string? postalCode, string? cellPhoneNumber,
                                       string? telephoneNumber, string? eMail)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            CustomerName = customerName;
            CountryId = countryId;
            RegionId = regionId;
            CityId = cityId;
            CountyId = countyId;
            DistrictId = districtId;
            Adress = adress;
            PostalCode = postalCode;
            CellPhoneNumber = cellPhoneNumber;
            TelephoneNumber = telephoneNumber;
            EMail = eMail;
        }
    }
}
