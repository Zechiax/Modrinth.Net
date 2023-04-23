using System.Text;
using System.Text.Json;
using Modrinth.Extensions;
using Modrinth.Http;
using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.VersionFile;

public class VersionFileEndpoint : IVersionFileEndpoint
{
    private const string VersionFilePathSegment = "version_file";
    private readonly IRequester _client;

    public VersionFileEndpoint(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(VersionFilePathSegment + '/' + hash, UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            {"algorithm", hashAlgorithm.ToString().ToLower()}
        };

        parameters.AddToRequest(reqMsg);

        return await _client.GetJsonAsync<Models.Version>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteVersionByHashAsync(string hash, HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(VersionFilePathSegment + '/' + hash, UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            {"algorithm", hashAlgorithm.ToString().ToLower()}
        };

        parameters.AddToRequest(reqMsg);

        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }
}