using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BOnlineStore.Shared.Entities
{
    public interface ITenant
    {
        [BsonRepresentation(BsonType.String)]
        Guid TenantId { get; }

        void SetTenant(Guid tenantId);
    }
}
