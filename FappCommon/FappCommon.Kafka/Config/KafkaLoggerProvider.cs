using System.Runtime.Versioning;
using FappCommon.Exceptions.InfrastructureExceptions;
using FappCommon.Kafka.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Config;

[UnsupportedOSPlatform("browser")]
public class KafkaLoggerProvider : ILoggerProvider
{
    private KafkaLogger? LoggerSingleton { get; set; }
    private IServiceProvider ServiceProvider { get; set; }

    public KafkaLoggerProvider(IServiceProvider serviceScope)
    {
        ServiceProvider = serviceScope;
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (LoggerSingleton is not null)
            return LoggerSingleton;

        KafkaProducerConfig config =
            ServiceProvider.GetRequiredService<KafkaProducerConfig>()
            ?? throw DependencyInjectionException.GenerateException(nameof(KafkaProducerConfig));

        LoggerSingleton = new KafkaLogger(new KafkaLogProducerService(config));

        return LoggerSingleton;
    }

    public void Dispose()
    {
        // Nothing to dispose
    }
}