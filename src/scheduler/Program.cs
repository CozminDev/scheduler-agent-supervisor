using MassTransit;
using scheduler;
using infrastructure;
using Repositories;

var builder = Host.CreateDefaultBuilder(args);

IHost host = builder
    .ConfigureServices((hostContext, services) =>
    {
        Config config = new Config(){
            ConnectionString = Environment.GetEnvironmentVariable("DBHOST") ?? hostContext.Configuration.GetSection("Config").GetValue<string>("ConnectionString"),
            DatabaseName = Environment.GetEnvironmentVariable("DBNAME") ?? hostContext.Configuration.GetSection("Config").GetValue<string>("DatabaseName"),
        };
        services.AddSingleton<IConfig>(config);
        services.AddSingleton<IMongoDBHelper, MongoDBHelper>();
        services.AddSingleton<IJobRepository, JobRepository>();
        services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context,cfg) =>
                {
                    cfg.Host(Environment.GetEnvironmentVariable("RABBIT"), h => {
                        h.Username("guest");
                        h.Password("guest");
                    });

                });
            });
            
        services.AddHostedService<Worker>();
        
    })
    .Build();

await host.RunAsync();
