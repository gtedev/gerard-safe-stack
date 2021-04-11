using MongoDB.Driver;

namespace GerardSafe.MongoDb.Database
{
    public static class MongoDbClient
    {
        public static MongoClient CreateClient(string connectionString = null)
        {
            // using temporarily hardcoded connection string
            var localConnectionString = "mongodb://localhost:27017";

            var devConnectionString =
                "mongodb+srv://gerard-workout-dev:gerard-workout-dev@gerardworkouts.02abm.mongodb.net/GerardWorkouts?retryWrites=true&w=majority";

            return new MongoClient(localConnectionString);
        }
    }
}
