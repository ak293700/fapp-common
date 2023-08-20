using Confluent.Kafka;
using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions;
using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions.Base;
using Microsoft.Extensions.Configuration;


namespace FappCommon.Kafka.Config;

public class KafkaConfig
{
    public string BootstrapServers { get; }
    public string GroupId { get; }
    public string Topic { get; }
    public ProducerConfig ProducerConfig { get; }
    public ConsumerConfig ConsumerConfig { get; }

    public KafkaConfig(IConfiguration configuration, bool isConsumer = false)
    {
        IConfigurationSection section = configuration.GetSection("Kafka");

        BootstrapServers = section["BootstrapServers"]
                           ?? throw ValueNotFoundConfigurationException.GenerateException(
                               $"Kafka:{nameof(BootstrapServers)}");

        // Needed only for consumer
        GroupId = section["GroupId"]
                  ?? (!isConsumer
                      ? string.Empty
                      : throw ValueNotFoundConfigurationException.GenerateException($"Kafka:{nameof(GroupId)}"));

        Topic = section["Topic"]
                ?? throw ValueNotFoundConfigurationException.GenerateException($"Kafka:{nameof(Topic)}");

        ProducerConfig = new ProducerConfig { BootstrapServers = BootstrapServers };
        ConsumerConfig = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }
}