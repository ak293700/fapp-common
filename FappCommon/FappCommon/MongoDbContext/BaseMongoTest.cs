namespace FappCommon.MongoDbContext;

public abstract class BaseMongoTest<TMockMongoContext>/* : IDisposable*/
    where TMockMongoContext : BaseMockMongoDbContext, new()
{
    protected readonly BaseMongoDatabaseFixture<TMockMongoContext> Fixture;
    protected readonly TMockMongoContext DbContext;


    protected BaseMongoTest(BaseMongoDatabaseFixture<TMockMongoContext> fixture)
    {
        Fixture = fixture;
        DbContext = Fixture.GenerateDatabase();
        // SeedAsync().Wait();
    }


    // public void Dispose()
    // {
    //     _context.Dispose();
    // }
}