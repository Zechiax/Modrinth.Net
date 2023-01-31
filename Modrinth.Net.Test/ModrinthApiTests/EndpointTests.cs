using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Modrinth.Net.Test.ModrinthApiTests;

[SetUpFixture]
public class EndpointTests
{
    protected static IConfigurationRoot Configuration =
        new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    protected IModrinthClient _client = null!;
    protected IModrinthClient _noAuthClient = null!;

    private static string GetToken()
    {
        var token = Environment.GetEnvironmentVariable("MODRINTH_TOKEN");
        if (string.IsNullOrEmpty(token)) token = Configuration["ModrinthApiKey"];

        if (string.IsNullOrEmpty(token)) throw new Exception("MODRINTH_TOKEN environment variable is not set.");

        return token;
    }

    [OneTimeSetUp]
    public void SetUp()
    {
        var token = GetToken();
        var userAgent = $"Zechiax/Modrinth.Net.Test/{Assembly.GetExecutingAssembly().GetName().Version}";
        _client = new ModrinthClient(url: ModrinthClient.StagingBaseUrl, userAgent: userAgent, token: token);
        _noAuthClient = new ModrinthClient(url: ModrinthClient.StagingBaseUrl, userAgent: userAgent);
    }
}