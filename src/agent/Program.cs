using MassTransit;
using Repositories;
using infrastructure;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMongoDBHelper(cfg => {
             cfg.ConnectionString = hostContext.Configuration.GetSection("Config").GetValue<string>("ConnectionString");
             cfg.DatabaseName =  hostContext.Configuration.GetSection("Config").GetValue<string>("DatabaseName");
        });
        services.AddTransient<IJobRepository, JobRepository>();
        services.AddMassTransit(x =>
            {
                x.AddConsumer<JobConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("job-queue", e =>
                    {
                        e.ConfigureConsumer<JobConsumer>(context);
                    });
                });
            });
    })
    .Build();

await host.RunAsync();
