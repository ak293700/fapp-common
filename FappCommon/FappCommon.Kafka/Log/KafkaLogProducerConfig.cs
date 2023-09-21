using FappCommon.Kafka.Config;
using Microsoft.Extensions.Configuration;

namespace FappCommon.Kafka.Log;

public class KafkaLogProducerConfig : KafkaProducerConfig
{
    public KafkaLogProducerConfig(IConfiguration configuration)
        : base(configuration, "Log")
    {
    }
}