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
        /// Bölge Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RegionId { get; private set; }

        public City() : base()
        {
            Code = "";
            Name = "";
        }

        public City(Guid tenantId, string id, string code, string name, string? regionId = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            RegionId = regionId;
        }

        public void UpdateCity(string code, string name, string? regionId = null)
        {
            Code = code;
            Name = name;
            RegionId = regionId;
        }
    }
}
