using domain;
using Repositories;

namespace supervisor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IJobRepository _jobRepository;

    public Worker(ILogger<Worker> logger, IJobRepository jobRepository)
    {
        _logger = logger;
        _jobRepository = jobRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Supervisor running at: {time}", DateTime.UtcNow);

        while (!stoppingToken.IsCancellationRequested)
        {
            var jobs = await _jobRepository.ListJobs();

            foreach (Job job in jobs)
            {
                if(job.FailCount < job.MaxFailCount){
                    job.FailCount++;
                    job.JobStatus = JobStatus.NotStarted;                  
                }
                else
                    job.JobStatus = JobStatus.Failed;
                    
                await _jobRepository.UpdateJob(job);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}
