using System;
using Newtonsoft.Json;
using UP.Ates.Common.Kafka;

namespace UP.Ates.Accounting.Consumers.TaskTracker.TaskAssignedEvent.v1;

public class
    TaskAssignedEventConsumer : MessageConsumer<
        Ates.TaskTracker.Contracts.Outgoing.TaskAssignedEvent.v1.TaskAssignedEvent>
{
    protected override string TopicName => "TaskAssigned";

    protected override void Handle(Ates.TaskTracker.Contracts.Outgoing.TaskAssignedEvent.v1.TaskAssignedEvent entity)
    {
        Console.WriteLine(JsonConvert.SerializeObject(entity));
    }
}