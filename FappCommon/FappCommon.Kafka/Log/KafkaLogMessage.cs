using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Log;

[Serializable]
[JsonSerializable(typeof(JsonSerializer))]
public record KafkaLogMessage(
    string Template,
    LogLevel LogLevel,
    DateTime TimespanAsUtc,
    string Data
)
{
    public static KafkaJsonSerializer Serializer = new();
}

public class KafkaJsonSerializer : IDeserializer<KafkaLogMessage>, ISerializer<KafkaLogMessage>
{
    public byte[] Serialize(KafkaLogMessage data, SerializationContext context)
    {
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
    }

    public KafkaLogMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull)
        {
            return default!;
        }

        string json = Encoding.UTF8.GetString(data.ToArray());
        return JsonSerializer.Deserialize<KafkaLogMessage>(json)!;
    }
}