using Modrinth.RestClient.Models;

namespace Modrinth.RestClient.Endpoints.Team;

public interface ITeamApi
{
    /// <summary>
    /// Gets project's team members by project's slug or ID
    /// </summary>
    /// <param name="slugOrId"></param>
    /// <returns></returns>
    Task<TeamMember[]> GetProjectTeamMembersByProjectAsync(string slugOrId);

    /// <summary>
    /// Gets team members by team ID
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns></returns>
    Task<TeamMember[]> GetTeamMembersByTeamIdAsync(string teamId);

    /// <summary>
    /// Gets the members of multiple teams
    /// </summary>
    /// <param name="ids">The IDs of the teams</param>
    /// <returns></returns>
    Task<TeamMember[][]> GetMembersOfMultipleTeamsAsync(IEnumerable<string> ids);

}