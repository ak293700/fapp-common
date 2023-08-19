namespace FappCommon.Mongo4Test;

public interface IMongoDbFactory<out TDbContext>
    where TDbContext : IBaseMongoDbContext
{
    public TDbContext Create();
}