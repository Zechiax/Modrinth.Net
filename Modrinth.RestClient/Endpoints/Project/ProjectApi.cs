using Flurl.Http;
using Modrinth.RestClient.Extensions;
using Modrinth.RestClient.Models;
using Index = Modrinth.RestClient.Models.Enums.Index;

namespace Modrinth.RestClient.Endpoints.Project;

public class ProjectApi : IProjectApi
{
    private const string ProjectPathSegment = "project"; 
    
    private readonly FlurlClient _client;

    public ProjectApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.Project> GetProjectAsync(string slugOrId)
    {
        return await _client.Request(ProjectPathSegment, slugOrId).GetJsonAsync<Models.Project>();
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetMultipleProjectsAsync(IEnumerable<string> ids)
    {
        var projects = _client.Request(ProjectPathSegment, "search")
            .SetQueryParam("ids", ids.ToModrinthQueryString())
            .GetJsonAsync<Models.Project[]>();
        
        return await projects;
    }

    /// <inheritdoc />
    public async Task<SlugIdValidity> CheckProjectIdSlugValidityAsync(string slugOrId)
    {
        return await _client.Request(ProjectPathSegment, slugOrId, "check").GetJsonAsync<SlugIdValidity>();
    }

    /// <inheritdoc />
    public async Task<SearchResponse> SearchProjectsAsync(string query, Index index = Index.Downloads, ulong offset = 0, ulong limit = 10)
    {
        return await _client.Request("search")
            .SetQueryParam("query", query)
            .SetQueryParam("index", index.ToString().ToLower())
            .SetQueryParam("offset", offset)
            .SetQueryParam("limit", limit)
            .GetJsonAsync<SearchResponse>();
    }
}