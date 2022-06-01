using MassTransit;
using scheduler;
using infrastructure;
using Repositories;

var builder = Host.CreateDefaultBuilder(args);

IHost host = builder
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMongoDBHelper(cfg => {
             cfg.ConnectionString = Environment.GetEnvironmentVariable("DBHOST");
             cfg.DatabaseName =  Environment.GetEnvironmentVariable("DBNAME");
        });
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
