using Flurl.Http;
using Modrinth.Extensions;
using Modrinth.Models;

namespace Modrinth.Endpoints.Project;

public class ProjectApi : IProjectApi
{
    private const string ProjectPathSegment = "project"; 
    
    private readonly FlurlClient _client;

    public ProjectApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.Project> GetAsync(string slugOrId)
    {
        return await _client.Request(ProjectPathSegment, slugOrId).GetJsonAsync<Models.Project>();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string slugOrId)
    {
       await _client.Request(ProjectPathSegment, slugOrId).DeleteAsync();
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetMultipleAsync(IEnumerable<string> ids)
    {
        var projects = _client.Request("projects")
            .SetQueryParam("ids", ids.ToModrinthQueryString())
            .GetJsonAsync<Models.Project[]>();
        
        return await projects;
    }

    /// <inheritdoc />
    public async Task<SlugIdValidity> CheckIdSlugValidityAsync(string slugOrId)
    {
        return await _client.Request(ProjectPathSegment, slugOrId, "check").GetJsonAsync<SlugIdValidity>();
    }

    /// <inheritdoc />
    public async Task<Dependencies> GetDependenciesAsync(string slugOrId)
    {
        return await _client.Request(ProjectPathSegment, slugOrId, "dependencies").GetJsonAsync<Dependencies>();
    }

    /// <inheritdoc />
    public async Task FollowAsync(string slugOrId)
    {
        await _client.Request(ProjectPathSegment, slugOrId, "follow").PostAsync();
    }

    /// <inheritdoc />
    public async Task UnfollowAsync(string slugOrId)
    {
        await _client.Request(ProjectPathSegment, slugOrId, "follow").DeleteAsync();
    }

    /// <inheritdoc />
    public async Task<SearchResponse> SearchAsync(string query, Models.Enums.Index index = Models.Enums.Index.Downloads, ulong offset = 0, ulong limit = 10)
    {
        return await _client.Request("search")
            .SetQueryParam("query", query)
            .SetQueryParam("index", index.ToString().ToLower())
            .SetQueryParam("offset", offset)
            .SetQueryParam("limit", limit)
            .GetJsonAsync<SearchResponse>();
    }
}