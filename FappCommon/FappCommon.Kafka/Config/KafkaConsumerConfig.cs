using Microsoft.Extensions.Configuration;


namespace FappCommon.Kafka.Config;

public class KafkaConsumerConfig : KafkaConfig
{
    public KafkaConsumerConfig(IConfiguration configuration) : base(configuration, true)
    {
    }
}