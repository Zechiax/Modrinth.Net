using Modrinth.Http;

namespace Modrinth.Endpoints;

/// <summary>
///     Base class for all endpoints
/// </summary>
public abstract class Endpoint
{
    /// <summary>
    ///     Creates a new <see cref="Endpoint" /> with the given <see cref="IRequester" /> and <see cref="ModrinthClientConfig" />
    /// </summary>
    /// <param name="requester">The <see cref="IRequester" /> to use for requests</param>
    /// <param name="config">The <see cref="ModrinthClientConfig" /> to use for configuration</param>
    protected Endpoint(IRequester requester, ModrinthClientConfig config)
    {
        Requester = requester;
        Config = config;
    }

    /// <summary>
    ///     The requester used to make requests
    /// </summary>
    protected IRequester Requester { get; }

    /// <summary>
    ///     Sends a request and disposes of the response when the caller does not need it.
    ///     Use this for fire-and-forget API calls (DELETE, POST, PATCH without return values).
    /// </summary>
    /// <param name="request">The request to send</param>
    /// <param name="cancellationToken">The cancellation token</param>
    protected async Task SendWithoutResponseAsync(HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        using var response = await Requester.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    ///     The configuration used by the endpoint
    /// </summary>
    protected ModrinthClientConfig Config { get; }
}