using System.Threading.Tasks;
using UP.Ates.Common.Kafka;
using UP.Ates.TaskTracker.Contracts.Outgoing.PopugTask.v2;
using UP.Ates.TaskTracker.Contracts.Outgoing.TaskAssignedEvent.v1;

namespace UP.Ates.TaskTracker.Producers
{
    public class PopugProducer : MessageProducer<Domain.PopugTask>
    {
        protected override object MapToContract(Domain.PopugTask message)
        {
            return PopugTask.FromDomain(message);
        }
        
        public async Task ProduceTaskAssigned(Domain.PopugTask popugTask, string topicName)
        {
            var evt =  TaskAssignedEvent.FromDomain(popugTask);
            await ProduceInternal(evt, topicName);
        }
    }
}