
namespace Models;

public class Job{
    public Guid ID { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? CompleteBy { get; set; }

    public int FailCount { get; set; }

    public int MaxFailCount { get; set; }

    public JobStatus JobStatus { get; set; }
}

public enum JobStatus{
    NotStarted = 0,
    Scheduled = 1,
    Started = 2,
    Success = 3,
    Failed = 4
}