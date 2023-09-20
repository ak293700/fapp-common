using System.Collections.Concurrent;
using System.Runtime.Versioning;
using FappCommon.Exceptions.InfrastructureExceptions;
using FappCommon.Kafka.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Config;

[UnsupportedOSPlatform("browser")]
public class KafkaLoggerProvider : ILoggerProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<string, KafkaLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public KafkaLoggerProvider(IServiceProvider serviceScope)
    {
        _serviceProvider = serviceScope;
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (_loggers.TryGetValue(categoryName, out KafkaLogger? logger))
            return logger;

        KafkaProducerConfig config =
            _serviceProvider.GetRequiredService<KafkaProducerConfig>()
            ?? throw DependencyInjectionException.GenerateException(nameof(KafkaProducerConfig));

        logger = new KafkaLogger(new KafkaLogProducerService(config), categoryName);
        _loggers.TryAdd(categoryName, logger);

        return logger;
    }

    void IDisposable.Dispose()
    {
        // Nothing to dispose
    }
}