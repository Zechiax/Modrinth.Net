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
            new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower)
        },
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private readonly SemaphoreSlim _semaphore;

    /// <summary>
    ///     Creates a new <see cref="Requester" /> with the specified <see cref="ModrinthClientConfig" />
    /// </summary>
    /// <param name="config"> The config to use </param>
    /// <param name="httpClient"> The <see cref="HttpClient" /> to use, if null a new one will be created </param>
    public Requester(ModrinthClientConfig config, HttpClient? httpClient = null)
    {
        _config = config;
        _semaphore = new SemaphoreSlim(_config.MaxConcurrentRequests);

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
            throw new ModrinthApiException(
                $"Response could not be deserialize for Path {e.Path} | URL {request.RequestUri} | Response {response.StatusCode} | Data {await response.Content.ReadAsStringAsync(cancellationToken)}",
                response, innerException: e);
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

        // Throttle the number of concurrent requests
        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var retryCount = 0;
            while (true)
            {
                HttpResponseMessage response;
                try
                {
                    response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
                }
                catch (HttpRequestException e)
                {
                    throw new ModrinthApiException("An error occurred while sending the request.", innerException: e);
                }

                // Response was successful
                if (response.IsSuccessStatusCode) return response;

                // Handle rate-limiting (429 Too Many Requests).
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    if (cancellationToken.IsCancellationRequested)
                        throw new OperationCanceledException(
                            "The operation was canceled",
                            cancellationToken);

                    if (retryCount >= _config.RateLimitRetryCount)
                        throw new ModrinthApiException(
                            $"Request was rate limited and retry limit ({_config.RateLimitRetryCount}) was reached",
                            response);

                    // Wait for the duration specified by the 'X-Ratelimit-Reset' header.
                    if (response.Headers.TryGetValues("X-Ratelimit-Reset", out var resetValues))
                    {
                        var resetInSeconds = int.Parse(resetValues.First());
                        // Add a small buffer (e.g., 500 ms) to be safe.
                        await Task.Delay(TimeSpan.FromSeconds(resetInSeconds).Add(TimeSpan.FromMilliseconds(500)),
                            cancellationToken).ConfigureAwait(false);

                        retryCount++;
                        // A request message cannot be sent twice, so we must create a copy for the retry.
                        request = CopyRequest(request);
                        continue; // Continue to the next iteration of the while loop to retry the request.
                    }
                }
                
                // If we reach here, the response was not successful and not rate-limited.
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
                    // The error response wasn't in the expected format; proceed with a generic error.
                }

                var message = "An error occurred while communicating with Modrinth API (HTTP " +
                              $"{(int)response.StatusCode} {response.StatusCode})";
                if (error != null) message += $": {error.Error}: {error.Description}";

                message += $"{Environment.NewLine}Request: {request.Method} {request.RequestUri}";

                throw new ModrinthApiException(message, response, error);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    /// <summary>
    ///      Disposes the underlying resources.
    /// </summary>
    /// Indicates whether the method was called from the <see cref="Dispose()" /> method or from the finalizer.
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }
        
        if (disposing)
        {
            HttpClient.Dispose();
            _semaphore.Dispose();
        }
        
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