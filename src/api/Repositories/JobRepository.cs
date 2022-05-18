using domain;
using infrastructure;

namespace Repositories;

public interface IJobRepository
{
    Task UpdateJob(Job job);
}
public class JobRepository: IJobRepository{
    private readonly IMongoDBHelper mongoDBHelper;
    private readonly string collectionName = "Job";

    public JobRepository(IMongoDBHelper mongoDBHelper)
    {
        this.mongoDBHelper = mongoDBHelper;
    }

    public async Task UpdateJob(Job job){
        await mongoDBHelper.UpdateAsync<Job>(collectionName, x => x.ID == job.ID, job);
    }
}

