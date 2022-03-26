using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities;

public class Job{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("JobID")]
    public Guid JobID { get; set; }

    [BsonElement("StartTime")]
    public DateTime StartTime { get; set; }

    [BsonElement("CompleteBy")]
    public DateTime CompleteBy { get; set; }

    [BsonElement("FailCount")]
    public int FailCount { get; set; }

    [BsonElement("MaxFailCount")]
    public int MaxFailCount { get; set; }

    [BsonElement("JobStatus")]
    public JobStatus JobStatus { get; set; }
}

public enum JobStatus{
    NotStarted = 0,
    Scheduled = 1,
    Started = 2,
    Success = 3,
    Failed = 4
}
