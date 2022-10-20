
// ReSharper disable once CheckNamespace
namespace UP.Ates.TaskTracker.Contracts.Outgoing.PopugTask.v1;

public class PopugTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }
    public int Status { get; set; }
}