namespace FappCommon.Mongo4Test.Interfaces;

public interface IMongoDbFactory<out TDbContext>
    where TDbContext : IBaseMongoDbContext
{
    public TDbContext Create();
}