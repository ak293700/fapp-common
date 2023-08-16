# Mongo4Test

Easily create, seed, mongo database with migration for tests

## Instructions

### YourMockDbContext

Mock your application database context.

#### Implement

- `BaseMockMongoDbContext`
- Your database context interface

### YourMongoDatabaseFixture

Singleton that creates a mongo instance that is shared between every test

#### Implement

- `BaseMongoDatabaseFixture<YourMockDbContext>`

### YourMongoDatabaseCollection

Needed to be able to share YourMongoDatabaseFixture between the tests

Should have the tag `[CollectionDefinition(UniqueName)]`

#### Implement

- `ICollectionFixture<YourMongoDatabaseFixture>`

### YourTest

A class that will do some XUnit tests

### Implement

- `BaseMongoTest<MockMongoDbContext>`

### Seeds

Create a methods to seed you database using `Fixture.ImportData`
and then call these methods in the constructor

Should have the tag `[Collection(UniqueName)]`

