using AutoMapper;
using Repositories;

namespace Services;

public interface IJobService
{
    Task UpdateJob(Models.Job job);
}

public class JobService: IJobService{
    private readonly IMapper mapper;
    private readonly IJobRepository jobRepo;

    public JobService(IMapper mapper, IJobRepository jobRepo)
    {
        this.mapper = mapper;
        this.jobRepo = jobRepo;
    }

    public async Task UpdateJob(Models.Job job){
        await jobRepo.UpdateJob(mapper.Map<Entities.Job>(job));
    }
}

