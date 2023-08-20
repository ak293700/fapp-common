using FappCommon.Mongo4Test.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FappCommon.Mongo4Test.Implementations;

public class MongoDbFactory<TDbContext> : IMongoDbFactory<TDbContext>
    where TDbContext : BaseMongoDbContext, new()
{
    private readonly IConfiguration _configuration;
    private readonly MongoDbOptions _options;

    public MongoDbFactory(MongoDbOptions options, IConfiguration configuration)
    {
        _configuration = configuration;
        _options = options;
    }

    public TDbContext Create()
    {
        TDbContext context = BaseMongoDbContext.Init<TDbContext>(_options, _configuration);
        return context;
    }
}