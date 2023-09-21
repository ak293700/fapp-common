using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions;
using Microsoft.Extensions.Configuration;

namespace FappCommon.Kafka.Config;

public abstract class KafkaConsumerConfig : KafkaConfig
{
    public string Group { get; protected set; }
    public string Topic { get; protected set; }

    protected KafkaConsumerConfig(IConfiguration configuration, string kafkaSectionName)
        : base(configuration)
    {
        string kafkaAbsoluteSectionName = $"Kafka:{kafkaSectionName}";
        IConfigurationSection section = configuration.GetSection(kafkaAbsoluteSectionName);

        Group = section["Group"]
                ?? throw ValueNotFoundConfigurationException.GenerateException($"{kafkaAbsoluteSectionName}:Group");

        Topic = section["Topic"]
                ?? throw ValueNotFoundConfigurationException.GenerateException($"{kafkaAbsoluteSectionName}:Topic");
    }
}