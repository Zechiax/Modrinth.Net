using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Modrinth.Exceptions;
using Modrinth.JsonConverters;
using Modrinth.Models.Errors;

namespace Modrinth.Http;

/// <inheritdoc cref="IRequester" />
public class Requester : IRequester
{
    private readonly ModrinthClientConfig _config;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new ColorConverter(),
            new JsonStringEnumConverter()
        }
    };

    /// <summary>
    ///     Creates a new <see cref="Requester" /> with the specified <see cref="ModrinthClientConfig" />
    /// </summary>
    /// <param name="config"> The config to use </param>
    /// <param name="httpClient"> The <see cref="HttpClient" /> to use, if null a new one will be created </param>
    public Requester(ModrinthClientConfig config, HttpClient? httpClient = null)
    {
        _config = config;
        HttpClient = httpClient ?? new HttpClient();

        BaseAddress = new Uri(config.BaseUrl);

        HttpClient.BaseAddress = BaseAddress;
        HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(config.UserAgent);

        if (!string.IsNullOrEmpty(config.ModrinthToken))
            HttpClient.DefaultRequestHeaders.Add("Authorization", config.ModrinthToken);
    }

    /// <summary>
    ///     The <see cref="HttpClient" /> used to send requests
    /// </summary>
    public HttpClient HttpClient { get; }

    /// <inheritdoc />
    public Uri BaseAddress { get; }

    /// <inheritdoc />
    public bool IsDisposed { get; private set; }

    /// <summary>
    ///     Sends a request to the API and deserializes the response to the specified type
    /// </summary>
    /// <param name="request"> The request to send </param>
    /// <param name="cancellationToken"> The cancellation token </param>
    /// <typeparam name="T"> The type to deserialize the response to </typeparam>
    /// <returns> The deserialized response </returns>
    /// <exception cref="ModrinthApiException">
    ///     Thrown when the response could not be deserialized, or the response was not
    ///     successful
    /// </exception>
    public async Task<T> GetJsonAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        if (IsDisposed)
            throw new ObjectDisposedException(nameof(Requester));

        var response = await SendAsync(request, cancellationToken).ConfigureAwait(false);

        try
        {
            var deserializedT = await JsonSerializer
                .DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(cancellationToken),
                    _jsonSerializerOptions,
                    cancellationToken)
                .ConfigureAwait(false) ?? throw new ModrinthApiException("Response could not be deserialized",
                response);

            return deserializedT;
        }
        catch (JsonException e)
        {
            throw new ModrinthApiException("Response could not be deserialized", response, innerException: e);
        }
    }


    /// <summary>
    ///     For sending HTTP requests to the API, it returns the HTTP response
    /// </summary>
    /// <param name="request"> The HTTP request to send </param>
    /// <param name="cancellationToken"> The cancellation token </param>
    /// <returns> The HTTP response </returns>
    /// <exception cref="ModrinthApiException"> Thrown when the response was not successful </exception>
    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        if (IsDisposed)
            throw new ObjectDisposedException(nameof(Requester));

        var retryCount = 0;
        while (true)
        {
            var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode) return response;

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                if (retryCount >= _config.RateLimitRetryCount)
                    throw new ModrinthApiException(
                        $"Request was rate limited and retry limit ({_config.RateLimitRetryCount}) was reached",
                        response);

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

            var message = "An error occurred while communicating with Modrinth API (HTTP " +
                          $"{(int) response.StatusCode} {response.StatusCode})";
            if (error != null) message += $": {error.Error}: {error.Description}";

            // Add request information to the exception
            message += $"{Environment.NewLine}Request: {request.Method} {request.RequestUri}";

            throw new ModrinthApiException(message, response);
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