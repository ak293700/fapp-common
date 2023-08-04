## Common
IBaseMongoDbContext { 
    
}

BaseMockMongoDbContext : IBaseMongoDbContext 
{
- abstract GenerateDatabaseFromConnectionString(): BaseMockMongoDbContext;
- abstract InitCollections();
- abstract RunMigrations();
}

BaseMongoDatabaseFixture
{
- GenerateDatabase<T>(): T where T extends BaseMockMongoDbContext;
- ImportData()

}

BaseMongoTest {
- fixture: BaseMongoDatabaseFixture;


}

## Application
IApplicationMongoDbContext: IBaseMongoDbContext
{
- Users: IMongoCollection<User> ;

}


## Test
BaseTest: BaseMongoTest {
- SeedUser()
- 
}

MockMongoDbContext<>: BaseMockMongoDbContext, IApplicationMongoDbContext 
{
- override InitCollections()
- 
}
  
MongoDatabaseFixture : BaseMongoDatabaseFixture
{

}
