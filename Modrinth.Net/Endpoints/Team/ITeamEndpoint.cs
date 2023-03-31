using Modrinth.Exceptions;
using Modrinth.Models;

namespace Modrinth.Endpoints.Team;

public interface ITeamEndpoint
{
    /// <summary>
    ///     Gets project's team members by project's slug or ID
    /// </summary>
    /// <param name="slugOrId"></param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<TeamMember[]> GetProjectTeamAsync(string slugOrId);

    /// <summary>
    ///     Gets team members by team ID
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<TeamMember[]> GetAsync(string teamId);

    /// <summary>
    ///     Gets the members of multiple teams
    /// </summary>
    /// <param name="ids">The IDs of the teams</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<TeamMember[][]> GetMultipleAsync(IEnumerable<string> ids);
}