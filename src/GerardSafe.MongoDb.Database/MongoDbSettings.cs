using System;
using System.Collections.Generic;
using System.Text;

namespace GerardSafe.MongoDb.Database
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }
    }
}
