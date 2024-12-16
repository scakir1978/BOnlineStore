using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    public class DeliveryAdress : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public string CustomerName { get; private set; }

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

        public DeliveryAdress() : base()
        {
            Code = "";
            Name = "";
            CustomerName= "";
        }

        public DeliveryAdress(Guid tenantId, string id, string code, string name, string customerName, string? countryId,
                              string? regionId, string? cityId, string? countyId, string? districtId, string? adress,
                              string? postalCode, string? cellPhoneNumber, string? telephoneNumber, string? eMail) : base(tenantId, id)
        {
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

        public void UpdateDeliveryAdress(string code, string name, string customerName, string? countryId,
                                      string? regionId, string? cityId, string? countyId, string? districtId, string? adress,
                                      string? postalCode, string? cellPhoneNumber, string? telephoneNumber, string? eMail)
        {
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
