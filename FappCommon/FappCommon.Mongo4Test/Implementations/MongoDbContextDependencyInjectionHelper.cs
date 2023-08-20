using FappCommon.Mongo4Test.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FappCommon.Mongo4Test.Implementations;

public static class MongoDbContextDependencyInjectionHelper
{
    public static IServiceCollection AddMongoDbContext<TContextInterface, TContextImplementation>
        (this IServiceCollection services, MongoDbOptions options)
        where TContextInterface : class, IBaseMongoDbContext
        where TContextImplementation : BaseMongoDbContext, TContextInterface, new()
    {
        services.AddSingleton<MongoDbOptions>(_ => options);
        services.AddSingleton<IMongoDbFactory<TContextImplementation>, MongoDbFactory<TContextImplementation>>();

        services.AddScoped<TContextInterface, TContextImplementation>(provider =>
        {
            IMongoDbFactory<TContextImplementation>? factory =
                provider.GetService<IMongoDbFactory<TContextImplementation>>();
            return factory!.Create();
        });

        return services;
    }
}