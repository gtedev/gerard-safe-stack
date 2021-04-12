using GerardSafe.MongoDb.Database.Models;
using System.Collections.Generic;

namespace GerardSafe.MongoDb.Database
{
    public interface IMongoDBContext
    {
        IEnumerable<Workout> GetWorkouts();

        void InsertWorkouts(IEnumerable<Workout> workouts);
    }
}
