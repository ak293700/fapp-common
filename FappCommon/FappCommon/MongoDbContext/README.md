## Common
IBaseMongoDbContext { // OK
    
}

BaseMockMongoDbContext : IBaseMongoDbContext // OK 
{
- abstract GenerateDatabaseFromConnectionString(): BaseMockMongoDbContext;
- abstract InitCollections();
- abstract RunMigrations();
}

BaseMongoDatabaseFixture // OK
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

MockMongoDbContext<>: BaseMockMongoDbContext, IApplicationMongoDbContext // OK
{
- override InitCollections()
- 
}
  
MongoDatabaseFixture : BaseMongoDatabaseFixture // OK
{

}
