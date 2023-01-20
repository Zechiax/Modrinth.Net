using Flurl.Http;
using Modrinth.RestClient.Extensions;
using Modrinth.RestClient.Models;

namespace Modrinth.RestClient.Endpoints.Team;

/// <inheritdoc />
public class TeamApi : ITeamApi
{
    private const string TeamsPathSegment = "teams";
    private readonly FlurlClient _client;
    public TeamApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<TeamMember[]> GetProjectTeamMembersByProjectAsync(string slugOrId)
    {
        return await _client.Request(TeamsPathSegment, slugOrId).GetJsonAsync<TeamMember[]>();
    }

    /// <inheritdoc />
    public async Task<TeamMember[]> GetTeamMembersByTeamIdAsync(string teamId)
    {
        return await _client.Request(TeamsPathSegment, teamId, "members").GetJsonAsync<TeamMember[]>();
    }

    /// <inheritdoc />
    public async Task<TeamMember[][]> GetMembersOfMultipleTeamsAsync(IEnumerable<string> ids)
    {
        return await _client.Request(TeamsPathSegment).SetQueryParam("ids", ids.ToModrinthQueryString()).GetJsonAsync<TeamMember[][]>();
    }
}