using Microsoft.Extensions.Configuration;

namespace FappCommon.Mocks;

public static class ConfigurationMock
{
    public static IConfiguration GetWithConnectionString(string connectionString)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:UserMongoDb", connectionString },
                { "Logging", "{\"LogLevel\": {\"Default\": \"Information\"}}" }
            }!)
            .Build();
    }

    public static IConfiguration GetCustom(Dictionary<string, string?> customSettings)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(customSettings)
            .Build();
    }
}