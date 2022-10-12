using Confluent.Kafka;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UP.Ates.Auth.Models;

namespace UP.Ates.Auth.Producers
{
    public class MessageProducer
    {
        public async Task Produce(ApplicationUser message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "127.0.0.1:9092",
            };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var messageJson = JsonConvert.SerializeObject(message);
            await producer.ProduceAsync(TopicNames.User,
                new Message<Null, string> { Value = messageJson });
        }
    }

    public static class TopicNames
    {
        public static string User = "User";
    }
}