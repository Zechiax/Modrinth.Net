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
        "bace1768e893e60574dcb1155e057a2fd0da3f3400c862a93c37dfe4d7908de1739b3b72190353f1a2a981ec18e1175d1dcf2109f0fb64ffdc73c45629a4cf55",
        "3651e6cdb1dbb46580f27386caa01c88d28e51a5feec57cc435be73be25d718da9a719798e2b887e0fde14b6eaa970f30ee7220ff1f81489acd6174840c34d06"
    };

    protected static readonly string[] ValidSha1Hashes =
        {"8b0a4139d9e82300b7aac82f2402ec3497991c52", "429eb439f0835e31fbbfd00234ef2daa8ecc8a87"};

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