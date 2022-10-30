using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Shared.Entities
{
    public abstract class Entity : IEntity
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set ; }


        [BsonRepresentation(BsonType.String)]
        public Guid TenantId { get; private set; }

        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public Entity(Guid tenantId, string id)
        {
            Id = id;
            TenantId = tenantId;
        }

        public void SetTenant(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}
