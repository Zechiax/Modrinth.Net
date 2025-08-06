using System.Text;
using System.Text.Json;
using Modrinth.Http;
using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.VersionFile;

/// <inheritdoc cref="Modrinth.Endpoints.VersionFile.IVersionFileEndpoint" />
public class VersionFileEndpoint : Endpoint, IVersionFileEndpoint
{
    private const string VersionFilePathSegment = "version_file";

    /// <inheritdoc />
    public VersionFileEndpoint(IRequester requester, ModrinthClientConfig config) : base(requester, config)
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
        var requestBody = new
        {
            hashes,
            algorithm = hashAlgorithm.ToString().ToLower()
        };

        reqMsg.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

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

        reqMsg.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

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

        reqMsg.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        return await Requester.GetJsonAsync<IDictionary<string, Models.Version>>(reqMsg, cancellationToken)
            .ConfigureAwait(false);
    }
}