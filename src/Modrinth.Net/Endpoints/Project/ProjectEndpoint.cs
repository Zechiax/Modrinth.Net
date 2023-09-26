using Modrinth.Extensions;
using Modrinth.Http;
using Modrinth.Models;
using File = System.IO.File;
using Index = Modrinth.Models.Enums.Index;

namespace Modrinth.Endpoints.Project;

/// <inheritdoc cref="Modrinth.Endpoints.Project.IProjectEndpoint" />
public class ProjectEndpoint : Endpoint, IProjectEndpoint
{
    private const string ProjectPathSegment = "project";

    /// <inheritdoc />
    public ProjectEndpoint(IRequester requester) : base(requester)
    {
    }

    /// <inheritdoc />
    public async Task<Models.Project> GetAsync(string slugOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId, UriKind.Relative);

        return await Requester.GetJsonAsync<Models.Project>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetRandomAsync(ulong count = 10, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("projects_random", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            { "count", count.ToString() }
        };

        parameters.AddToRequest(reqMsg);

        return await Requester.GetJsonAsync<Models.Project[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string slugOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId, UriKind.Relative);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetMultipleAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("projects", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            { "ids", ids.ToModrinthQueryString() }
        };

        parameters.AddToRequest(reqMsg);

        return await Requester.GetJsonAsync<Models.Project[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string> CheckIdSlugValidityAsync(string slugOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/check", UriKind.Relative);

        var validity = await Requester.GetJsonAsync<SlugIdValidity>(reqMsg, cancellationToken).ConfigureAwait(false);

        return validity.Id;
    }

    /// <inheritdoc />
    public async Task<Dependencies> GetDependenciesAsync(string slugOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/dependencies", UriKind.Relative);

        return await Requester.GetJsonAsync<Dependencies>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task FollowAsync(string slugOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/follow", UriKind.Relative);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task UnfollowAsync(string slugOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/follow", UriKind.Relative);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteIconAsync(string slugOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/icon", UriKind.Relative);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ChangeIconAsync(string slugOrId, string iconPath, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(iconPath).TrimStart('.');

        var reqMsg = new HttpRequestMessage();

        reqMsg.Method = HttpMethod.Patch;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/icon", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            { "ext", extension }
        };

        var stream = File.OpenRead(iconPath);
        using var streamContent = new StreamContent(stream);

        reqMsg.Content = streamContent;

        parameters.AddToRequest(reqMsg);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task AddGalleryImageAsync(string slugOrId, string imagePath, bool featured, string? title = null,
        string? description = null, ulong? ordering = null, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(imagePath).TrimStart('.');

        var reqMsg = new HttpRequestMessage();

        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/gallery", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            { "featured", featured.ToString().ToLower() },
            { "title", title },
            { "description", description },
            { "ordering", ordering },
            { "ext", extension }
        };

        parameters.AddToRequest(reqMsg);

        await using var stream = File.OpenRead(imagePath);
        using var streamContent = new StreamContent(stream);

        reqMsg.Content = streamContent;

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ModifyGalleryImageAsync(string slugOrId, string url, bool? featured = null, string? title = null,
        string? description = null, ulong? ordering = null, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();

        reqMsg.Method = HttpMethod.Patch;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/gallery", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            { "url", url },
            { "featured", featured?.ToString().ToLower() },
            { "title", title },
            { "description", description },
            { "ordering", ordering }
        };

        parameters.AddToRequest(reqMsg);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteGalleryImageAsync(string slugOrId, string url,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();

        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(ProjectPathSegment + "/" + slugOrId + "/gallery", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            { "url", url }
        };

        parameters.AddToRequest(reqMsg);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<SearchResponse> SearchAsync(string query, Index index = Index.Downloads, ulong offset = 0,
        ulong limit = 10, FacetCollection? facets = null, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("search", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            { "query", query.EscapeIfContains() },
            { "index", index.ToString().ToLower() },
            { "offset", offset },
            { "limit", limit }
        };

        if (facets is { Count: > 0 }) parameters.Add("facets", facets.ToString());

        parameters.AddToRequest(reqMsg);

        return await Requester.GetJsonAsync<SearchResponse>(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}