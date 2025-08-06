using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Modrinth.Net.Test.ModrinthApiTests;

[SetUpFixture]
public class EndpointTests
{
    protected const string ModrinthNetTestProjectId = "8crNATHo";

    protected const string TestProjectSlug = "fabric-api";

    protected static readonly string[] ValidSha512Hashes =
    {
        "11732c4e36c3909360a24aa42a44da89048706cf10aaafa0404d7153cbc7395ff68a130f7b497828d6932740e004416b692650c3fbcc1f32babd7cb6eb9791d8",
        "bffb4f0b5347ddcf85ee5d12a6a771098b7bb61a3354ce4afa6bcd4ab88e438d2b05380481995eb58ec1a0404fdddd9bd27706f2782e828628fc9dd12208e501"
    };

    protected static readonly string[] ValidSha1Hashes =
        {"43035a1c6f506285a9910bc8038d1b1b925f8dd1", "2f73c4a26c553bf0f0d2f921dd5a09ed90c515d8"};

    protected static readonly string[] ValidFileName =
    {
        "fabric-api-0.102.0+1.21.jar",
        "sodium-fabric-0.5.8+mc1.20.6.jar"
    };

    private static readonly IConfigurationRoot Configuration =
        new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    protected readonly FileInfo Icon = new("../../../../Modrinth.Net.Test/Assets/Icons/ModrinthNet.png");

    /// <summary>
    ///     Must be users that have at least one project
    ///     The first ID should be the ID of the user that will be authenticated
    /// </summary>
    protected readonly string[] TestUserIds = {"JZA4dW8o", "TEZXhE2U"};

    protected IModrinthClient Client = null!;
    protected IModrinthClient NoAuthClient = null!;

    protected IModrinthClient ProductionClientNoAuth = null!;

    protected string TestUserId => TestUserIds[0];

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

        var configAuth = new ModrinthClientConfig
        {
            ModrinthToken = token,
            BaseUrl = ModrinthClient.BaseUrl,
            UserAgent = userAgent.ToString()
        };

        var configNoAuth = new ModrinthClientConfig
        {
            BaseUrl = ModrinthClient.BaseUrl,
            UserAgent = userAgent.ToString()
        };

        var productionConfig = new ModrinthClientConfig
        {
            BaseUrl = ModrinthClient.BaseUrl,
            UserAgent = userAgent.ToString()
        };

        Client = new ModrinthClient(configAuth);
        NoAuthClient = new ModrinthClient(configNoAuth);

        ProductionClientNoAuth = new ModrinthClient(productionConfig);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        Client.Dispose();
        NoAuthClient.Dispose();
        ProductionClientNoAuth.Dispose();
    }
}