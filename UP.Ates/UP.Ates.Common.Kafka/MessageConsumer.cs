using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace UP.Ates.Common.Kafka;

public abstract class MessageConsumer<TEntity> : IHostedService
{
    private CancellationTokenSource _cts;
    private Thread? _consumingThread;
    private readonly string _entityName;
    protected abstract string TopicName { get; }

    protected MessageConsumer()
    {
        _consumingThread = default;
        _cts = new CancellationTokenSource();
        _entityName = typeof(TEntity).FullName;
    }

    protected static class TopicNames
    {
        public static string User = "User";
    }

    private void Consume(CancellationToken token)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092,localhost:9092",
            GroupId = _entityName,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(new[] { TopicName });

        while (!token.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(token);
            var resultString = consumeResult.Message.Value;
            if (typeof(TEntity).FullName == consumeResult.Message.Key)
            {
                var resultObject = JsonConvert.DeserializeObject<TEntity>(resultString);
                Handle(resultObject);
                consumer.Commit(consumeResult);
            }
        }

        consumer.Close();
    }

    protected abstract void Handle(TEntity entity);


    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consumingThread = new Thread(() => Consume(_cts.Token));
        _consumingThread.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts.Cancel();
        return Task.CompletedTask;
    }
}