using MongoDB.Driver;

namespace GerardSafe.MongoDb.Database
{
    public static class MongoDbClient
    {
        public static MongoClient CreateClient(string connectionString = null)
            => new MongoClient(connectionString);
    }
}
