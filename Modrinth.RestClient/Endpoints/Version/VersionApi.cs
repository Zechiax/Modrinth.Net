using Flurl.Http;
using Modrinth.RestClient.Extensions;

namespace Modrinth.RestClient.Endpoints.Project;

public class VersionApi : IVersionApi
{
    private const string VersionsPath = "version";
    private FlurlClient _client;
    public VersionApi(FlurlClient client)
    {
        _client = client;
    }
    public async Task<Version> GetVersionByIdAsync(string versionId)
    {
        return await _client.Request(VersionsPath, versionId).GetJsonAsync<Version>();
    }

    public async Task<Version[]> GetProjectVersionListAsync(string slugOrId)
    {
        return await _client.Request("project", slugOrId, VersionsPath).GetJsonAsync<Version[]>();
    }

    public async Task<Version[]> GetMultipleVersionsAsync(IEnumerable<string> ids)
    {
        return await _client.Request("versions").SetQueryParam("ids", ids.ToModrinthQueryString())
            .GetJsonAsync<Version[]>();
    }
}