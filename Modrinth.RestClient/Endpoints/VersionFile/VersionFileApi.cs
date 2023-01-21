using Flurl.Http;
using Modrinth.RestClient.Extensions;
using Modrinth.RestClient.Models.Enums;

namespace Modrinth.RestClient.Endpoints.VersionFile;

public class VersionFileApi : IVersionFile
{
    private const string VersionFilePathSegment = "version_file";
    private FlurlClient _client;
    public VersionFileApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Version> GetVersionByHashAsync(string hash, HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        return await _client.Request(VersionFilePathSegment, hash)
            .SetQueryParam("algorithm", hashAlgorithm.ToString().ToLower()).GetJsonAsync<Version>();
    }

    /// <inheritdoc />
    public async Task DeleteVersionByHashAsync(string hash, HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        await _client.Request(VersionFilePathSegment, hash)
            .SetQueryParam("algorithm", hashAlgorithm.ToString().ToLower()).DeleteAsync();
    }
}