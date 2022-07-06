namespace BOnlineStore.MongoDb.GenericRepository
{
    public interface IDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
