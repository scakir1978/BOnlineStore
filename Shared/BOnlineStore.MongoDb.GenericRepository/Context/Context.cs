using MongoDB.Driver;

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
