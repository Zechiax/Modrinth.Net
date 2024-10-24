using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Modrinth.Http;
using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.VersionFile;

[JsonSerializable(typeof(VersionFileRequest))]
[JsonSerializable(typeof(LatestVersionRequest))]
[JsonSerializable(typeof(GetMultipleVersionRequest))]
internal partial class VersionFileEndpointJsonContext : JsonSerializerContext;

// ReSharper disable InconsistentNaming
internal record VersionFileRequest(string[] hashes, string algorithm);
internal record LatestVersionRequest(string[] loaders, string[] game_versions);
internal record GetMultipleVersionRequest(string algorithm, string[] hashes, string[] loaders, string[] game_versions);
// ReSharper restore InconsistentNaming

/// <inheritdoc cref="Modrinth.Endpoints.VersionFile.IVersionFileEndpoint" />
public class VersionFileEndpoint : Endpoint, IVersionFileEndpoint
{
    private const string VersionFilePathSegment = "version_file";

    /// <inheritdoc />
    public VersionFileEndpoint(IRequester requester) : base(requester)
    {
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(VersionFilePathSegment + '/' + hash, UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            {"algorithm", hashAlgorithm.ToString().ToLower()}
        };

        parameters.AddToRequest(reqMsg);

        return await Requester.GetJsonAsync<Models.Version>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteVersionByHashAsync(string hash, HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(VersionFilePathSegment + '/' + hash, UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            {"algorithm", hashAlgorithm.ToString().ToLower()}
        };

        parameters.AddToRequest(reqMsg);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IDictionary<string, Models.Version>> GetMultipleVersionsByHashAsync(string[] hashes,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri("version_files", UriKind.Relative);

        // Info in request body application/json
        var requestBody = new VersionFileRequest(hashes, hashAlgorithm.ToString().ToLower());

        var json = JsonSerializer.Serialize(requestBody, VersionFileEndpointJsonContext.Default.VersionFileRequest);
        reqMsg.Content = new StringContent(json, Encoding.UTF8, "application/json");

        return await Requester.GetJsonAsync<IDictionary<string, Models.Version>>(reqMsg, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetLatestVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm,
        string[] loaders, string[] gameVersions, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(VersionFilePathSegment + '/' + hash + "/update", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            {"algorithm", hashAlgorithm.ToString().ToLower()}
        };

        parameters.AddToRequest(reqMsg);

        // Info in request body application/json
        var requestBody = new
        {
            loaders,
            game_versions = gameVersions
        };
        var requestBodyJson = new LatestVersionRequest(loaders, gameVersions);

        var json = JsonSerializer.Serialize(requestBody, VersionFileEndpointJsonContext.Default.LatestVersionRequest);
        reqMsg.Content = new StringContent(json, Encoding.UTF8, "application/json");

        return await Requester.GetJsonAsync<Models.Version>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IDictionary<string, Models.Version>> GetMultipleLatestVersionsByHashAsync(string[] hashes,
        HashAlgorithm hashAlgorithm,
        string[] loaders, string[] gameVersions, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri("version_files/update", UriKind.Relative);

        // Info in request body application/json
        var requestBody = new
        {
            algorithm = hashAlgorithm.ToString().ToLower(),
            hashes,
            loaders,
            game_versions = gameVersions
        };
        var requestBodyJson = new GetMultipleVersionRequest(hashAlgorithm.ToString().ToLower(), hashes, loaders, gameVersions);

        var json = JsonSerializer.Serialize(requestBody, VersionFileEndpointJsonContext.Default.GetMultipleVersionRequest);
        reqMsg.Content = new StringContent(json, Encoding.UTF8, "application/json");

        return await Requester.GetJsonAsync<IDictionary<string, Models.Version>>(reqMsg, cancellationToken)
            .ConfigureAwait(false);
    }
}