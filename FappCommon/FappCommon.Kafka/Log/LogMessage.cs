using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Log;

public record LogMessage(
    string Template,
    LogLevel LogLevel,
    object Data
    );