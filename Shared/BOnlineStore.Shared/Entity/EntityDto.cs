using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Shared.Entity
{
    public  abstract class EntityDto
    {
       public Guid Id { get; set; }
    }
}
