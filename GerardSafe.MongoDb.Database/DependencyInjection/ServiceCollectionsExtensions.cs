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

            var settings = new MongoDbSettings();
            configuration.GetSection(nameof(MongoDbSettings)).Bind(settings);

            serviceCollection.AddSingleton<IMongoDbSettings, MongoDbSettings>((_) => settings);

            var mongoDbClient = MongoDbClient.CreateClient(settings.ConnectionString);
            serviceCollection.AddSingleton(mongoDbClient);
            serviceCollection.AddTransient<IMongoDBContext, MongoDBContext>();

            return serviceCollection;
        }
    }
}
