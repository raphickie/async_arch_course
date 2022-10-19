namespace UP.Ates.TaskTracker.Contracts.Outgoing.v1;

public class PopugTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }
    public int Status { get; set; }

    public static PopugTask FromDomain(Domain.PopugTask domainTask) => new PopugTask
    {
        Id = domainTask.Id,
        Title = domainTask.Title,
        UserId = domainTask.UserId,
        Status = domainTask.Status
    };
}