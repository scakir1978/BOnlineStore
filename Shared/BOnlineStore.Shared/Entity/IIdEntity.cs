using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Entity
{
    public interface IIdEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; }
    }
}
