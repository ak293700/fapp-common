using Microsoft.Extensions.Configuration;


namespace FappCommon.Kafka.Config;

public class KafkaProducerConfig : KafkaConfig
{
    public KafkaProducerConfig(IConfiguration configuration) : base(configuration, false)
    {
    }
}