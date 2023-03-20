using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Modrinth.Exceptions;
using Modrinth.JsonConverters;
using Modrinth.Models.Errors;

namespace Modrinth;

public class Requester : IRequester
{
    private int _requestLimit;
    private int _requestCount;
    private int _timeLeft;
    
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
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
                {"User-Agent", "Modrinth.Net"}
            }
        };

        if (!string.IsNullOrEmpty(apiToken)) HttpClient.DefaultRequestHeaders.Add("Authorization", apiToken);
    }

    public HttpClient HttpClient { get; }

    public Uri BaseAddress { get; }
    public bool IsDisposed { get; private set; }

    public async Task<T> GetJsonAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var response = await SendAsync(request, cancellationToken).ConfigureAwait(false);

        return await JsonSerializer
            .DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(cancellationToken), _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false) ?? throw new ModrinthApiException("Response could not be deserialized", response.StatusCode, response.Content, null);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        // We need to atomically check if the number of requests we have left is greater than 0
        // and if it is, decrement it. If it is not, we need to wait until it is.
        
        if (_requestLimit <= 0)
        {
            await Task.Delay(_timeLeft, cancellationToken).ConfigureAwait(false);
        }

        Interlocked.Decrement(ref _requestCount);
        var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        
        SetRateLimitFromHeaders(response.Headers);

        if (response.IsSuccessStatusCode) return response;
        
        // Error handling
        var error = await JsonSerializer
            .DeserializeAsync<ResponseError>(await response.Content.ReadAsStreamAsync(cancellationToken), _jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        throw new ModrinthApiException(
            $"An error occurred while communicating with Modrinth API: {response.ReasonPhrase}: {error?.Error}: {error?.Description}",
            response.StatusCode,
            response.Content, null);
    }
    
    private void SetRateLimitFromHeaders(HttpHeaders headers)
    {
        if (headers.TryGetValues("X-RateLimit-Limit", out var limit))
        {
            _requestLimit = int.Parse(limit.First());
        }

        if (headers.TryGetValues("X-RateLimit-Remaining", out var remaining))
        {
            _requestCount = int.Parse(remaining.First());
        }

        if (headers.TryGetValues("X-RateLimit-Reset", out var reset))
        {
            _timeLeft = int.Parse(reset.First());
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        HttpClient.Dispose();
        IsDisposed = true;
    }
}