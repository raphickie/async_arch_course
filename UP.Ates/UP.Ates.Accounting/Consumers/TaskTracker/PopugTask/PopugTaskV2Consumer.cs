using System;
using UP.Ates.Common.Kafka;

namespace UP.Ates.Accounting.Consumers.TaskTracker.PopugTask;

public class PopugTaskV2Consumer : MessageConsumer<Ates.TaskTracker.Contracts.Outgoing.PopugTask.v2.PopugTask>
{
    protected override string TopicName => "PopugTask";

    protected override void Handle(Ates.TaskTracker.Contracts.Outgoing.PopugTask.v2.PopugTask entity)
    {
        Console.WriteLine("Saving the new task in database");
    }
}