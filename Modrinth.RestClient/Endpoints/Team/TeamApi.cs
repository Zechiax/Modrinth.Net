using Modrinth.RestClient.Models;

namespace Modrinth.RestClient.Endpoints.Team;

public class TeamApi : ITeamApi
{
    public async Task<TeamMember[]> GetProjectTeamMembersByProjectAsync(string slugOrId)
    {
        throw new NotImplementedException();
    }

    public async Task<TeamMember[]> GetTeamMembersByTeamIdAsync(string teamId)
    {
        throw new NotImplementedException();
    }

    public async Task<TeamMember[][]> GetMembersOfMultipleTeamsAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }
}