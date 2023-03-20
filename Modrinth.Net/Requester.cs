using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Modrinth.Exceptions;
using Modrinth.JsonConverters;
using Modrinth.Models.Errors;

namespace Modrinth;

public class Requester : IRequester
{
    private const int RetryLimit = 5;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new ColorConverter(),
            new JsonStringEnumConverter()
        }
    };

    public Requester(Uri baseUri, string userAgent, string? apiToken = null)
    {
        BaseAddress = baseUri;
        HttpClient = new HttpClient
        {
            BaseAddress = baseUri,
            DefaultRequestHeaders =
            {
                {"User-Agent", userAgent}
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
            .DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(cancellationToken), _jsonSerializerOptions,
                cancellationToken)
            .ConfigureAwait(false) ?? throw new ModrinthApiException("Response could not be deserialized",
            response.StatusCode, response.Content, null);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        var retryCount = 0;
        while (true)
        {
            var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode) return response;

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                if (retryCount >= RetryLimit)
                    throw new ModrinthApiException(
                        $"Request was rate limited and retry limit ({RetryLimit}) was reached", response.StatusCode,
                        response.Content, null);

                if (response.Headers.TryGetValues("X-Ratelimit-Reset", out var resetValues))
                {
                    var resetInSeconds = int.Parse(resetValues.First());
                    // Reset time + 1 seconds to be sure
                    var resetTime = DateTimeOffset.Now.AddSeconds(resetInSeconds + 1);
                    await Task.Delay(resetTime - DateTimeOffset.Now, cancellationToken).ConfigureAwait(false);

                    retryCount++;
                    // We need to create a new request message because the old one has already been sent
                    request = CopyRequest(request);
                    continue;
                }
            }

            // Error handling
            ResponseError? error = null;
            try
            {
                error = await JsonSerializer.DeserializeAsync<ResponseError>(
                        await response.Content.ReadAsStreamAsync(cancellationToken), _jsonSerializerOptions,
                        cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (JsonException)
            {
                // Ignore
            }

            var message = "An error occurred while communicating with Modrinth API";
            if (error != null) message += $": {error.Error}: {error.Description}";

            throw new ModrinthApiException(message, response.StatusCode, response.Content, null);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        HttpClient.Dispose();
        IsDisposed = true;
    }

    private static HttpRequestMessage CopyRequest(HttpRequestMessage request)
    {
        var newRequest = new HttpRequestMessage(request.Method, request.RequestUri);
        newRequest.Content = request.Content;
        newRequest.Method = request.Method;
        newRequest.Version = request.Version;
        foreach (var header in request.Headers) newRequest.Headers.Add(header.Key, header.Value);
        newRequest.VersionPolicy = request.VersionPolicy;
        return newRequest;
    }
}