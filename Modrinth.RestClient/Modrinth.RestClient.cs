using Flurl.Http;
using Modrinth.RestClient.Endpoints.Project;
using Modrinth.RestClient.Endpoints.Tag;
using Modrinth.RestClient.Endpoints.Team;
using Modrinth.RestClient.Endpoints.User;
using Modrinth.RestClient.Endpoints.VersionFile;

namespace Modrinth.RestClient;


/// <summary>
/// Base for creating new clients using RestEase from <see cref="IModrinthApi"/> interface
/// </summary>
public class ModrinthApi : IModrinthApi
{
    private static ModrinthApi? _instance;
    private static readonly object Lock = new();

    /// <summary>
    /// API Url of the production server
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string BaseUrl = "https://api.modrinth.com/v2";

    /// <summary>
    /// API Url of the staging server
    /// </summary>
    public const string StagingBaseUrl = "https://staging-api.modrinth.com/v2";

    #region Endpoints

    /// <inheritdoc />
    public IProjectApi Project { get; }
    
    /// <inheritdoc />
    public ITeamApi Team { get; }
    
    /// <inheritdoc />
    public IUserApi User { get; }
    
    /// <inheritdoc />
    public IVersionApi Version { get; }
    
    /// <inheritdoc />
    public ITagApi Tag { get; }
    
    /// <inheritdoc />
    public IVersionFile VersionFile { get; }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ModrinthApi"/> class.
    /// </summary>
    /// <param name="token">Authentication token for Authenticated requests</param>
    /// <param name="userAgent">User-Agent header you want to use while communicating with Modrinth API, it's recommended to set a uniquely-identifying one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)</param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl"/></param>
    /// <returns></returns>
    private ModrinthApi(string token = "", string userAgent = "", string url = BaseUrl)
    {
        var client = new FlurlClient(url)
            .WithHeader("User-Agent", userAgent)
            .WithHeader("Authorization", token)
            .WithHeader("Accept", "application/json")
            .WithHeader("Content-Type", "application/json");

        Project = new ProjectApi(client);
        Tag = new TagApi(client);
        Team = new TeamApi(client);
        User = new UserApi(client);
        Version = new VersionApi(client);
        VersionFile = new VersionFileApi(client);
    }

    /// <summary>
    /// Returns a new instance of <see cref="ModrinthApi"/> if it's not already created, otherwise returns the existing one
    /// </summary>
    /// <param name="token">Authentication token for Authenticated requests</param>
    /// <param name="userAgent">User-Agent header you want to use while communicating with Modrinth API, it's recommended to set a uniquely-identifying one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)</param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl"/></param>
    /// <returns></returns>
    public static IModrinthApi GetInstance(string userAgent = "", string token = "" , string url = BaseUrl) {
        if (_instance == null) {
            lock (Lock)
            {
                if (string.IsNullOrEmpty(userAgent))
                {
                    throw new ArgumentException("User-Agent cannot be empty", nameof(userAgent));
                }

                _instance ??= new ModrinthApi(token: token, userAgent: userAgent, url: url);
            }
        }
        return _instance;
    }
}