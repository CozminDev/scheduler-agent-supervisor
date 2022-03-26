using infrastructure;

namespace Repositories;

public interface IJobRepository
{
    Task UpdateJob(Entities.Job job);
}
public class JobRepository: IJobRepository{
    private readonly IMongoDBHelper mongoDBHelper;
    private readonly string collectionName = "Job";

    public JobRepository(IMongoDBHelper mongoDBHelper)
    {
        this.mongoDBHelper = mongoDBHelper;
    }

    public async Task UpdateJob(Entities.Job job){
        await mongoDBHelper.UpdateAsync<Entities.Job>(collectionName, x => x.JobID == job.JobID, job);
    }
}

