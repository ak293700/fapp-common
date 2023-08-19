// See https://aka.ms/new-console-template for more information


using FappCommon.Mocks;
using FappCommon.Mongo4Test;
using Microsoft.Extensions.Configuration;
using TestProject;

MongoDbOptions options = new MongoDbOptions
{
    ConnectionStringName = "UserMongoDb",
    DatabaseName = "users"
};

IConfiguration configuration = ConfigurationMock.GetWithConnectionString("mongodb://root:password@localhost:27017/");
IMongoDbFactory<ExampleMongoContext> factory = new MongoDbFactory<ExampleMongoContext>(options, configuration);

ExampleMongoContext context = factory.Create();

await context.Randoms.InsertOneAsync(new RandomEntity
{
    Name = "Test",
    Age = 1
});