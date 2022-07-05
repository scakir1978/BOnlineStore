namespace BOnlineStore.MongoDb.GenericRepository
{
    public interface ISettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
