using System.Reflection;
using Microsoft.Extensions.Configuration;
using Modrinth.Client;

namespace Modrinth.Net.Test.ModrinthApiTests;

[SetUpFixture]
public class EndpointTests
{
    protected const string TestProjectSlug = "gravestones";
    protected const string ModrinthNetTestProjectId = "jKePI2WR";
    protected const string ModrinthNetTestUploadedVersionId = "dJIVHDfy";

    protected static readonly IConfigurationRoot Configuration =
        new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    protected readonly FileInfo Icon = new("../../../../Modrinth.Net.Test/Assets/Icons/ModrinthNet.png");

    protected IModrinthClient Client = null!;
    protected IModrinthClient NoAuthClient = null!;

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

        Client = new ModrinthClient(url: ModrinthClient.StagingBaseUrl, userAgent: userAgent, token: token);
        NoAuthClient = new ModrinthClient(url: ModrinthClient.StagingBaseUrl, userAgent: userAgent);
    }
}