using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Modrinth.Net.Test.ModrinthApiTests;

[SetUpFixture]
public class EndpointTests
{
    protected const string ModrinthNetTestProjectId = "jKePI2WR";
    protected const string ModrinthNetTestUploadedVersionId = "dJIVHDfy";

    protected const string TestProjectSlug = "gravestones";

    protected static readonly string[] ValidSha512Hashes =
    {
        "f62b94dbdb7ec79c1cc9f7f01a07b72828e77d22426552cd876d0fa8ba2a446efaecaea262ed481b2f77fa57063a3bdcd1c5febb8ae97a766d82abf3eb9ee198",
        "88014dd2d5fe7a259648eb716690f282e220556665ff20746bfa1f1d5a5f480e92fa94e07ea64db700c66720977d0c2147954efbf2c0c48bf38e419f04820453"
    };

    protected static readonly string[] ValidSha1Hashes =
        {"7e64ba677dbae046389b63a4db324284355987db", "fde0d8156d8d46b2b0f9d09906ef7f83ce712517"};

    private static readonly IConfigurationRoot Configuration =
        new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    protected readonly FileInfo Icon = new("../../../../Modrinth.Net.Test/Assets/Icons/ModrinthNet.png");

    /// <summary>
    ///     Must be users that have at least one project
    ///     The first ID should be the ID of the user that will be authenticated
    /// </summary>
    protected readonly string[] TestUserIds = {"MaovZxtD", "5XoMa0C4"};

    protected IModrinthClient Client = null!;
    protected IModrinthClient NoAuthClient = null!;

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
            BaseUrl = ModrinthClient.StagingBaseUrl,
            UserAgent = userAgent.ToString()
        };

        var configNoAuth = new ModrinthClientConfig
        {
            BaseUrl = ModrinthClient.StagingBaseUrl,
            UserAgent = userAgent.ToString()
        };

        Client = new ModrinthClient(configAuth);
        NoAuthClient = new ModrinthClient(configNoAuth);
    }
}