using FappCommon.Kafka.Config;
using FappCommon.Kafka.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FappCommon.Kafka.Extensions;

public static class KafkaConfigurationExtension
{
    public static IServiceCollection AddKafkaProducer<TProducer, TConfig>(this IServiceCollection services)
        where TProducer : class
        where TConfig : KafkaProducerConfig
    {
        services.TryAddSingleton<TConfig>();
        services.AddSingleton<TProducer>();

        return services;
    }

    public static IServiceCollection AddKafkaConsumer<TConsumer, TConfig>(this IServiceCollection services)
        where TConsumer : class, IKafkaConsumer
        where TConfig : KafkaProducerConfig
    {
        services.TryAddSingleton<TConfig>();
        services.AddHostedService<TConsumer>();

        return services;
    }
}