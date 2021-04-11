namespace GerardSafe.MongoDb.Database
{
    public static class MongoDBContextFactory
    {
        public static IMongoDBContext CreateContext()
        {
            var client = MongoDbClient.CreateClient("mongodb://localhost:27017");
            return new MongoDBContext(client);
        }
    }
}
