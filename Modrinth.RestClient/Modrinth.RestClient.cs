using System.Text;
using Microsoft.Extensions.Http;
using Modrinth.RestClient.Models;
using Polly;
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


/// <summary>
/// Base for creating new clients using RestEase from <see cref="IModrinthApi"/> interface
/// </summary>
public static class ModrinthApi
{
    /// <summary>
    /// API Url of the production server
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string BaseUrl = "https://api.modrinth.com/v2";

    /// <summary>
    /// API Url of the staging server
    /// </summary>
    public const string StagingBaseUrl = "https://staging-api.modrinth.com/v2";

    /// <summary>
    /// Initializes new RestClient from <see cref="IModrinthApi"/>
    /// </summary>
    /// <param name="userAgent">User-Agent header you want to use while communicating with Modrinth API, it's recommended to set 'a uniquely-identifying' one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)</param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl"/></param>
    /// <returns>New RestEase RestClient from <see cref="IModrinthApi"/> interface</returns>
    public static IModrinthApi NewClient(string url = BaseUrl, string userAgent = "")
    {
        var api = new RestEase.RestClient(url)
        {
            // Custom query builder to comply with Modrinth's API specification
            QueryStringBuilder = new ModrinthQueryBuilder()
        }.For<IModrinthApi>();

        api.UserAgentHeader = userAgent;

        return api;
    }
    
    /// <summary>
    /// Initializes new RestClient from <see cref="IModrinthApi"/> with specified policy
    /// </summary>
    /// <param name="userAgent">User-Agent header you want to use while communicating with Modrinth API, it's recommended to set 'a uniquely-identifying' one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)</param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl"/></param>
    /// <param name="policy">Use custom Polly resiliency policy <a href="https://github.com/App-vNext/Polly#resilience-policies=">see Polly</a></param>
    /// <returns>New RestEase RestClient from <see cref="IModrinthApi"/> interface</returns>
    public static IModrinthApi NewClient(IAsyncPolicy<HttpResponseMessage> policy, string url = BaseUrl, string userAgent = "")
    {
        var api = new RestEase.RestClient(url, new PolicyHttpMessageHandler(policy))
        {
            // Custom query builder to comply with Modrinth's API specification
            QueryStringBuilder = new ModrinthQueryBuilder()
        }.For<IModrinthApi>();

        api.UserAgentHeader = userAgent;

        return api;
    }
}

/// <summary>
/// Custom query builder for Modrinth, as Modrinth requires arrays to be formatted as ids=["someId1", "someId2"]
/// </summary>
public class ModrinthQueryBuilder : QueryStringBuilder
{
    /// <summary>
    /// Override of the Build method for QueryStringBuilder to provide formatting for Modrinth
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public override string Build(QueryStringBuilderInfo info)
    {
        if (!info.QueryParams.Any())
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        SortedDictionary<string, List<string>> query = new SortedDictionary<string, List<string>>();
        
        // Each key will have list of values as its value
        foreach (var kvp in info.QueryParams)
        {
            if (query.ContainsKey(kvp.Key) && kvp.Value is not null)
            {
                var list = query[kvp.Key];
                list.Add(kvp.Value);
            }
            // First time we see the key
            else if (kvp.Value is not null)
            {
                query.Add(kvp.Key, new List<string> {kvp.Value});
            }
        }

        var counter = query.Count;
        // Let's build the query string
        foreach (var (key, list) in query)
        {
            counter--;
            sb.Append($"{key}=[");

            for (int i = 0; i < list.Count; i++)
            {
                // Check if we have the last value
                sb.Append($"\"{list[i]}\"{(i == (list.Count - 1) ? null : ',')}");
            }

            // Check for the last value
            sb.Append($"]{(counter == 0 ? null : "&")}");
        }
        
        return sb.ToString();
    }
}