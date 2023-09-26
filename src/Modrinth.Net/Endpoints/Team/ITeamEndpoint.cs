using Modrinth.Exceptions;
using Modrinth.Models;

namespace Modrinth.Endpoints.Team;

/// <summary>
///     Team endpoints
/// </summary>
public interface ITeamEndpoint
{
    /// <summary>
    ///     Gets project's team members by project's slug or ID
    /// </summary>
    /// <param name="slugOrId"></param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<TeamMember[]> GetProjectTeamAsync(string slugOrId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets team members by team ID
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<TeamMember[]> GetAsync(string teamId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the members of multiple teams
    /// </summary>
    /// <param name="ids">The IDs of the teams</param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<TeamMember[][]> GetMultipleAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);
    
    
    /// <summary>
    ///     Adds a user to a team
    /// </summary>
    /// <param name="teamId"> The ID of the team to add the user to </param>
    /// <param name="userId"> The ID of the user to add </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task AddUserAsync(string teamId, string userId, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Removes a user from a team
    /// </summary>
    /// <param name="teamId"> The ID of the team to remove the user from </param>
    /// <param name="userId"> The ID of the user to remove </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task RemoveMemberAsync(string teamId, string userId, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Joins a team
    /// </summary>
    /// <param name="teamId"> The ID of the team to join </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task JoinAsync(string teamId, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Transfers ownership of a team to another user
    /// </summary>
    /// <param name="teamId"> The ID of the team to transfer </param>
    /// <param name="userId"> The ID of the user to transfer ownership to </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task TransferOwnershipAsync(string teamId, string userId, CancellationToken cancellationToken = default);
}