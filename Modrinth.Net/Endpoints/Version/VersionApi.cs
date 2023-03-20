using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.Version;

public class VersionApi : IVersionApi
{
    private const string VersionsPath = "version";
    private readonly IRequester _client;

    public VersionApi(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetAsync(string versionId)
    {throw new NotImplementedException();
        // return await _client.Request(VersionsPath, versionId).GetJsonAsync<Models.Version>();
    }

    /// <inheritdoc />
    public async Task<Models.Version[]> GetProjectVersionListAsync(string slugOrId, string[]? loaders = null,
        string[]? gameVersions = null, bool? featured = null)
    {throw new NotImplementedException();
        // var request = _client.Request("project", slugOrId, VersionsPath);
        //
        // if (loaders != null)
        //     request = request.SetQueryParam("loaders", loaders.ToModrinthQueryString());
        //
        // if (gameVersions != null)
        //     request = request.SetQueryParam("game_versions", gameVersions.ToModrinthQueryString());
        //
        // if (featured != null)
        //     request = request.SetQueryParam("featured", featured.Value.ToString().ToLower());
        //
        // Console.WriteLine(request.Url);
        //
        // return await request.GetJsonAsync<Models.Version[]>();
    }

    /// <inheritdoc />
    public async Task<Models.Version[]> GetMultipleAsync(IEnumerable<string> ids)
    {throw new NotImplementedException();
        // return await _client.Request("versions")
        //     .SetQueryParam("ids", ids.ToModrinthQueryString())
        //     .GetJsonAsync<Models.Version[]>();
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetByVersionNumberAsync(string slugOrId, string versionNumber)
    {throw new NotImplementedException();
        // return await _client.Request("project", slugOrId, VersionsPath, versionNumber)
        //     .GetJsonAsync<Models.Version>();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string versionId)
    {throw new NotImplementedException();
        // await _client.Request(VersionsPath, versionId).DeleteAsync();
    }

    /// <inheritdoc />
    public async Task ScheduleAsync(string versionId, DateTime date, VersionRequestedStatus requestedStatus)
    {throw new NotImplementedException();
        // await _client.Request(VersionsPath, versionId, "schedule")
        //     .PostJsonAsync(new
        //     {
        //         time = date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
        //         requested_status = requestedStatus.ToString().ToLower()
        //     });
    }
}