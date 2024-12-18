using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.BFF.Api.Dtos
{
    public class DeliveryAdressDto : EntityDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? CustomerName { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// Ülke Id
        /// </summary>
        public string? CountryId { get; private set; }
        
        /// <summary>
        /// Ülke bilgileri
        /// </summary>
        public CountryDto Country { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// Şehir Id
        /// </summary>
        public string? CityId { get; private set; }

        /// <summary>
        /// Şehir bilgileri
        /// </summary>
        public CityDto City { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// İlçe Id
        /// </summary>
        public string? CountyId { get; private set; }

        /// <summary>
        /// İlçe bilgileri
        /// </summary>
        public CountyDto County { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// Mahalle Id
        /// </summary>
        public string? DistrictId { get; private set; }

        /// <summary>
        /// Mahalle bilgileri
        /// </summary>
        public DistrictDto District { get; set; }

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

        public DeliveryAdressDto(string id, string code, string name, string customerName, string? countryId, CountryDto country,
                                 string? cityId, CityDto city,  string? countyId, CountyDto county, string? districtId, DistrictDto district,
                                 string? adress, string? postalCode, string? cellPhoneNumber, string? telephoneNumber, string? eMail)
        {
            Id = id;
            Code = code;
            Name = name;
            CustomerName = customerName;
            CountryId = countryId;
            Country = country;
            CityId = cityId;
            City = city;
            CountyId = countyId;
            County = county;
            DistrictId = districtId;
            District = district;
            Adress = adress;
            PostalCode = postalCode;
            CellPhoneNumber = cellPhoneNumber;
            TelephoneNumber = telephoneNumber;
            EMail = eMail;
        }
    }
}
