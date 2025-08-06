using NUnit.Framework;
using Modrinth.Net;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Modrinth.Net.Test.ModrinthApiTests;

public static class ModrinthApiKeyProvider
{
    private static readonly Lazy<string?> ApiKeyLazy = new(LoadApiKey);

    public static string? GetApiKey() => ApiKeyLazy.Value;

    private static string? LoadApiKey()
    {
        // For local development, this reads from the secrets.json file associated with the test project.
        // 'dotnet user-secrets init' and 'dotnet user-secrets set "ModrinthApiKey" "your_key"' more info in README.md
        var config = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

        var key = config["ModrinthApiKey"];

        // For CI/CD environments, fall back to an environment variable.
        if (string.IsNullOrEmpty(key))
        {
            key = Environment.GetEnvironmentVariable("MODRINTH_API_KEY");
        }
        
        // Old environment variable name
        if (string.IsNullOrEmpty(key))
        {
            key = Environment.GetEnvironmentVariable("MODRINTH_TOKEN");
        }

        return key;
    }
}

/// <summary>
/// A central place for all test constants.
/// </summary>
public static class TestData
{
    public const string ModrinthNetTestProjectId = "8crNATHo";
    public const string TestProjectSlug = "fabric-api";
    
    public static readonly string[] ValidSha512Hashes =
    {
        "11732c4e36c3909360a24aa42a44da89048706cf10aaafa0404d7153cbc7395ff68a130f7b497828d6932740e004416b692650c3fbcc1f32babd7cb6eb9791d8",
        "bffb4f0b5347ddcf85ee5d12a6a771098b7bb61a3354ce4afa6bcd4ab88e438d2b05380481995eb58ec1a0404fdddd9bd27706f2782e828628fc9dd12208e501"
    };

    public static readonly string[] ValidSha1Hashes =
        {"43035a1c6f506285a9910bc8038d1b1b925f8dd1", "2f73c4a26c553bf0f0d2f921dd5a09ed90c515d8"};

    public static readonly string[] ValidFileNames =
    {
        "fabric-api-0.102.0+1.21.jar",
        "sodium-fabric-0.5.8+mc1.20.6.jar"
    };
    
    /// <summary>
    /// Users that have at least one project. The first ID is the primary one used for authenticated tests.
    /// </summary>
    public static readonly string[] TestUserIds = {"JZA4dW8o", "TEZXhE2U"};
    
    public static readonly FileInfo Icon = new("../../../../Modrinth.Net.Test/Assets/Icons/ModrinthNet.png");
}


/// <summary>
/// Base class for all tests that DO NOT require authentication.
/// It sets up a non-authenticated client and provides access to test data.
/// All unauthenticated test classes should inherit from this.
/// </summary>
[TestFixture]
public abstract class UnauthenticatedTestBase
{
    protected IModrinthClient NoAuthClient { get; private set; } = null!;

    [OneTimeSetUp]
    public void SetUpUnauthenticated()
    {
        var userAgent = new UserAgent
        {
            GitHubUsername = "Zechiax",
            ProjectName = "Modrinth.Net.Test",
            ProjectVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
        };

        var config = new ModrinthClientConfig
        {
            // No API token is provided
            BaseUrl = ModrinthClient.BaseUrl,
            UserAgent = userAgent.ToString()
        };

        NoAuthClient = new ModrinthClient(config);
    }

    [OneTimeTearDown]
    public void TearDownUnauthenticated()
    {
        NoAuthClient.Dispose();
    }
}

/// <summary>
/// Base class for all tests that REQUIRE authentication.
/// It inherits from the UnauthenticatedTestBase to also get a NoAuthClient.
/// It will automatically skip all inheriting tests if an API key is not found.
/// </summary>
[TestFixture]
[Category("Authenticated")]
public abstract class AuthenticatedTestBase : UnauthenticatedTestBase
{
    protected IModrinthClient Client { get; private set; } = null!;
    protected string TestUserId => TestData.TestUserIds[0];

    [OneTimeSetUp]
    public void SetUpAuthenticated()
    {
        var token = ModrinthApiKeyProvider.GetApiKey();
        
        // Ensure that the API key is set, otherwise skip the tests
        Assume.That(token, Is.Not.Null.And.Not.Empty, 
            "API key not found. Skipping authenticated tests. (Set via user secrets or MODRINTH_API_KEY environment variable, more info in README.md - https://github.com/Zechiax/Modrinth.Net?tab=readme-ov-file#testing )");

        var userAgent = new UserAgent
        {
            GitHubUsername = "Zechiax",
            ProjectName = "Modrinth.Net.Test",
            ProjectVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
        };
        
        var config = new ModrinthClientConfig
        {
            ModrinthToken = token,
            BaseUrl = ModrinthClient.BaseUrl,
            UserAgent = userAgent.ToString()
        };

        Client = new ModrinthClient(config);
    }

    [OneTimeTearDown]
    public void TearDownAuthenticated()
    {
        Client?.Dispose(); // Client might not be initialized if the assumption fails
    }
}