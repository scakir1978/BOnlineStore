using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public class Context : IContext
    {
        public IMongoDatabase Database { get; set; }
        public MongoClient Client { get; set; }

        public Context(ISettings settings)
        {
            Client = new MongoClient(settings.ConnectionString);
            Database = Client.GetDatabase(settings.DatabaseName);
        }        

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            return Database.GetCollection<TEntity>(name);
        }
    }
}
