using Confluent.Kafka;
using FappCommon.Kafka.Config;

namespace FappCommon.Kafka.Log;

public class KafkaLogProducerService
{
    private readonly KafkaProducerConfig _config;

    public KafkaLogProducerService(KafkaProducerConfig config)
    {
        _config = config;
    }

    public void ProduceMessage(LogMessage message)
    {
        using IProducer<Null, LogMessage>?
            producer = new ProducerBuilder<Null, LogMessage>(_config.ProducerConfig).Build();
        
        producer.ProduceAsync(_config.Topic, new Message<Null, LogMessage> { Value = message });
    }
}