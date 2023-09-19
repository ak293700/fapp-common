using System.Text.Json;
using FappCommon.Kafka.Interfaces;
using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Log;

public class KafkaLogger : IKafkaLogger
{
    private readonly KafkaLogProducerService _kafkaLogProducerService;

    public KafkaLogger(KafkaLogProducerService kafkaLogProducerService)
    {
        _kafkaLogProducerService = kafkaLogProducerService;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return default!;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        // Because the logging level is already checked
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        (string? template, Dictionary<string, object>? arguments) = GetTemplateAndArguments(state);

        if (template is null || arguments is null)
            return; // have a backup plan here

        KafkaLogMessage kafkaLogMessage = new KafkaLogMessage(
            template,
            logLevel,
            DateTime.Now,
            JsonSerializer.Serialize(arguments));

        Task.Run(() => _kafkaLogProducerService.Produce(kafkaLogMessage));
    }

    private static (string?, Dictionary<string, object>?) GetTemplateAndArguments<TState>(TState state)
    {
        if (state is not IReadOnlyList<KeyValuePair<string, object>> properties)
            return (null, null);

        const string originalFormat = "{OriginalFormat}";
        string? messageTemplate = properties
            .FirstOrDefault(p => p.Key == originalFormat).Value
            ?.ToString();

        Dictionary<string, object> arguments =
            properties
                .Where(p => p.Key != originalFormat)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value);

        return (messageTemplate, arguments);
    }
}