using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UP.Ates.TaskTracker.Domain;

public class PopugTask : IValidatableObject

{
    public string Id { get; set; }
    public string Title { get; set; }
    public string JiraId { get; set; }
    public string UserId { get; set; }
    public TaskStatus Status { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        char[] UnsupportedTitleChars = { '[', ']' };
        if (UnsupportedTitleChars.Any(c => Title.Contains(c)))
            yield return new ValidationResult("square brackets are not for Title field. If you want to set JiraId, put it to the JiraId field");
    }
}

public enum TaskStatus
{
    Undefined = 0,
    NotDone = 1,
    Done = 2
}