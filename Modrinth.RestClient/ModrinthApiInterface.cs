using Modrinth.RestClient.Models;
using RestEase;
using Version = Modrinth.RestClient.Models.Version;

namespace Modrinth.RestClient;

/// <summary>
/// Interface for Modrinth API using RestEase
/// </summary>
public interface IModrinthApi
{
    /// <summary>
    /// Get or set User-Agent header to be used when sending requests
    /// </summary>
    [Header("User-Agent")] 
    string UserAgentHeader { get; set; } 

    /// <summary>
    /// Gets project by slug or ID
    /// </summary>
    /// <param name="slugOrId">The ID or slug of the project</param>
    /// <returns></returns>
    [Get("project/{slugORid}")]
    Task<Project> GetProjectAsync([Path("slugORid")] string slugOrId);
    
    /// <summary>
    /// Gets multiple projects by their IDs
    /// </summary>
    /// <param name="ids">IEnumerable of string ids</param>
    /// <returns></returns>
    [Get("projects")]
    Task<Project[]> GetMultipleProjectsAsync([Query("ids")] IEnumerable<string> ids);

    /// <summary>
    /// Gets version list of a project by its ID
    /// </summary>
    /// <param name="slugOrId">The ID or slug of the project</param>
    /// <returns></returns>
    [Get("project/{slugORid}/version")]
    Task<Version[]> GetProjectVersionListAsync([Path("slugORid")] string slugOrId);
    
    /// <summary>
    /// Gets multiple versions by their ids
    /// </summary>
    /// <param name="ids">The IDs of the versions</param>
    /// <returns></returns>
    [Get("versions")]
    Task<Version[]> GetMultipleVersionsAsync([Query("ids")] IEnumerable<string> ids);
    
    /// <summary>
    /// Check project slug/ID validity
    /// </summary>
    /// <returns></returns>
    [Get("project/{slugORid}/check")]
    Task<string> CheckProjectIdSlugValidityAsync([Path("slugORid")] string slugOrId);

    /// <summary>
    /// Get specific version by ID
    /// </summary>
    /// <param name="versionId"></param>
    /// <returns></returns>
    [Get("version/{id}")]
    Task<Version> GetVersionByIdAsync([Path("id")] string versionId);
    
    /// <summary>
    /// Gets user by his ID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [Get("user/{id}")]
    Task<User> GetUserByIdAsync([Path("id")] string userId);
    
    /// <summary>
    /// Gets user by username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [Get("user/{username}")]
    Task<User> GetUserByUsernameAsync([Path("username")] string username);

    /// <summary>
    /// Gets multiple users by their IDs
    /// </summary>
    /// <param name="ids">The IDs of the projects</param>
    /// <returns></returns>
    [Get("users")]
    Task<User[]> GetMultipleUsersByIdAsync([Query("ids")] IEnumerable<string> ids);

    /// <summary>
    /// Gets all projects of a user by their ID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [Get("user/{id}/projects")]
    Task<Project[]> GetUsersProjectsByUserIdAsync([Path("id")] string userId);
    
    /// <summary>
    /// Gets all projects of a user by their username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [Get("user/{username}/projects")]
    Task<Project[]> GetUserProjectsByUsernameAsync([Path("username")] string username);

    /// <summary>
    /// Gets project's team members by project's slug or ID
    /// </summary>
    /// <param name="slugOrId"></param>
    /// <returns></returns>
    [Get("project/{slugORid}/members")]
    Task<TeamMember[]> GetProjectTeamMembersByProjectAsync([Path("slugORid")] string slugOrId);

    /// <summary>
    /// Gets team members by team ID
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns></returns>
    [Get("team/{id}/members")]
    Task<TeamMember[]> GetTeamMembersByTeamIdAsync([Path("id")] string teamId);

    /// <summary>
    /// Gets the members of multiple teams
    /// </summary>
    /// <param name="ids">The IDs of the teams</param>
    /// <returns></returns>
    [Get("teams")]
    Task<TeamMember[][]> GetMembersOfMultipleTeamsAsync([Query("ids")] IEnumerable<string> ids);

    /// <summary>
    /// Search Modrinth for project by it's name
    /// </summary>
    /// <param name="query">The query to search for</param>
    /// <returns></returns>
    [Get("search")]
    Task<SearchResponse> SearchProjectsAsync([Query("query")] string query);
}