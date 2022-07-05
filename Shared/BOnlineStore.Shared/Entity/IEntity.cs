using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Shared.Entities
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        Guid Id { get; }
        
    }
}
