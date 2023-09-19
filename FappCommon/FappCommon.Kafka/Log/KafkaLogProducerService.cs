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

    public async Task Produce(KafkaLogMessage message)
    {
        using IProducer<Null, KafkaLogMessage>?
            producer = new ProducerBuilder<Null, KafkaLogMessage>(_config.ProducerConfig)
                .SetKeySerializer(Serializers.Null)
                .SetValueSerializer(KafkaLogMessage.Serializer)
                .Build();

        await producer.ProduceAsync(_config.Topic, new Message<Null, KafkaLogMessage> { Value = message });
    }
}