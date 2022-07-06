namespace BOnlineStore.MongoDb.GenericRepository
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        //Configuration için kullanılacak
        #region Const Values

        public const string ConnectionStringValue = nameof(ConnectionString);
        public const string DatabaseNameValue = nameof(DatabaseName);

        #endregion

    }
}
