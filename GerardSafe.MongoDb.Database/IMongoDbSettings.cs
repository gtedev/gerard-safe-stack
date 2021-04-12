namespace GerardSafe.MongoDb.Database
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; }

        string ConnectionString { get; }
    }
}
