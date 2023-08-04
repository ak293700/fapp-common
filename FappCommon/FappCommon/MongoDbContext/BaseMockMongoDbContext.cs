using MongoDB.Driver;

namespace FappCommon.MongoDbContext;

public abstract class BaseMockMongoDbContext: IBaseMongoDbContext
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
        
        mock.RunMigrations(connectionString);
        mock.InitCollections(database);

        return mock;
    }


    protected abstract void InitCollections(IMongoDatabase database);
    protected abstract void RunMigrations(string connectionString);
}