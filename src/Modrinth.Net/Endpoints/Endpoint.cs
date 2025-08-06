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
    ///     The configuration used by the endpoint
    /// </summary>
    protected ModrinthClientConfig Config { get; }
}