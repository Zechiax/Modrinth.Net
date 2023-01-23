using System.Reflection;

namespace Modrinth.RestClient.Test.ModrinthApiTests;

[SetUpFixture]
public class EndpointTests
{
    protected IModrinthClient _client = null!;
    protected IModrinthClient _noAuthClient = null!;
    
    [OneTimeSetUp]
    public void SetUp()
    {
        var token = "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";//Environment.GetEnvironmentVariable("MODRINTH_TOKEN");
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("MODRINTH_TOKEN environment variable is not set.");
        }
        var userAgent = $"Zechiax/Modrinth.RestClient.Test/{Assembly.GetExecutingAssembly().GetName().Version}";
        _client = new ModrinthClient(url: ModrinthClient.StagingBaseUrl, userAgent: userAgent, token: token);
        _noAuthClient = new ModrinthClient(url: ModrinthClient.StagingBaseUrl, userAgent: userAgent);
    }
}