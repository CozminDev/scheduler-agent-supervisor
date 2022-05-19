using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
         services.AddMassTransit(x =>
            {
                x.AddConsumer<JobConsumer>();
                x.UsingRabbitMq((context,cfg) =>
                {
                    cfg.Host("localhost", h => {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("job-queue", e => {
                        e.ConfigureConsumer<JobConsumer>(context);
                    });
                });
            });
    })
    .Build();

await host.RunAsync();
