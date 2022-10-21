namespace UP.Ates.TaskTracker.Contracts.Outgoing.PopugTask.v2;

public class PopugTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string JiraId { get; set; }
    public string UserId { get; set; }
    public int Status { get; set; }

    public static PopugTask FromDomain(Domain.PopugTask domainTask) => new PopugTask
    {
        Id = domainTask.Id,
        Title = domainTask.Title,
        JiraId = domainTask.JiraId,
        UserId = domainTask.UserId,
        Status = (int)domainTask.Status
    };
}