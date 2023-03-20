using System.Text.Json;
using System.Text.Json.Serialization;
using Modrinth.Exceptions;
using Modrinth.JsonConverters;

namespace Modrinth;

public class Requester : IRequester
{
    public HttpClient HttpClient { get; }

    public Uri BaseAddress { get; }
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
        BaseAddress = baseUri;
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
    
    public async Task<T> GetJsonAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var response = await SendAsync(request, cancellationToken).ConfigureAwait(false);
        // TODO: Add error handling, if the response is not successful and the content cannot be deserialized
        if (!response.IsSuccessStatusCode)
        {
            throw new ModrinthApiException("Error: " + response.StatusCode + " " + response.ReasonPhrase + "" +
                                           await response.Content.ReadAsStringAsync() + "" +
                                           $"{response.RequestMessage.RequestUri} base url: {BaseAddress}"
                , response.StatusCode, response.Content, null);
        }

        return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    public void Dispose()
    {
        HttpClient.Dispose();
        IsDisposed = true;
    }
}