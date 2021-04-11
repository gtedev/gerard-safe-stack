using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerardSafe.MongoDb.Database.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbDatabase(this IServiceCollection serviceCollection)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

            var connectionString =
                configuration.GetSection("MongoDbSettings")
                .GetSection("ConnectionString")
                .Value;

            var mongoDbClient = MongoDbClient.CreateClient(connectionString);
            serviceCollection.AddSingleton(mongoDbClient);
            serviceCollection.AddTransient<IMongoDBContext, MongoDBContext>();

            return serviceCollection;
        }
    }
}
