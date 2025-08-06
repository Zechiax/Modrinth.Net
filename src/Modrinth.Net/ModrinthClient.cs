using Modrinth.Endpoints.Miscellaneous;
using Modrinth.Endpoints.Notifications;
using Modrinth.Endpoints.Project;
using Modrinth.Endpoints.Tag;
using Modrinth.Endpoints.Team;
using Modrinth.Endpoints.User;
using Modrinth.Endpoints.Version;
using Modrinth.Endpoints.VersionFile;
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
    ///     Custom <see cref="HttpClient" /> to use for requests, if null a new one will be created, the config
    ///     will overwrite some options like user-agent or base url
    /// </param>
    /// <exception cref="ArgumentException">Thrown when the configuration is invalid, e.g., User-Agent is empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when the configuration contains invalid numeric values, e.g.,
    ///     MaxConcurrentRequests is less than or equal to zero
    /// </exception>
    public ModrinthClient(ModrinthClientConfig config, HttpClient? httpClient = null)
    {
        config.Validate();

        _config = config;
        _requester = new Requester(config, httpClient);

        Project = new ProjectEndpoint(_requester);
        Tag = new TagEndpoint(_requester);
        Team = new TeamEndpoint(_requester);
        User = new UserEndpoint(_requester);
        Version = new VersionEndpoint(_requester);
        VersionFile = new VersionFileEndpoint(_requester);
        Miscellaneous = new MiscellaneousEndpoint(_requester);
        Notification = new NotificationsEndpoint(_requester);
    }

    /// <summary>
    ///     Disposes the underlying <see cref="HttpClient" /> and other resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Disposes the resources used by the <see cref="ModrinthClient" />.
    /// </summary>
    /// <param name="disposing">
    ///     Indicates whether the method was called from the <see cref="Dispose()" /> method or from the finalizer.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            _requester.Dispose();
        }
        
        IsDisposed = true;
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
    public IVersionEndpoint Version { get; }

    /// <inheritdoc />
    public ITagEndpoint Tag { get; }

    /// <inheritdoc />
    public IVersionFileEndpoint VersionFile { get; }

    /// <inheritdoc />
    public IMiscellaneousEndpoint Miscellaneous { get; }

    /// <inheritdoc />
    public INotificationsEndpoint Notification { get; }

    #endregion
}