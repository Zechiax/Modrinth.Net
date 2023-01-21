using Version = Modrinth.RestClient.Models.Version;

namespace Modrinth.RestClient.Endpoints.Project;

public interface IVersionApi
{
    /// <summary>
    /// Get specific version by ID
    /// </summary>
    /// <param name="versionId"></param>
    /// <returns></returns>
    Task<Version> GetAsync(string versionId);
    
    /// <summary>
    /// Gets version list of a project by its ID
    /// </summary>
    /// <param name="slugOrId">The ID or slug of the project</param>
    /// <returns></returns>
    Task<Version[]> GetProjectVersionListAsync(string slugOrId);
    
    /// <summary>
    /// Gets multiple versions by their ids
    /// </summary>
    /// <param name="ids">The IDs of the versions</param>
    /// <returns></returns>
    Task<Version[]> GetMultipleAsync(IEnumerable<string> ids);
    
    Task DeleteAsync(string versionId);
    
    
}