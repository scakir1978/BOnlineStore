using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Renk
    /// </summary>
    public class Color : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ColorGroupId { get; private set; }

        public Color() : base()
        {
            Code = "";
            Name = "";
        }

        public Color(Guid tenantId, string id, string code, string name, string colorGroupId) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            ColorGroupId = colorGroupId;
        }

        public void UpdateColor(string code, string name, string colorGroupId)
        {
            Code = code;
            Name = name;
            ColorGroupId = colorGroupId;
        }
    }
}
