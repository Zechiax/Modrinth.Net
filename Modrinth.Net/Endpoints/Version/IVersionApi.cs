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
    /// </summary>
    /// <param name="slugOrId">The ID or slug of the project</param>
    /// <returns></returns>
    Task<Models.Version[]> GetProjectVersionListAsync(string slugOrId);

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

    Task DeleteAsync(string versionId);
}