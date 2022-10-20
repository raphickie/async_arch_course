using System;
using UP.Ates.Common.Kafka;

namespace UP.Ates.Accounting.Consumers.TaskTracker.PopugTask.v1;

public class PopugTaskV1Consumer : MessageConsumer<Ates.TaskTracker.Contracts.Outgoing.PopugTask.v1.PopugTask>
{
    protected override string TopicName => "PopugTask";

    protected override void Handle(Ates.TaskTracker.Contracts.Outgoing.PopugTask.v1.PopugTask entity)
    {
    }
}