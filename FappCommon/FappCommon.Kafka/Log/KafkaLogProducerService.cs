using Confluent.Kafka;

namespace FappCommon.Kafka.Log;

public class KafkaLogProducerService
{
    private readonly KafkaLogProducerConfig _config;

    public KafkaLogProducerService(KafkaLogProducerConfig config)
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