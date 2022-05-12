using Mapster;
using Repositories;

namespace Services;

public interface IJobService
{
    Task UpdateJob(Models.Job job);
}

public class JobService: IJobService{
    private readonly IJobRepository jobRepo;

    public JobService(IJobRepository jobRepo)
    {
        this.jobRepo = jobRepo;
    }

    public async Task UpdateJob(Models.Job job){
        await jobRepo.UpdateJob(job.Adapt<Entities.Job>());
    }
}

