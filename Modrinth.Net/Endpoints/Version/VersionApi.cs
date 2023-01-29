using Flurl.Http;
using Modrinth.Extensions;

namespace Modrinth.Endpoints.Version;

public class VersionApi : IVersionApi
{
    private const string VersionsPath = "version";
    private FlurlClient _client;
    public VersionApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetAsync(string versionId)
    {
        return await _client.Request(VersionsPath, versionId).GetJsonAsync<Models.Version>();
    }

    /// <inheritdoc />
    public async Task<Models.Version[]> GetProjectVersionListAsync(string slugOrId)
    {
        return await _client.Request("project", slugOrId, VersionsPath).GetJsonAsync<Models.Version[]>();
    }

    /// <inheritdoc />
    public async Task<Models.Version[]> GetMultipleAsync(IEnumerable<string> ids)
    {
        return await _client.Request("versions")
            .SetQueryParam("ids", ids.ToModrinthQueryString())
            .GetJsonAsync<Models.Version[]>();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string versionId)
    {
        await _client.Request(VersionsPath, versionId).DeleteAsync();
    }
}