using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Shared.Entities
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; }

        [BsonRepresentation(BsonType.String)]
        Guid TenantId { get; }

        void SetTenant(Guid tenantId);
        
    }
}
