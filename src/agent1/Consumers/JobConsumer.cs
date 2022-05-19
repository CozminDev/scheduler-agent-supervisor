using domain;
using MassTransit;

public class JobConsumer : IConsumer<Job>
{
    private readonly ILogger<JobConsumer> _logger;

    public JobConsumer(ILogger<JobConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<Job> context)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
       return Task.CompletedTask;
    }
}