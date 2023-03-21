namespace Modrinth;

/// <summary>
///     Object containing options for <see cref="ModrinthClient" />
/// </summary>
public class ModrinthClientOptions
{
    /// <summary>
    ///     The token to use for requests that require authentication
    /// </summary>
    public string? ModrinthToken { get; set; }

    /// <summary>
    ///     The base URL to use for requests, you don't need to change this unless you want to use a different instance of
    ///     Modrinth (e.g. a staging server)
    ///     Default is <see cref="ModrinthClient.BaseUrl" />
    /// </summary>
    public string BaseUrl { get; set; } = ModrinthClient.BaseUrl;

    /// <summary>
    ///     User-Agent you want to use while communicating with Modrinth API, it's recommended to
    ///     set a uniquely-identifying one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)
    /// </summary>
    public string UserAgent { get; set; } = $"Modrinth.Net/{PackageVersion.GetVersion()}";

    /// <summary>
    ///     The number of times to retry a request if the rate limit is hit
    /// </summary>
    public int RateLimitRetryCount { get; set; } = 5;
}