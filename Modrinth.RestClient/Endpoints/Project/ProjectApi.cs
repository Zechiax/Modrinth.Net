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

    public async Task<bool> ModifyProjectAsync(string slugOrId, Models.Project project)
    {
        var response = await _client.Request(ProjectPathSegment, slugOrId).PatchJsonAsync(project);
        return response.ResponseMessage.IsSuccessStatusCode;
    }
    
    public async Task<bool> DeleteProjectAsync(string slugOrId)
    {
        var response = await _client.Request(ProjectPathSegment, slugOrId).DeleteAsync();
        return response.ResponseMessage.IsSuccessStatusCode;
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

    public async Task<bool> FollowProjectAsync(string slugOrId)
    {
        var response = await _client.Request(ProjectPathSegment, slugOrId, "follow").PostAsync();
        return response.ResponseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> UnfollowProjectAsync(string slugOrId)
    {
        var response = await _client.Request(ProjectPathSegment, slugOrId, "follow").DeleteAsync();
        return response.ResponseMessage.IsSuccessStatusCode;
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