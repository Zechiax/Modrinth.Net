using System.Reflection;
using Microsoft.Extensions.Configuration;
using Modrinth.Client;

namespace Modrinth.Net.Test.ModrinthApiTests;

[SetUpFixture]
public class EndpointTests
{
    protected const string TestProjectSlug = "gravestones";
    protected const string ModrinthNetTestProjectId = "8tXTnRfF";

    protected static readonly IConfigurationRoot Configuration =
        new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    protected readonly FileInfo Icon = new("../../../../Modrinth.Net.Test/Assets/Icons/ModrinthNet.png");

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
        var userAgent = new UserAgent
        {
            GitHubUsername = "Zechiax",
            ProjectName = "Modrinth.Net.Test",
            ProjectVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
        };

        _client = new ModrinthClient(url: ModrinthClient.StagingBaseUrl, userAgent: userAgent, token: token);
        _noAuthClient = new ModrinthClient(url: ModrinthClient.StagingBaseUrl, userAgent: userAgent);
    }
}