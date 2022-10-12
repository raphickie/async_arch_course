using System;
using UP.Ates.TaskTracker.Models;

namespace UP.Ates.TaskTracker.Consumers.Auth;

public class UserConsumer : MessageConsumerBase<ApplicationUser>
{
    protected override string TopicName => TopicNames.User;
    protected override void Handle(ApplicationUser entity)
    {
        Console.WriteLine(entity.Id);
    }
}