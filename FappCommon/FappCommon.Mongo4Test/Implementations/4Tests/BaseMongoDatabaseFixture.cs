using Mongo2Go;
using Mongo2Go.Helper;

namespace FappCommon.Mongo4Test.Implementations._4Tests;

public abstract class BaseMongoDatabaseFixture<TMockMongoContext> : IDisposable
    where TMockMongoContext : BaseMockMongoDbContext, new()
{
    protected readonly MongoDbRunner Runner;

    // ReSharper disable once StaticMemberInGenericType
    private static string? _mongoImportBinaryLocation = null!;

    protected BaseMongoDatabaseFixture()
    {
        Runner = MongoDbRunner.Start();

        if (_mongoImportBinaryLocation is not null)
            return;

        // Find the mongo binary so we can import things
        MongoBinaryLocator mongoBinaryLocator = new(null, null);
        _mongoImportBinaryLocation = Path.Combine("{0}", "{1}")
            .Formatted(mongoBinaryLocator.Directory, MongoDbDefaults.MongoImportExecutable);
    }


    public TMockMongoContext GenerateDatabase()
    {
        return BaseMockMongoDbContext
            .GenerateDatabaseFromConnectionString<TMockMongoContext>(Runner.ConnectionString);
    }

    public void ImportData(string databaseName, string collectionName, string inputFile)
    {
        Runner.Import(databaseName, collectionName, inputFile, false);
    }

    public void Dispose()
    {
        Runner.Dispose();
        GC.SuppressFinalize(this);
    }
}