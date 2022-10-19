using UP.Ates.Auth.Models;
using UP.Ates.Common.Kafka;

namespace UP.Ates.Auth.Producers
{
    public class ApplicationUserProducer : MessageProducer<ApplicationUser>
    {
        protected override object MapToContract(ApplicationUser message)
        {
            return Contracts.Outgoing.v1.ApplicationUser.FromDomain(message);
        }
    }
}