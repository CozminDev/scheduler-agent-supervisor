using Microsoft.Extensions.DependencyInjection;

namespace infrastructure;

public static class ServiceCollectionExtensions{
    public static void AddMongoDBHelper(this IServiceCollection services, Action<IConfig> configuration){
        Config config = new Config();
        configuration(config);

        services.AddSingleton<IConfig>(config);
        services.AddSingleton<IMongoDBHelper, MongoDBHelper>();
    }
}