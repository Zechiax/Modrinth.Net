namespace Modrinth;

public interface IRequester : IDisposable
{
    bool IsDisposed { get; }
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default); 
}