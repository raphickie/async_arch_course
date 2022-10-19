using System;

namespace UP.Ates.TaskTracker.Domain;

public class PopugTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }
    public int Status { get; set; }
}