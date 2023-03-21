namespace Modrinth;

/// <summary>
///   Class that handles HTTP requests to the API
/// </summary>
public interface IRequester : IDisposable
{
    /// <summary>
    ///  Base address of the API
    /// </summary>
    Uri BaseAddress { get; }
    /// <summary>
    /// Whether the client has been disposed
    /// </summary>
    bool IsDisposed { get; }
    /// <summary>
    ///    For sending HTTP requests to the API, it returns the HTTP response
    /// </summary>
    /// <param name="request"> The HTTP request to send </param>
    /// <param name="cancellationToken"> The cancellation token </param>
    /// <returns> The HTTP response </returns>
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default);

    /// <summary>
    ///  Sends a request to the API and deserializes the response to the specified type
    /// </summary>
    /// <param name="request"> The request to send </param>
    /// <param name="cancellationToken"> The cancellation token </param>
    /// <typeparam name="T"> The type to deserialize the response to </typeparam>
    /// <returns></returns>
    Task<T> GetJsonAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default);
}