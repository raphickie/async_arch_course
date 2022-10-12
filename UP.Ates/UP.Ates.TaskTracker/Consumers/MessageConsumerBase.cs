using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace UP.Ates.TaskTracker.Consumers;

public abstract class MessageConsumerBase<TEntity>:IHostedService
{
    private CancellationTokenSource _cts;
    private Thread _consumingThread;
    protected abstract string TopicName { get; }

    protected MessageConsumerBase()
    {
        _cts = new CancellationTokenSource();
    }

    protected static class TopicNames
    {
        public static string User = "User";
    }
    
    public void Consume(CancellationToken token)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092,localhost:9092",
            GroupId = "foo",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(new[]{TopicName});

        while (!token.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(token);
            var resultString = consumeResult.Message.Value;
            var resultObject = JsonConvert.DeserializeObject<TEntity>(resultString);
            Handle(resultObject);
            consumer.Commit(consumeResult);
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