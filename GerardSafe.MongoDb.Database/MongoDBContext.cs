using GerardSafe.MongoDb.Database.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace GerardSafe.MongoDb.Database
{
    public class MongoDBContext : IMongoDBContext
    {
        private IMongoDbSettings settings;
        private MongoClient mongoDbClient;
        private IMongoDatabase mongoDatabase;
        private IMongoCollection<Workout> mongoDbWorkouts;

        public MongoDBContext(IMongoDbSettings settings) 
        {
            this.settings = settings;

            this.mongoDbClient =
                new MongoClient(settings.ConnectionString);

            this.mongoDatabase =
                this.mongoDbClient.GetDatabase(settings.DatabaseName);

            this.mongoDbWorkouts = this.mongoDatabase.GetCollection<Workout>("workouts");
        }

        public IEnumerable<Workout> GetWorkouts()
        {
            return
                this.mongoDbWorkouts
                .Find(Builders<Workout>.Filter.Empty)
                .ToEnumerable();
        }

        public void InsertWorkouts(IEnumerable<Workout> workouts)
        {
            this.mongoDbWorkouts
                .InsertMany(workouts);
        }
    }
}
