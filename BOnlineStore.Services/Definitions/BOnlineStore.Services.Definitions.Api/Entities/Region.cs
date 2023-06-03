using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Bölge tanımlama
    /// <summary>
    /// Bölge
    /// </summary>
    public class Region : Entity
    {
        /// <summary>
        /// Bölge kodu
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// Bölge adı
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Ülke Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CountryId { get; private set; }

        public Region() : base()
        {
            Code = "";
            Name = "";
        }

        public Region(Guid tenantId, string id, string code, string name, string? countryId = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            CountryId = countryId;
        }

        public void UpdateRegion(string code, string name, string? countryId = null)
        {
            Code = code;
            Name = name;
            CountryId = countryId;
        }
    }
}
