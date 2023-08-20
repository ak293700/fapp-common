namespace FappCommon.Mongo4Test.Implementations._4Tests;

public abstract class BaseMongoTest<TMockMongoContext> : IDisposable
    where TMockMongoContext : BaseMockMongoDbContext, new()
{
    protected readonly BaseMongoDatabaseFixture<TMockMongoContext> Fixture;
    protected readonly TMockMongoContext Context;


    protected BaseMongoTest(BaseMongoDatabaseFixture<TMockMongoContext> fixture)
    {
        Fixture = fixture;
        Context = Fixture.GenerateDatabase();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}