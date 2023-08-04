using MongoDB.Driver;

namespace FappCommon.MongoDbContext.Mock;

public abstract class BaseMockMongoDbContext
{
    public string DatabaseName { get; private init; } = null!;
    public IMongoClient Client { get; private init; } = null!;

    public static TMockMongoContext GenerateDatabaseFromConnectionString<TMockMongoContext>(string connectionString)
        where TMockMongoContext : BaseMockMongoDbContext, new()
    {
        MongoClient client = new MongoClient(connectionString);
        string databaseName = Guid.NewGuid().ToString();
        IMongoDatabase database = client.GetDatabase(databaseName);
        
        TMockMongoContext mock = new TMockMongoContext
        {
            Client = client,
            DatabaseName = databaseName,
        };
        
        mock.InitCollections(database);
        mock.RunMigrations(connectionString);

        return mock;
    }
    
    
    public abstract void InitCollections(IMongoDatabase database);
    public abstract void RunMigrations(string connectionString);
}