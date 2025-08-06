namespace Modrinth;

/// <summary>
///     Object containing options for <see cref="ModrinthClient" />
/// </summary>
public class ModrinthClientConfig
{
    /// <summary>
    ///     The token to use for requests that require authentication
    /// </summary>
    public string? ModrinthToken { get; set; }

    /// <summary>
    ///     The base URL to use for requests, you don't need to change this unless you want to use a different instance of
    ///     Modrinth (e.g., a staging server)
    ///     Default is <see cref="ModrinthClient.BaseUrl" />
    /// </summary>
    public string BaseUrl { get; set; } = ModrinthClient.BaseUrl;

    /// <summary>
    ///     User-Agent you want to use while communicating with Modrinth API, it's recommended to
    ///     set a uniquely identifying one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)
    /// </summary>
    public string UserAgent { get; set; } = $"Modrinth.Net/{PackageVersion.GetVersion()}";

    /// <summary>
    ///     The number of times to retry a request if the rate limit is hit
    /// </summary>
    public int RateLimitRetryCount { get; set; } = 5;

    /// <summary>
    ///     The maximum number of concurrent requests to send
    /// </summary>
    public int MaxConcurrentRequests { get; set; } = 10;

    /// <summary>
    ///     Validates the configuration options.
    ///     Throws an <see cref="ArgumentException" /> if any of the required properties are null or empty,
    ///     or an <see cref="ArgumentOutOfRangeException" /> if any of the numeric properties are out of range.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(UserAgent))
            throw new ArgumentException("User-Agent cannot be null or empty.", nameof(UserAgent));

        if (string.IsNullOrWhiteSpace(BaseUrl))
            throw new ArgumentException("BaseUrl cannot be null or empty.", nameof(BaseUrl));

        if (RateLimitRetryCount < 0)
            throw new ArgumentOutOfRangeException(nameof(RateLimitRetryCount),
                "RateLimitRetryCount must be non-negative.");

        if (MaxConcurrentRequests <= 0)
            throw new ArgumentOutOfRangeException(nameof(MaxConcurrentRequests),
                "MaxConcurrentRequests must be greater than zero.");
    }
}