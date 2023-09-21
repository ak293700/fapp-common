using System.Text.Json;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using FappCommon.Kafka.Config;
using FappCommon.Kafka.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FappCommon.Kafka.Base;

public abstract class KafkaConsumerBase<TMessage, TServiceStruct, TConfig> : BackgroundService, IKafkaConsumer
    where TServiceStruct : class
    where TConfig : KafkaConsumerConfig
{
    private readonly TConfig _config;
    private readonly IServiceScopeFactory _scopeFactory;
    protected TServiceStruct ServiceStruct { get; private set; } = null!;

    protected KafkaConsumerBase(IServiceScopeFactory scopeFactory, TConfig config)
    {
        _scopeFactory = scopeFactory;
        _config = config;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        KeyValuePair<string, string>[] config =
        {
            new("bootstrap.servers", _config.Host)
        };

        // Create the topic if it does not exist
        using IAdminClient adminClient = new AdminClientBuilder(config).Build();

        try
        {
            await adminClient.CreateTopicsAsync(new List<TopicSpecification>
            {
                new()
                {
                    Name = _config.Topic,
                    ReplicationFactor = 1,
                    NumPartitions = 1
                }
            }, new CreateTopicsOptions { RequestTimeout = TimeSpan.FromSeconds(10) });
        }
        catch (CreateTopicsException e)
        {
            if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
            {
                throw;
            }
        }

        await base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // stoppingToken is used from within the Perform method
#pragma warning disable CA2016
        // ReSharper disable once MethodSupportsCancellation
        return Task.Run(() => Init(stoppingToken));
#pragma warning restore CA2016
    }

    private async Task Init(CancellationToken stoppingToken)
    {
        IServiceScope scope = _scopeFactory.CreateScope();
        ServiceStruct = InitServiceStruct(scope);
        // LogService context = scope.ServiceProvider.GetRequiredService<LogService>();

        using IConsumer<Ignore, TMessage>? consumer =
            new ConsumerBuilder<Ignore, TMessage>(_config.ConsumerConfig)
                .SetValueDeserializer(new MessageSerializer<TMessage>())
                .Build();
        consumer.Subscribe(_config.Topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ConsumeOnce(consumer, stoppingToken);
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }

    protected abstract TServiceStruct InitServiceStruct(IServiceScope scope);

    protected abstract Task ConsumeOnce(IConsumer<Ignore, TMessage> consumer,
        CancellationToken stoppingToken);
}

internal class MessageSerializer<T> : ISerializer<T>, IDeserializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data);
    }

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return isNull ? default! : JsonSerializer.Deserialize<T>(data)!;
    }
}