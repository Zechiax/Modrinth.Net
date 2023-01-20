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
public class ModrinthApi 
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

    /// <inheritdoc cref="IProjectApi" />
    public IProjectApi Project { get; }
    
    /// <inheritdoc cref="ITeamApi" />
    public ITeamApi Team { get; }
    
    /// <inheritdoc cref="IUserApi" />
    public IUserApi User { get; }
    
    /// <inheritdoc cref="IVersionApi" />
    public IVersionApi Version { get; }
    
    /// <inheritdoc cref="ITagApi" />
    public ITagApi Tag { get; }
    
    /// <inheritdoc cref="IVersionFile" />
    public IVersionFile VersionFile { get; }

    #endregion

    /// <summary>
    /// Initializes new RestClient from <see cref="IModrinthApi"/>
    /// </summary>
    /// <param name="userAgent">User-Agent header you want to use while communicating with Modrinth API, it's recommended to set 'a uniquely-identifying' one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)</param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl"/></param>
    /// <returns>New RestEase RestClient from <see cref="IModrinthApi"/> interface</returns>
    private ModrinthApi(string url = BaseUrl, string userAgent = "", string authorization = "")
    {
        var client = new FlurlClient(url)
            .WithHeader("User-Agent", userAgent)
            .WithHeader("Authorization", authorization)
            .WithHeader("Accept", "application/json")
            .WithHeader("Content-Type", "application/json");

        Project = new ProjectApi(client);
        Tag = new TagApi(client);
        Team = new TeamApi(client);
        User = new UserApi(client);
    }

    public static ModrinthApi GetInstance(string url = BaseUrl, string userAgent = "", string authorization = "") {
        if (_instance == null) {
            lock (Lock)
            {
                _instance ??= new ModrinthApi(url, userAgent, authorization);
            }
        }
        return _instance;
    }
}