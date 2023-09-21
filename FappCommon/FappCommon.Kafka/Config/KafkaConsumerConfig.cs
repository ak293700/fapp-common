using Confluent.Kafka;
using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions;
using Microsoft.Extensions.Configuration;

namespace FappCommon.Kafka.Config;

public abstract class KafkaConsumerConfig : KafkaConfig
{
    public string Group { get; }
    public string Topic { get; }

    public ConsumerConfig ConsumerConfig { get; }

    protected KafkaConsumerConfig(IConfiguration configuration, string kafkaSectionName)
        : base(configuration)
    {
        string kafkaAbsoluteSectionName = $"Kafka:{kafkaSectionName}";
        IConfigurationSection section = configuration.GetSection(kafkaAbsoluteSectionName);

        Group = section["Group"]
                ?? throw ValueNotFoundConfigurationException.GenerateException($"{kafkaAbsoluteSectionName}:Group");

        Topic = section["Topic"]
                ?? throw ValueNotFoundConfigurationException.GenerateException($"{kafkaAbsoluteSectionName}:Topic");

        ConsumerConfig = new ConsumerConfig
        {
            BootstrapServers = Host,
            GroupId = Group,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
    }
}