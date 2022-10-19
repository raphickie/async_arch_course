using Confluent.Kafka;
using Newtonsoft.Json;

namespace UP.Ates.Common.Kafka;

public abstract class MessageProducer<T>
{
    protected abstract object MapToContract(T message);
    public async Task Produce(T message, string topicName)
    {
        var contract = MapToContract(message);
        await ProduceInternal(contract, topicName);
    }
    
    protected async Task ProduceInternal(object message, string topicName)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "127.0.0.1:9092",
        };
        using var producer = new ProducerBuilder<string, string>(config).Build();
        var messageJson = JsonConvert.SerializeObject(message);
        var contractFullName = message.GetType().FullName;
        await producer.ProduceAsync(topicName,
            new Message<string, string> { Key=message.GetType().FullName, Value = messageJson });
    }
}