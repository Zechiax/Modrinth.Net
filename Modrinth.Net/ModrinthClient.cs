using Modrinth.Endpoints.Miscellaneous;
using Modrinth.Endpoints.Project;
using Modrinth.Endpoints.Tag;
using Modrinth.Endpoints.Team;
using Modrinth.Endpoints.User;
using Modrinth.Endpoints.Version;
using Modrinth.Http;

namespace Modrinth;

/// <summary>
///     A client for the Modrinth API
/// </summary>
public class ModrinthClient : IModrinthClient
{
    /// <summary>
    ///     API Url of the production server
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string BaseUrl = "https://api.modrinth.com/v2/";

    /// <summary>
    ///     API Url of the staging server
    /// </summary>
    public const string StagingBaseUrl = "https://staging-api.modrinth.com/v2/";

    private readonly ModrinthClientConfig _config;

    private readonly IRequester _requester;

    /// <inheritdoc />
    [Obsolete("Use the constructor that takes a ModrinthClientConfig instead")]
    public ModrinthClient(UserAgent userAgent, string? token = null, string url = BaseUrl)
        : this(userAgent.ToString(), token, url)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ModrinthClient" /> class.
    /// </summary>
    /// <param name="token">Authentication token for Authenticated requests</param>
    /// <param name="userAgent">
    ///     User-Agent header you want to use while communicating with Modrinth API, it's recommended to
    ///     set a uniquely-identifying one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)
    /// </param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl" /></param>
    /// <returns></returns>
    [Obsolete("Use the constructor that takes a ModrinthClientConfig instead")]
    public ModrinthClient(string userAgent, string? token = null, string url = BaseUrl) : this(
        new ModrinthClientConfig
        {
            ModrinthToken = token,
            BaseUrl = url,
            UserAgent = userAgent
        })
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ModrinthClient" /> class.
    ///     Uses the default configuration.
    /// </summary>
    public ModrinthClient() : this(new ModrinthClientConfig())
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ModrinthClient" /> class.
    /// </summary>
    /// <param name="config"> Configuration for the client </param>
    /// <param name="httpClient">
    ///     Custom <see cref="HttpClient" /> to use for requests, if null a new one will be created, some
    ///     options like user-agent or base url will be overwritten by the config
    /// </param>
    /// <exception cref="ArgumentException"> Thrown when the User-Agent is empty </exception>
    public ModrinthClient(ModrinthClientConfig config, HttpClient? httpClient = null)
    {
        if (string.IsNullOrEmpty(config.UserAgent))
            throw new ArgumentException("User-Agent cannot be empty", nameof(config.UserAgent));

        _config = config;
        _requester = new Requester(config, httpClient);

        Project = new ProjectEndpoint(_requester);
        Tag = new TagEndpoint(_requester);
        Team = new TeamEndpoint(_requester);
        User = new UserEndpoint(_requester);
        Version = new VersionFileEndpoint(_requester);
        VersionFileEndpoint = new Endpoints.VersionFile.VersionFileEndpoint(_requester);
        Miscellaneous = new MiscellaneousEndpoint(_requester);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (IsDisposed || _requester.IsDisposed) return;
        _requester.Dispose();
        IsDisposed = true;
        GC.SuppressFinalize(this);
    }

    #region Endpoints

    /// <inheritdoc />
    public bool IsDisposed { get; private set; }

    /// <inheritdoc />
    public IProjectEndpoint Project { get; }

    /// <inheritdoc />
    public ITeamEndpoint Team { get; }

    /// <inheritdoc />
    public IUserEndpoint User { get; }

    /// <inheritdoc />
    public IVersionFileEndpoint Version { get; }

    /// <inheritdoc />
    public ITagEndpoint Tag { get; }

    /// <inheritdoc />
    public Endpoints.VersionFile.IVersionFileEndpoint VersionFileEndpoint { get; }

    /// <inheritdoc />
    public IMiscellaneousEndpoint Miscellaneous { get; }

    #endregion
}