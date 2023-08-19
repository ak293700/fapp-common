using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDBMigrations;

namespace FappCommon.Mongo4Test;

public abstract class BaseMongoDbContext : IBaseMongoDbContext
{
    private ILoggerFactory LoggerFactory { get; set; } = null!;
    private MongoDbOptions Options { get; set; } = null!;

    public static TContext Init<TContext>(MongoDbOptions options, IConfiguration configuration)
        where TContext : BaseMongoDbContext, new()
    {
        TContext context = new TContext();
        context.Options = options;
        context.LoggerFactory = context.CreateLoggerFactory(configuration);

        IMongoDatabase? database = context.CreateClient(options.ConnectionStringName).GetDatabase(options.DatabaseName);
        context.InitializeCollections(database);

        return context;
    }

    protected abstract void InitializeCollections(IMongoDatabase database);

    private ILoggerFactory CreateLoggerFactory(IConfiguration configuration)
    {
        return Microsoft.Extensions.Logging.LoggerFactory.Create(lb =>
        {
            lb.AddConfiguration(configuration.GetSection("Logging"));
            lb.AddSimpleConsole();
        });
    }

    private MongoClient CreateClient(string connectionString)
    {
        MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.LoggingSettings = new LoggingSettings(LoggerFactory);
        return new MongoClient(settings);
    }

    public static void RunMigrations<TAssemblyType>(MongoDbOptions options, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(options.DatabaseName)
                                  ?? throw ConfigurationException.ValueNotFoundException.Instance;

        RunMigrations<TAssemblyType>(connectionString, options.DatabaseName);
    }

    public static void RunMigrations<TAssemblyType>(string connectionString, string databaseName)
    {
        new MigrationEngine()
            .UseDatabase(connectionString, databaseName)
            .UseAssemblyOfType<TAssemblyType>()
            .UseSchemeValidation(false)
            .Run();
    }


    public void Dispose()
    {
        LoggerFactory.Dispose();
        GC.SuppressFinalize(this);
    }
}