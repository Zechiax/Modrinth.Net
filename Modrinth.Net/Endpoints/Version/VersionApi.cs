using Modrinth.Extensions;
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
    {
        // return await _client.Request(VersionsPath, versionId).GetJsonAsync<Models.Version>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(VersionsPath+ '/' + versionId, UriKind.Relative);
        
        return await _client.GetJsonAsync<Models.Version>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Version[]> GetProjectVersionListAsync(string slugOrId, string[]? loaders = null,
        string[]? gameVersions = null, bool? featured = null)
    {
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
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("project/" + slugOrId + '/' + VersionsPath, UriKind.Relative);
        
        var parameters = new ParameterBuilder();
        
        if (loaders != null)
            parameters.Add("loaders", loaders.ToModrinthQueryString());
        
        if (gameVersions != null)
            parameters.Add("game_versions", gameVersions.ToModrinthQueryString());
        
        if (featured != null)
            parameters.Add("featured", featured.Value.ToString().ToLower());
        
        parameters.AddToRequest(reqMsg);
        
        return await _client.GetJsonAsync<Models.Version[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Version[]> GetMultipleAsync(IEnumerable<string> ids)
    {
        // return await _client.Request("versions")
        //     .SetQueryParam("ids", ids.ToModrinthQueryString())
        //     .GetJsonAsync<Models.Version[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("versions", UriKind.Relative);
        
        var parameters = new ParameterBuilder()
        {
            {"ids", ids.ToModrinthQueryString()}
        };
        
        parameters.AddToRequest(reqMsg);
        
        return await _client.GetJsonAsync<Models.Version[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetByVersionNumberAsync(string slugOrId, string versionNumber)
    {
        // return await _client.Request("project", slugOrId, VersionsPath, versionNumber)
        //     .GetJsonAsync<Models.Version>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("project/" + slugOrId + '/' + VersionsPath + '/' + versionNumber, UriKind.Relative);
        
        return await _client.GetJsonAsync<Models.Version>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string versionId)
    {
        // await _client.Request(VersionsPath, versionId).DeleteAsync();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(VersionsPath + '/' + versionId, UriKind.Relative);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ScheduleAsync(string versionId, DateTime date, VersionRequestedStatus requestedStatus)
    {
        // await _client.Request(VersionsPath, versionId, "schedule")
        //     .PostJsonAsync(new
        //     {
        //         time = date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
        //         requested_status = requestedStatus.ToString().ToLower()
        //     });
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(VersionsPath + '/' + versionId + '/' + "schedule", UriKind.Relative);
        
        var parameters = new ParameterBuilder()
        {
            {"time", date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")},
            {"requested_status", requestedStatus.ToString().ToLower()}
        };
        
        parameters.AddToRequest(reqMsg);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }
}