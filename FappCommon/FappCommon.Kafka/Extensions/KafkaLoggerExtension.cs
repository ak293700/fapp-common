using FappCommon.Kafka.Config;
using FappCommon.Kafka.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace FappCommon.Kafka.Extensions;

public static class KafkaLoggerExtension
{
    public static ILoggingBuilder AddKafkaLogger(this ILoggingBuilder builder)
    {
        IServiceCollection services = builder.Services;

        services.TryAddSingleton<KafkaLogProducerConfig>();
        services.TryAddScoped<KafkaLogProducerService>();

        builder.AddConfiguration();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, KafkaLoggerProvider>());
        return builder;
    }
}