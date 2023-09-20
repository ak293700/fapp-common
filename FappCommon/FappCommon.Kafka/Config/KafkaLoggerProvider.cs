using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Versioning;
using FappCommon.Exceptions.InfrastructureExceptions;
using FappCommon.Kafka.Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FappCommon.Kafka.Config;

/// <summary>
/// Provide the name of the app under the "AppName" section of the configuration
/// to log this info, otherwise the name of the startup assembly is used.
/// </summary>
[UnsupportedOSPlatform("browser")]
public class KafkaLoggerProvider : ILoggerProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<string, KafkaLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);
    private readonly string? _currentAppName;

    public KafkaLoggerProvider(IServiceProvider serviceScope, IConfiguration configuration)
    {
        _serviceProvider = serviceScope;
        _currentAppName = configuration.GetValue<string>("AppName")
                          ?? Assembly.GetEntryAssembly()?.GetName().FullName;
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (_loggers.TryGetValue(categoryName, out KafkaLogger? logger))
            return logger;

        KafkaProducerConfig config =
            _serviceProvider.GetRequiredService<KafkaProducerConfig>()
            ?? throw DependencyInjectionException.GenerateException(nameof(KafkaProducerConfig));

        logger = new KafkaLogger(new KafkaLogProducerService(config), _currentAppName, categoryName);
        _loggers.TryAdd(categoryName, logger);

        return logger;
    }

    void IDisposable.Dispose()
    {
        // Nothing to dispose
    }
}