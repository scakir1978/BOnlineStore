using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Globalization;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Cam türü
    /// </summary>
    public class Glass : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        /// <summary>
        /// Cam grubu
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string GlassGroupId { get; private set; }

        public Glass() : base()
        {
            Code = "";
            Name = "";
        }

        public Glass(Guid tenantId, string id, string code, string name, string glassGroupId) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            GlassGroupId = glassGroupId;
        }

        public void UpdateGlass(string code, string name, string glassGroupId)
        {
            Code = code;
            Name = name;
            GlassGroupId = glassGroupId;
        }
    }
}
