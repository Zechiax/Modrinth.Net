using System.Reflection;

namespace Modrinth.RestClient.Test;

[SetUpFixture]
public class EndpointTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        var token = Environment.GetEnvironmentVariable("MODRINTH_TOKEN");
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("MODRINTH_TOKEN environment variable is not set.");
        }
        var userAgent = $"Zechiax/Modrinth.RestClient.Test/{Assembly.GetExecutingAssembly().GetName().Version}";
        ModrinthApi.GetInstance(url: ModrinthApi.StagingBaseUrl, userAgent: userAgent , authorization: token);
    }
}