using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Shared.Entities
{
    public interface IEntity : ITenant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; }

    }
}
