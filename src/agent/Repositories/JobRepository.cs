using domain;
using infrastructure;
using MongoDB.Driver;

namespace Repositories;

public interface IJobRepository
{
    IMongoClient GetClient();

    Task<List<Job>> ListNotStarted();

    Task UpdateJob(Job job);

    Task UpdateJobStatus(Guid jobID, JobStatus status);
}
public class JobRepository: IJobRepository{
    private readonly IMongoDBHelper mongoDBHelper;
    private readonly string collectionName = "Job";

    public JobRepository(IMongoDBHelper mongoDBHelper)
    {
        this.mongoDBHelper = mongoDBHelper;
    }

    public IMongoClient GetClient(){
        return mongoDBHelper.GetClient();
    } 

    public async Task<List<Job>> ListNotStarted(){
        return await mongoDBHelper.Select<Job>(collectionName, job => job.JobStatus == JobStatus.NotStarted);
    }

    public async Task UpdateJob(Job job){
        await mongoDBHelper.UpdateAsync<Job>(collectionName, x => x.ID == job.ID, job);
    }

    public async Task UpdateJobStatus(Guid jobID, JobStatus status){
        await mongoDBHelper.UpdateFieldAsync<Job, JobStatus>(collectionName, job => job.ID == jobID, job => job.JobStatus, status);
    }
}

