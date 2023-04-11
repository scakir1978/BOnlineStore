using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Model tanımlama

    public class Model : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ModelGroupId { get; private set; }

        public Model() : base()
        {
            Code = "";
            Name = "";
        }

        public Model(Guid tenantId, string id, string code, string name, string modelGroupId) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            ModelGroupId = modelGroupId;
        }

        public void UpdateModelGroup(string code, string name, string modelGroupId)
        {
            Code = code;
            Name = name;
            ModelGroupId = modelGroupId;
        }
    }
}
