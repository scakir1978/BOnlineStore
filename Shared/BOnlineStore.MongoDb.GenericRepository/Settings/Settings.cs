namespace BOnlineStore.MongoDb.GenericRepository
{
    public class Settings : ISettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
