using MongoDB.Driver;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public interface IContext
    {
        IMongoDatabase Database { get; set; }
        MongoClient Client { get; set; }
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
