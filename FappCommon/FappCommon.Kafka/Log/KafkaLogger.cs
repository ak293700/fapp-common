using FappCommon.Kafka.Interfaces;
using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Log;

public class KafkaLogger<T> : IKafkaLogger<T>
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
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;


        Console.WriteLine(formatter(state, exception));
        // _kafkaLogProducerService.ProduceMessage(formatter(state, exception));
    }
}