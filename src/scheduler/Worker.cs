using domain;
using MassTransit;
using Repositories;

namespace scheduler;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _bus;
    private readonly IJobRepository _jobRepository;

    public Worker(ILogger<Worker> logger, IBus bus, IJobRepository jobRepository)
    {
        _logger = logger;
        _bus = bus;
        _jobRepository = jobRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTime.UtcNow);

        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTime.UtcNow);

            var jobs = await _jobRepository.ListNotStarted();

            foreach(var job in jobs){
                // DateTime now = DateTime.UtcNow;

                // job.StartTime = now;
                // job.CompleteBy = now.AddMinutes(10);
                // job.JobStatus = JobStatus.Started;
                // await _jobRepository.UpdateJob(job);

                await Send(job);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }

    public async Task Send(Job job){
        ISendEndpoint endpoint = await _bus.GetSendEndpoint(new Uri("queue:job-queue"));

        await endpoint.Send(job);

        //_logger.LogInformation("Job sent to Queue at: {time}", DateTime.UtcNow);
    }
}
