using System.Text.Json;
using System.Text.Json.Serialization;
using FappCommon.Kafka.Base;
using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Log;

[Serializable]
[JsonSerializable(typeof(JsonSerializer))]
public record KafkaLogMessage(
    string Template,
    LogLevel LogLevel,
    DateTime TimespanAsUtc,
    string? SourceAppName,
    string? SourceClassName,
    string Data
)
{
    internal static MessageSerializer<KafkaLogMessage> Serializer { get; } = new();
}