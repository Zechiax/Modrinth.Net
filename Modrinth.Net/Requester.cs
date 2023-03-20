using System.Text.Json;
using System.Text.Json.Serialization;
using Modrinth.JsonConverters;

namespace Modrinth;

public class Requester : IRequester
{
    public HttpClient HttpClient { get; }
    
    public bool IsDisposed { get; private set; }
    
    private JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new ColorConverter(),
            new JsonStringEnumConverter()
        }
    };

    public Requester(Uri baseUri, string? apiToken = null)
    {
        HttpClient = new HttpClient
        {
            BaseAddress = baseUri,
            DefaultRequestHeaders =
            {
                {"User-Agent", "Modrinth.Net"},
            }
        };
        
        if (!string.IsNullOrEmpty(apiToken)) HttpClient.DefaultRequestHeaders.Add("Authorization", apiToken);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        HttpClient.Dispose();
        IsDisposed = true;
    }
}