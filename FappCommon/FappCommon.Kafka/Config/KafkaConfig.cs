using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions;
using Microsoft.Extensions.Configuration;

namespace FappCommon.Kafka.Config;

public abstract class KafkaConfig
{
    public string Host { get; protected set; }

    protected KafkaConfig(IConfiguration configuration)
    {
        Host = configuration.GetValue<string>("Kafka:Host")
               ?? throw ValueNotFoundConfigurationException.GenerateException("Kafka:Host");
    }
}