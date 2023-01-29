using Modrinth.Net.Models;
using Index = Modrinth.Net.Models.Enums.Index;

namespace Modrinth.Net.Endpoints.Project;

public interface IProjectApi
{
    /// <summary>
    /// Search Modrinth for project by it's name
    /// </summary>
    /// <param name="query">The query to search for</param>
    /// <param name="index">The sorting method used for sorting search results</param>
    /// <param name="offset">The offset into the search. Skips this number of results</param>
    /// <param name="limit">The number of results returned by the search</param>
    /// <returns></returns>
    Task<SearchResponse> SearchAsync(
        string query,
        Models.Enums.Index index = Models.Enums.Index.Downloads,
        ulong offset = 0,
        ulong limit = 10);
    
    /// <summary>
    /// Gets project by slug or ID
    /// </summary>
    /// <param name="slugOrId">The ID or slug of the project</param>
    /// <returns></returns>
    Task<Models.Project> GetAsync(string slugOrId);
    
    Task DeleteAsync(string slugOrId);
    
    /// <summary>
    /// Gets multiple projects by their IDs
    /// </summary>
    /// <param name="ids">IEnumerable of string ids</param>
    /// <returns></returns>
    Task<Models.Project[]> GetMultipleAsync(IEnumerable<string> ids);

    /// <summary>
    /// Check project slug/ID validity
    /// </summary>
    /// <returns></returns>
    Task<SlugIdValidity> CheckIdSlugValidityAsync(string slugOrId);

    Task<Dependencies> GetDependenciesAsync(string slugOrId);
    
    Task FollowAsync(string slugOrId);
    
    Task UnfollowAsync(string slugOrId);
}