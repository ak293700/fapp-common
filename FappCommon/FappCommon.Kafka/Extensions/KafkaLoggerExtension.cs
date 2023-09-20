using FappCommon.Kafka.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace FappCommon.Kafka.Extensions;

public static class KafkaLoggerExtension
{
    public static ILoggingBuilder AddKafkaLogger(this ILoggingBuilder builder)
    {
        builder.AddConfiguration();
        // builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, KafkaLoggerProvider>());
        builder.Services.AddSingleton<ILoggerProvider, KafkaLoggerProvider>();

        return builder;
    }
}