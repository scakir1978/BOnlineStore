using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Mahalle tanımları
    /// </summary>
    public class District : Entity
    {
        /// <summary>
        /// Mahalle kodu
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// Mahalle adı
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// İlçe Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CountyId { get; private set; }


        public District() : base()
        {
            Code = "";
            Name = "";
        }

        public District(Guid tenantId, string id, string code, string name, string? countyId = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            CountyId = countyId;
        }

        public void UpdateDistrict(string code, string name, string? countyId = null)
        {
            Code = code;
            Name = name;
            CountyId = countyId;
        }
    }
}
