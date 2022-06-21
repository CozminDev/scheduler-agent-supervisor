using System.Linq.Expressions;
using domain;
using infrastructure;

namespace Repositories;

public interface IJobRepository
{
    Task<List<Job>> ListJobs();

    Task UpdateJob(Job job);
}
public class JobRepository: IJobRepository{
    private readonly IMongoDBHelper mongoDBHelper;
    private readonly string collectionName = "Job";

    public JobRepository(IMongoDBHelper mongoDBHelper)
    {
        this.mongoDBHelper = mongoDBHelper;
    }

    public async Task<List<Job>> ListJobs(){
        Expression<Func<Job, bool>> filter = job => (job.JobStatus != JobStatus.Success && job.JobStatus != JobStatus.Failed) && job.CompleteBy < DateTime.UtcNow;

        return await mongoDBHelper.Select<Job>(collectionName, filter);
    }

    public async Task UpdateJob(Job job){
        await mongoDBHelper.UpdateAsync<Job>(collectionName, x => x.ID == job.ID, job);
    }
}

