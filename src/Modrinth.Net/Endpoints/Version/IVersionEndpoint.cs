using Modrinth.Exceptions;
using Modrinth.Models;
using Modrinth.Models.Enums.Project;
using Modrinth.Models.Enums.Version;

namespace Modrinth.Endpoints.Version;

/// <summary>
///     Version endpoints
/// </summary>
public interface IVersionEndpoint
{
    /// <summary>
    ///     Get specific version by ID
    /// </summary>
    /// <param name="versionId"></param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Version> GetAsync(string versionId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets version list of a project by its ID
    ///     Optionally filters for loaders, game versions and featured versions
    /// </summary>
    /// <param name="slugOrId">The ID or slug of the project</param>
    /// <param name="loaders">The types of loaders to filter for</param>
    /// <param name="gameVersions">The game versions to filter for</param>
    /// <param name="featured">Allows to filter for featured or non-featured versions only</param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Version[]> GetProjectVersionListAsync(string slugOrId, string[]? loaders = null,
        string[]? gameVersions = null, bool? featured = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets multiple versions by their ids, if the number of ids is greater than 100, it will be split into multiple
    ///     requests
    /// </summary>
    /// <param name="ids">The IDs of the versions</param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Version[]> GetMultipleAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets a version by its version number
    ///     If there are multiple versions with the same version number, the oldest one is returned
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project </param>
    /// <param name="versionNumber"> The version number of the version </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Version> GetByVersionNumberAsync(string slugOrId, string versionNumber,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a version
    /// </summary>
    /// <param name="projectId">Project ID to create the version on (does not support slugs)</param>
    /// <param name="files">List of files to add to this version (must include atleast 1 file)</param>
    /// <param name="primaryFile">Primary (featured) filename</param>
    /// <param name="name">Version name</param>
    /// <param name="versionNumber">Version number</param>
    /// <param name="changelog">Changelog</param>
    /// <param name="dependencies">Modrinth project dependencies</param>
    /// <param name="gameVersions">Supported Minecraft versions</param>
    /// <param name="versionType">Version type</param>
    /// <param name="loaders">Supported Minecraft modloaders</param>
    /// <param name="featured">Whether to feature it on the mod page</param>
    /// <param name="status">Unknown, but the mod seemingly doesn't show up if this is not VersionStatus.Listed.</param>
    /// <param name="requestedStatus">Unknown</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The newly created Version</returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Version> CreateAsync(string projectId, IEnumerable<UploadableFile> files, string primaryFile, string name, string versionNumber,
                                     string? changelog, IEnumerable<Dependency> dependencies, IEnumerable<string> gameVersions,
                                     ProjectVersionType versionType, IEnumerable<string> loaders, bool featured,
                                     VersionStatus status, VersionStatus? requestedStatus,
                                     CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes a version by its ID
    /// </summary>
    /// <param name="versionId"> The ID of the version </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task DeleteAsync(string versionId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Schedules a version with a requested status for a specific date
    /// </summary>
    /// <param name="versionId"> The ID of the version </param>
    /// <param name="date"> The date the version should be scheduled for </param>
    /// <param name="requestedStatus"> The requested status of the version </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task ScheduleAsync(string versionId, DateTime date, VersionRequestedStatus requestedStatus,
        CancellationToken cancellationToken = default);
}