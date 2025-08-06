using Modrinth.Extensions;
using Modrinth.Helpers;
using Modrinth.Http;
using Modrinth.Models.Enums.Version;

namespace Modrinth.Endpoints.Version;

/// <inheritdoc cref="Modrinth.Endpoints.Version.IVersionEndpoint" />
public class VersionEndpoint : Endpoint, IVersionEndpoint
{
    private const string VersionsPath = "version";

    /// <inheritdoc />
    public VersionEndpoint(IRequester requester, ModrinthClientConfig config) : base(requester, config)
    {
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetAsync(string versionId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(VersionsPath + '/' + versionId, UriKind.Relative);

        return await Requester.GetJsonAsync<Models.Version>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Version[]> GetProjectVersionListAsync(string slugOrId, string[]? loaders = null,
        string[]? gameVersions = null, bool? featured = null, CancellationToken cancellationToken = default)
    {
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

        return await Requester.GetJsonAsync<Models.Version[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Version[]> GetMultipleAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
    {
        return await BatchingHelper.GetFromBatchesAsync(ids, FetchVersionBatchAsync, 100, cancellationToken);
    
        async Task<Models.Version[]> FetchVersionBatchAsync(string[] batch, CancellationToken ct)
        {
            var reqMsg = new HttpRequestMessage();
            reqMsg.Method = HttpMethod.Get;
            reqMsg.RequestUri = new Uri("versions", UriKind.Relative);
    
            var parameters = new ParameterBuilder
            {
                { "ids", batch.ToModrinthQueryString() }
            };
    
            parameters.AddToRequest(reqMsg);
    
            return await Requester.GetJsonAsync<Models.Version[]>(reqMsg, ct).ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async Task<Models.Version> GetByVersionNumberAsync(string slugOrId, string versionNumber,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("project/" + slugOrId + '/' + VersionsPath + '/' + versionNumber, UriKind.Relative);

        return await Requester.GetJsonAsync<Models.Version>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string versionId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(VersionsPath + '/' + versionId, UriKind.Relative);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ScheduleAsync(string versionId, DateTime date, VersionRequestedStatus requestedStatus,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(VersionsPath + '/' + versionId + '/' + "schedule", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            { "time", date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
            { "requested_status", requestedStatus.ToString().ToLower() }
        };

        parameters.AddToRequest(reqMsg);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}