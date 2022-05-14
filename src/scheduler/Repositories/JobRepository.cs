using domain;
using infrastructure;

namespace Repositories;

public interface IJobRepository
{
    Task<List<Job>> ListNotStarted();
}
public class JobRepository: IJobRepository{
    private readonly IMongoDBHelper mongoDBHelper;
    private readonly string collectionName = "Job";

    public JobRepository(IMongoDBHelper mongoDBHelper)
    {
        this.mongoDBHelper = mongoDBHelper;
    }

    public async Task<List<Job>> ListNotStarted(){
        return await mongoDBHelper.Select<Job>(collectionName, job => job.JobStatus == JobStatus.NotStarted);
    }
}

