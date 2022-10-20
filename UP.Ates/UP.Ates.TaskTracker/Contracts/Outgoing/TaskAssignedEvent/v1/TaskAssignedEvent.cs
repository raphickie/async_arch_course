namespace UP.Ates.TaskTracker.Contracts.Outgoing.TaskAssignedEvent.v1;

public class TaskAssignedEvent
{
    public string Id { get; set; }
    public string UserId { get; set; }

    public static TaskAssignedEvent FromDomain(Domain.PopugTask domainTask) => new TaskAssignedEvent
    {
        Id = domainTask.Id,
        UserId = domainTask.UserId,
    };
}