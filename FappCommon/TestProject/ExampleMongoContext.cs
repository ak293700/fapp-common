using FappCommon.Mongo4Test;
using MongoDB.Driver;

namespace TestProject;

public class ExampleMongoContext : BaseMongoDbContext
{
    public IMongoCollection<RandomEntity> Randoms { get; private set; } = null!;

    public const string UserCollectionName = "random";

    protected override void InitializeCollections(IMongoDatabase database)
    {
        Randoms = database.GetCollection<RandomEntity>(UserCollectionName);
    }
}