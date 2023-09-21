using Confluent.Kafka;
using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions;
using Microsoft.Extensions.Configuration;

namespace FappCommon.Kafka.Config;

public abstract class KafkaProducerConfig : KafkaConfig
{
    public string Topic { get; }
    public ProducerConfig ProducerConfig { get; }

    protected KafkaProducerConfig(IConfiguration configuration, string kafkaSectionName) : base(configuration)
    {
        string kafkaAbsoluteSectionName = $"Kafka:{kafkaSectionName}";
        IConfigurationSection section = configuration.GetSection(kafkaAbsoluteSectionName);

        Topic = section["Topic"]
                ?? throw ValueNotFoundConfigurationException.GenerateException($"{kafkaAbsoluteSectionName}:Topic");

        ProducerConfig = new ProducerConfig { BootstrapServers = Host };
    }
}