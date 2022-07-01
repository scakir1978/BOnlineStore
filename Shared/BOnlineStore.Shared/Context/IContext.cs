using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared
{
    public interface IContext
    {
        IMongoDatabase Database { get; set; }
        MongoClient Client { get; set; }
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
