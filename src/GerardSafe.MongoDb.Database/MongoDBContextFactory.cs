namespace GerardSafe.MongoDb.Database
{
    public static class MongoDBContextFactory
    {
        public static IMongoDBContext CreateContext()
        {
            var settings = new MongoDbSettings
            {
                DatabaseName = "local",
                ConnectionString = "mongodb://localhost:27017"
            };

            var client = MongoDbClient.CreateClient(settings.ConnectionString);
            return new MongoDBContext(client, settings);
        }
    }
}
