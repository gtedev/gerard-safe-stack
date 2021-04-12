using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GerardSafe.MongoDb.Database.Models
{
    public class Workout
    {
        public Workout(string name, WorkoutType type, string workoutFamily, IEnumerable<Workout> exercises = null)
        {
            Name = name;
            Exercises = exercises;
            Type = type;
            WorkoutFamily = workoutFamily;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        ////[BsonElement("Name")]
        public string Name { get; set; }

        public IEnumerable<Workout> Exercises { get; set; }

        public WorkoutType Type { get; set; }

        public string WorkoutFamily { get; set; }

        // used in Seed project, to determine if Workout has been already added in datasbase.
        public override bool Equals(object obj)
        {
            var item = obj as Workout;

            if (item == null)
            {
                return false;
            }

            return this.Name == item.Name;
        }
    }
}
