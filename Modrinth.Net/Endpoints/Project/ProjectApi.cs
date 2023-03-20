using System.Web;
using Modrinth.Extensions;
using Modrinth.Models;
using Modrinth.Models.Facets;
using File = System.IO.File;
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
        
        return await _client.GetJsonAsync<Models.Project>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetRandomAsync(ulong count = 10)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("projects_random", UriKind.Relative);
        
        var parameters = new ParameterBuilder
        {
            {"count", count.ToString()}
        };

        parameters.AddToRequest(reqMsg);

        return await _client.GetJsonAsync<Models.Project[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string slugOrId)
    {
        // await _client.Request(ProjectPathSegment, slugOrId).DeleteAsync();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId, UriKind.Relative);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetMultipleAsync(IEnumerable<string> ids)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("projects", UriKind.Relative);
        
        var parameters = new ParameterBuilder
        {
            {"ids", ids.ToModrinthQueryString()}
        };
        
        parameters.AddToRequest(reqMsg);
        
        return await _client.GetJsonAsync<Models.Project[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<SlugIdValidity> CheckIdSlugValidityAsync(string slugOrId)
    {
        // return await _client.Request(ProjectPathSegment, slugOrId, "check").GetJsonAsync<SlugIdValidity>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/check", UriKind.Relative);
        
        return await _client.GetJsonAsync<SlugIdValidity>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Dependencies> GetDependenciesAsync(string slugOrId)
    {
        // return await _client.Request(ProjectPathSegment, slugOrId, "dependencies").GetJsonAsync<Dependencies>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/dependencies", UriKind.Relative);
        
        return await _client.GetJsonAsync<Dependencies>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task FollowAsync(string slugOrId)
    {
        // await _client.Request(ProjectPathSegment, slugOrId, "follow").PostAsync();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/follow", UriKind.Relative);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task UnfollowAsync(string slugOrId)
    {
        // await _client.Request(ProjectPathSegment, slugOrId, "follow").DeleteAsync();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/follow", UriKind.Relative);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteIconAsync(string slugOrId)
    {
        // await _client.Request(ProjectPathSegment, slugOrId, "icon").DeleteAsync();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/icon", UriKind.Relative);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ChangeIconAsync(string slugOrId, string iconPath)
    {
        // var extension = Path.GetExtension(iconPath).TrimStart('.');
        //
        // await using var stream = File.OpenRead(iconPath);
        // using var streamContent = new StreamContent(stream);
        //
        // await _client.Request(ProjectPathSegment, slugOrId, "icon")
        //     .SetQueryParam("ext", extension)
        //     .PatchAsync(streamContent);
        
        var extension = Path.GetExtension(iconPath).TrimStart('.');
        
        var reqMsg = new HttpRequestMessage();
        
        reqMsg.Method = HttpMethod.Patch;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/icon", UriKind.Relative);
        
        var parameters = new ParameterBuilder
        {
            {"ext", extension}
        };
        
        parameters.AddToRequest(reqMsg);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task AddGalleryImageAsync(string slugOrId, string imagePath, bool featured, string? title = null,
        string? description = null, ulong? ordering = null)
    {
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
        
        var extension = Path.GetExtension(imagePath).TrimStart('.');
        
        var reqMsg = new HttpRequestMessage();
        
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/gallery", UriKind.Relative);
        
        var parameters = new ParameterBuilder
        {
            {"featured", featured.ToString().ToLower()},
            {"title", title},
            {"description", description},
            {"ordering", ordering},
            {"ext", extension}
        };
        
        parameters.AddToRequest(reqMsg);
        
        await using var stream = File.OpenRead(imagePath);
        using var streamContent = new StreamContent(stream);
        
        reqMsg.Content = streamContent;
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ModifyGalleryImageAsync(string slugOrId, string url, bool? featured = null, string? title = null,
        string? description = null, ulong? ordering = null)
    {
        // var request = _client.Request(ProjectPathSegment, slugOrId, "gallery")
        //     .SetQueryParam("url", url)
        //     .SetQueryParam("featured", featured?.ToString().ToLower())
        //     .SetQueryParam("title", title)
        //     .SetQueryParam("description", description)
        //     .SetQueryParam("ordering", ordering);
        //
        // await request.PatchAsync();
        
        var reqMsg = new HttpRequestMessage();
        
        reqMsg.Method = HttpMethod.Patch;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/gallery", UriKind.Relative);
        
        var parameters = new ParameterBuilder
        {
            {"url", url},
            {"featured", featured?.ToString().ToLower()},
            {"title", title},
            {"description", description},
            {"ordering", ordering}
        };
        
        parameters.AddToRequest(reqMsg);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteGalleryImageAsync(string slugOrId, string url)
    {
        // await _client.Request(ProjectPathSegment, slugOrId, "gallery")
        //     .SetQueryParam("url", url)
        //     .DeleteAsync();
        
        var reqMsg = new HttpRequestMessage();
        
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/gallery", UriKind.Relative);
        
        var parameters = new ParameterBuilder
        {
            {"url", url}
        };
        
        parameters.AddToRequest(reqMsg);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<SearchResponse> SearchAsync(string query, Index index = Index.Downloads, ulong offset = 0,
        ulong limit = 10, FacetCollection? facets = null)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("search", UriKind.Relative);
        
        var parameters = new ParameterBuilder
        {
            {"query", query.EscapeIfContains()},
            {"index", index.ToString().ToLower()},
            {"offset", offset},
            {"limit", limit}
        };
        
        if (facets is {Count: > 0}) parameters.Add("facets", facets.ToString());
        
        parameters.AddToRequest(reqMsg);
        
        return await _client.GetJsonAsync<SearchResponse>(reqMsg).ConfigureAwait(false);
    }
}