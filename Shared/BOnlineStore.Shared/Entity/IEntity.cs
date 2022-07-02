﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        Guid Id { get; }
        
    }
}
