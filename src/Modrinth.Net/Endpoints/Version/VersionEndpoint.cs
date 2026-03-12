using System.Text;
using System.Text.Json;
using Modrinth.Endpoints.Project;
using Modrinth.Extensions;
using Modrinth.Helpers;
using Modrinth.Http;
using Modrinth.Models;
using Modrinth.Models.Enums.Project;
using Modrinth.Models.Enums.Version;
using File = System.IO.File;

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
    public async Task<Models.Version> CreateAsync(string projectId, IEnumerable<UploadableFile> files, string primaryFile, string name, string versionNumber,
                                                  string? changelog, IEnumerable<Dependency> dependencies, IEnumerable<string> gameVersions,
                                                  ProjectVersionType versionType, IEnumerable<string> loaders, bool featured,
                                                  VersionStatus status, VersionStatus? requestedStatus,
                                                  CancellationToken cancellationToken = default) {
	    // todo this is really messy, imo should do builder or maybe take struct/class directly and have serializer function in it
	    var reqMsg = new HttpRequestMessage();
	    reqMsg.Method = HttpMethod.Post;
	    reqMsg.RequestUri = new Uri(VersionsPath, UriKind.Relative);

	    // transform for web
	    // might be better to make some kind of base class like ModrinthApiSerializer and ModrinthApiDeserializer
	    // and then extend those
	    var deps = dependencies.Select(d => new
	    {
		    version_id = d.VersionId,
		    project_id = d.ProjectId,
		    file_name = d.FileName,
		    dependency_type = d.DependencyType
	    }).ToList();

	    // data
        var uploadableFiles = files as UploadableFile[] ?? files.ToArray();
        string j = JsonSerializer.Serialize(new
        {
            name = name,
            version_number = versionNumber,
            changelog = changelog,
            dependencies = deps,
            game_versions = gameVersions,
            version_type = versionType.ToModrinthString(),
            loaders = loaders,
            featured = featured,
            status = status.ToModrinthString(),
            requested_status = requestedStatus?.ToModrinthString(),
            project_id = projectId,
            file_parts = uploadableFiles.Select(f => f.FileName),
            primary_file = primaryFile
        });

	    MultipartFormDataContent c = new();
	    c.Add(new StringContent(j, Encoding.UTF8, "application/json"), "data");
        foreach (UploadableFile file in uploadableFiles)
        {
            c.Add(new StreamContent(file.Stream), file.FileName, file.FileName);
        }

	    reqMsg.Content = c;

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
        return await BatchingHelper.GetFromBatchesAsync(ids, FetchVersionBatchAsync, Config.BatchSize, cancellationToken);
    
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