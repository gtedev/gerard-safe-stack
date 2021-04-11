namespace GerardSafe.MongoDb.Database
{
    public static class MongoDBContextFactory
    {
        public static IMongoDBContext CreateContext()
        {
            var client = MongoDbClient.CreateClient();
            return new MongoDBContext(client);
        }
    }
}
