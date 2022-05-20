using domain;
using MassTransit;
using Repositories;

public class JobConsumer : IConsumer<Job>
{
    private readonly ILogger<JobConsumer> logger;
    private readonly IJobRepository jobRepo;

    public JobConsumer(ILogger<JobConsumer> logger, IJobRepository jobRepo)
    {
        this.logger = logger;
        this.jobRepo = jobRepo;
    }

    public async Task Consume(ConsumeContext<Job> context)
    {
        using (var session = await jobRepo.GetClient().StartSessionAsync())
        {
            session.StartTransaction();

            Job job = context.Message;

            await jobRepo.UpdateJobStatus(job.ID, JobStatus.Started);
            logger.LogInformation("Job started at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000);

            await jobRepo.UpdateJobStatus(job.ID, JobStatus.Success);
            logger.LogInformation("Job processed successfully at: {time}", DateTimeOffset.Now);

            await session.CommitTransactionAsync();
        }     
    }
}