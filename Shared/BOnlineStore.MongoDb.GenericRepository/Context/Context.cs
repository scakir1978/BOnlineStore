﻿using MongoDB.Driver;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public class Context : IContext
    {
        public IMongoDatabase Database { get; set; }
        public MongoClient Client { get; set; }

        public Context(IDatabaseSettings settings)
        {
            MongoClientSettings mongoClientSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            Client = new MongoClient(mongoClientSettings);            
            Database = Client.GetDatabase(settings.DatabaseName);
        }        

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            return Database.GetCollection<TEntity>(name);
        }
    }
}
