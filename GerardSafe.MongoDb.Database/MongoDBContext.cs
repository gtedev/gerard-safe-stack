using GerardSafe.MongoDb.Database.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace GerardSafe.MongoDb.Database
{
    public class MongoDBContext : IMongoDBContext
    {
        private MongoClient mongoDbClient;
        private IMongoDatabase mongoDatabase;
        private IMongoCollection<Workout> mongoDbWorkouts;

        public MongoDBContext(MongoClient mongoDbClient)
        {
            this.mongoDbClient = mongoDbClient;
            this.mongoDatabase = this.mongoDbClient.GetDatabase("local");

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
