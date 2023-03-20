using Modrinth.Models;
using Modrinth.Models.Facets;
using Index = Modrinth.Models.Enums.Index;

namespace Modrinth.Endpoints.Project;

public class ProjectApi : IProjectApi
{
    private const string ProjectPathSegment = "project";

    private readonly IRequester _client;

    public ProjectApi(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.Project> GetAsync(string slugOrId)
    {
        // return await _client.Request(ProjectPathSegment, slugOrId).GetJsonAsync<Models.Project>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId, UriKind.Relative);
        
        return await _client.GetJsonAsync<Models.Project>(reqMsg);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetRandomAsync(ulong count = 10)
    {
        throw new NotImplementedException();
        // return await _client.Request("projects_random")
        //     .SetQueryParam("count", count)
        //     .GetJsonAsync<Models.Project[]>();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string slugOrId)
    {
        throw new NotImplementedException();
        // await _client.Request(ProjectPathSegment, slugOrId).DeleteAsync();
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetMultipleAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
        // return await _client.Request("projects")
        //     .SetQueryParam("ids", ids.ToModrinthQueryString())
        //     .GetJsonAsync<Models.Project[]>();
    }

    /// <inheritdoc />
    public async Task<SlugIdValidity> CheckIdSlugValidityAsync(string slugOrId)
    {
        throw new NotImplementedException();
        // return await _client.Request(ProjectPathSegment, slugOrId, "check").GetJsonAsync<SlugIdValidity>();
    }

    /// <inheritdoc />
    public async Task<Dependencies> GetDependenciesAsync(string slugOrId)
    {
        throw new NotImplementedException();
        // return await _client.Request(ProjectPathSegment, slugOrId, "dependencies").GetJsonAsync<Dependencies>();
    }

    /// <inheritdoc />
    public async Task FollowAsync(string slugOrId)
    {
        throw new NotImplementedException();
        // await _client.Request(ProjectPathSegment, slugOrId, "follow").PostAsync();
    }

    /// <inheritdoc />
    public async Task UnfollowAsync(string slugOrId)
    {
        throw new NotImplementedException();
        // await _client.Request(ProjectPathSegment, slugOrId, "follow").DeleteAsync();
    }

    /// <inheritdoc />
    public async Task DeleteIconAsync(string slugOrId)
    {
        throw new NotImplementedException();
        // await _client.Request(ProjectPathSegment, slugOrId, "icon").DeleteAsync();
    }

    /// <inheritdoc />
    public async Task ChangeIconAsync(string slugOrId, string iconPath)
    {
        throw new NotImplementedException();
        // var extension = Path.GetExtension(iconPath).TrimStart('.');
        //
        // await using var stream = File.OpenRead(iconPath);
        // using var streamContent = new StreamContent(stream);
        //
        // await _client.Request(ProjectPathSegment, slugOrId, "icon")
        //     .SetQueryParam("ext", extension)
        //     .PatchAsync(streamContent);
    }

    /// <inheritdoc />
    public async Task AddGalleryImageAsync(string slugOrId, string imagePath, bool featured, string? title = null,
        string? description = null, ulong? ordering = null)
    {
        throw new NotImplementedException();
        // var extension = Path.GetExtension(imagePath).TrimStart('.');
        //
        // var request = _client.Request(ProjectPathSegment, slugOrId, "gallery")
        //     .SetQueryParam("featured", featured.ToString().ToLower())
        //     .SetQueryParam("title", title)
        //     .SetQueryParam("description", description)
        //     .SetQueryParam("ordering", ordering)
        //     .SetQueryParam("ext", extension);
        //
        // await using var stream = File.OpenRead(imagePath);
        // using var streamContent = new StreamContent(stream);
        //
        // await request.PostAsync(streamContent);
    }

    /// <inheritdoc />
    public async Task ModifyGalleryImageAsync(string slugOrId, string url, bool? featured = null, string? title = null,
        string? description = null, ulong? ordering = null)
    {
        throw new NotImplementedException();
        // var request = _client.Request(ProjectPathSegment, slugOrId, "gallery")
        //     .SetQueryParam("url", url)
        //     .SetQueryParam("featured", featured?.ToString().ToLower())
        //     .SetQueryParam("title", title)
        //     .SetQueryParam("description", description)
        //     .SetQueryParam("ordering", ordering);
        //
        // await request.PatchAsync();
    }

    /// <inheritdoc />
    public async Task DeleteGalleryImageAsync(string slugOrId, string url)
    {
        throw new NotImplementedException();
        // await _client.Request(ProjectPathSegment, slugOrId, "gallery")
        //     .SetQueryParam("url", url)
        //     .DeleteAsync();
    }

    /// <inheritdoc />
    public async Task<SearchResponse> SearchAsync(string query, Index index = Index.Downloads, ulong offset = 0,
        ulong limit = 10, FacetCollection? facets = null)
    {
        throw new NotImplementedException();
        // var request = _client.Request("search")
        //     .SetQueryParam("query", query.EscapeIfContains())
        //     .SetQueryParam("index", index.ToString().ToLower())
        //     .SetQueryParam("offset", offset)
        //     .SetQueryParam("limit", limit);
        //
        // if (facets is {Count: > 0}) request = request.SetQueryParam("facets", facets.ToString());
        //
        // return await request.GetJsonAsync<SearchResponse>();
    }
}