using Flurl.Http;
using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.VersionFile;

public class VersionFileApi : IVersionFile
{
    private const string VersionFilePathSegment = "version_file";
    private readonly FlurlClient _client;

    public VersionFileApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<System.Version> GetVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        return await _client.Request(VersionFilePathSegment, hash)
            .SetQueryParam("algorithm", hashAlgorithm.ToString().ToLower()).GetJsonAsync<System.Version>();
    }

    /// <inheritdoc />
    public async Task DeleteVersionByHashAsync(string hash, HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        await _client.Request(VersionFilePathSegment, hash)
            .SetQueryParam("algorithm", hashAlgorithm.ToString().ToLower()).DeleteAsync();
    }
}