using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// İlçe
    /// </summary>
    public class County : Entity
    {
        /// <summary>
        /// İlçe kodu.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// İlçe adı.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Şehir Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CityId { get; private set; }

        public County() : base()
        {
            Code = "";
            Name = "";
        }

        public County(Guid tenantId, string id, string code, string name, string? cityId = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            CityId = cityId;
        }

        public void UpdateCounty(string code, string name, string? cityId = null)
        {
            Code = code;
            Name = name;
            CityId = cityId;
        }
    }
}
