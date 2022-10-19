using System;
using UP.Ates.Common.Kafka;

namespace UP.Ates.TaskTracker.Consumers.Auth;

public class UserConsumer : MessageConsumer<Ates.Auth.Contracts.Outgoing.v1.ApplicationUser>
{
    protected override string TopicName => TopicNames.User;
    protected override void Handle(Ates.Auth.Contracts.Outgoing.v1.ApplicationUser entity)
    {
        Console.WriteLine(entity.Id);
    }
}