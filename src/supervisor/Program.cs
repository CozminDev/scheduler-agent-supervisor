using infrastructure;
using Repositories;
using supervisor;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMongoDBHelper(cfg => {
             cfg.ConnectionString = Environment.GetEnvironmentVariable("DBHOST");
             cfg.DatabaseName =  Environment.GetEnvironmentVariable("DBNAME");
        });
        services.AddSingleton<IJobRepository, JobRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();