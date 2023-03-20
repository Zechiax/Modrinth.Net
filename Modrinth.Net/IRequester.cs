namespace Modrinth;

public interface IRequester : IDisposable
{
    Uri BaseAddress { get; }
    bool IsDisposed { get; }
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default);

    Task<T> GetJsonAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default);
}