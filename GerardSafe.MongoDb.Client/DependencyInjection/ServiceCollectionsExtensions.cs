using Microsoft.Extensions.DependencyInjection;

namespace GerardSafe.MongoDb.Database.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbDatabase(this IServiceCollection serviceCollection)
        {
            var mongoDbClient = MongoDbClient.CreateClient();
            serviceCollection.AddSingleton(mongoDbClient);
            serviceCollection.AddTransient<IMongoDBContext, MongoDBContext>();

            return serviceCollection;
        }
    }
}
