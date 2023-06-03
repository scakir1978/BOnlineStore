using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Şehir
    /// </summary>
    public class City : Entity
    {
        /// <summary>
        /// Şehir/İl Kodu
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Şehir/İl Adı
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Ülke Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CountryId { get; private set; }

        /// <summary>
        /// Bölge Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RegionId { get; private set; }

        public City() : base()
        {
            Code = "";
            Name = "";
        }

        public City(Guid tenantId, string id, string code, string name, string? regionId = null, string? countryId = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            RegionId = regionId;
            CountryId = countryId;
        }

        public void UpdateCity(string code, string name, string? regionId = null, string? countryId = null)
        {
            Code = code;
            Name = name;
            RegionId = regionId;
            CountryId = countryId;
        }
    }
}
