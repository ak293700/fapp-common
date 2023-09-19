using Confluent.Kafka;
using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions;
using Microsoft.Extensions.Configuration;

namespace FappCommon.Kafka.Config;

public class KafkaConfig
{
    public string BootstrapServers { get; }
    public string Group { get; }
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
        Group = section["Group"]
                ?? (!isConsumer
                    ? string.Empty
                    : throw ValueNotFoundConfigurationException.GenerateException($"Kafka:{nameof(Group)}"));

        Topic = section["Topic"]
                ?? throw ValueNotFoundConfigurationException.GenerateException($"Kafka:{nameof(Topic)}");

        ProducerConfig = new ProducerConfig { BootstrapServers = BootstrapServers };
        ConsumerConfig = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = Group,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }
}