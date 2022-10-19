using System;

namespace UP.Ates.TaskTracker.Domain;

public class PopugTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }
    public TaskStatus Status { get; set; }
}

public enum TaskStatus
{
    Undefined = 0,
    NotDone = 1,
    Done = 2
}