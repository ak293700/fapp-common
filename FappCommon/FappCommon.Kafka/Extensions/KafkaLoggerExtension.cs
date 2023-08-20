using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace FappCommon.Kafka.Extensions;

public static class KafkaLoggerExtension
{
    public static ILoggingBuilder AddKafkaLogger(this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DbLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions<DbLoggerConfiguration, DbLoggerProvider>(builder.Services);

        return builder;
    }
    //
    // public static ILoggingBuilder AddKafkaLogger(this ILoggingBuilder builder,
    //     Action<DbLoggerConfiguration> configure)
    // {
    //     builder.AddDbLogger();
    //     builder.Services.Configure(configure);
    //
    //     return builder;
    // }
}