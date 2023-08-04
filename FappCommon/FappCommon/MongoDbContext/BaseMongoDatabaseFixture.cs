using Mongo2Go;

namespace FappCommon.MongoDbContext;

public abstract class BaseMongoDatabaseFixture<TMockMongoContext> : IDisposable
    where TMockMongoContext : BaseMockMongoDbContext, new()
{
    private readonly MongoDbRunner _runner;

    public BaseMongoDatabaseFixture()
    {
        _runner = MongoDbRunner.Start();
    }

    public abstract TMockMongoContext GenerateDatabase();

    public void ImportData(string databaseName, string collectionName, string inputFile)
    {
        _runner.Import(databaseName, collectionName, inputFile, false);
    }

    public void Dispose()
    {
        _runner.Dispose();
        GC.SuppressFinalize(this);
    }
}