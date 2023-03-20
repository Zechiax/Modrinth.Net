using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.Version;

public interface IVersionApi
{
    /// <summary>
    ///     Get specific version by ID
    /// </summary>
    /// <param name="versionId"></param>
    /// <returns></returns>
    Task<Models.Version> GetAsync(string versionId);

    /// <summary>
    ///     Gets version list of a project by its ID
    ///     Optionally filters for loaders, game versions and featured versions
    /// </summary>
    /// <param name="slugOrId">The ID or slug of the project</param>
    /// <param name="loaders">The types of loaders to filter for</param>
    /// <param name="gameVersions">The game versions to filter for</param>
    /// <param name="featured">Allows to filter for featured or non-featured versions only</param>
    /// <returns></returns>
    Task<Models.Version[]> GetProjectVersionListAsync(string slugOrId, string[]? loaders = null,
        string[]? gameVersions = null, bool? featured = null);

    /// <summary>
    ///     Gets multiple versions by their ids
    /// </summary>
    /// <param name="ids">The IDs of the versions</param>
    /// <returns></returns>
    Task<Models.Version[]> GetMultipleAsync(IEnumerable<string> ids);

    /// <summary>
    ///     Gets a version by its version number
    ///     If there are multiple versions with the same version number, the oldest one is returned
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project </param>
    /// <param name="versionNumber"> The version number of the version </param>
    /// <returns></returns>
    Task<Models.Version> GetByVersionNumberAsync(string slugOrId, string versionNumber);

    /// <summary>
    ///     Deletes a version by its ID
    /// </summary>
    /// <param name="versionId"> The ID of the version </param>
    /// <returns></returns>
    Task DeleteAsync(string versionId);

    /// <summary>
    ///     Schedules a version with a requested status for a specific date
    /// </summary>
    /// <param name="versionId"> The ID of the version </param>
    /// <param name="date"> The date the version should be scheduled for </param>
    /// <param name="requestedStatus"> The requested status of the version </param>
    /// <returns></returns>
    Task ScheduleAsync(string versionId, DateTime date, VersionRequestedStatus requestedStatus);
}