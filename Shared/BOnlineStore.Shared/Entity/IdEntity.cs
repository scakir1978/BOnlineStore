using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Entity
{
    public class IdEntity : IIdEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public IdEntity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public IdEntity(string id)
        {
            Id = id;
        }

    }
}
