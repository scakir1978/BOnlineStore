using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Shared.Entities
{
    public abstract class Entity : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid TenantId { get; private set; }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set ; }

        public Entity(Guid id, Guid tenantId)
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
