using Modrinth.Http;

namespace Modrinth.Endpoints;

/// <summary>
///   Base class for all endpoints
/// </summary>
public abstract class Endpoint
{
    /// <summary>
    ///  The requester used to make requests
    /// </summary>
    protected IRequester Requester { get; }
    
    /// <summary>
    ///  Creates a new <see cref="Endpoint" /> with the given <see cref="IRequester" />
    /// </summary>
    /// <param name="requester"> The <see cref="IRequester" /> to use for requests </param>
    protected Endpoint(IRequester requester)
    {
        Requester = requester;
    }
}