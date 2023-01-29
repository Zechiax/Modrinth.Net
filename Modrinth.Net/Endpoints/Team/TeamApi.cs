using Flurl.Http;
using Modrinth.Net.Models;
using Modrinth.Net.Extensions;

namespace Modrinth.Net.Endpoints.Team;

/// <inheritdoc />
public class TeamApi : ITeamApi
{
    private const string TeamsPathSegment = "team";
    private readonly FlurlClient _client;
    public TeamApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<TeamMember[]> GetProjectTeamAsync(string slugOrId)
    {
        return await _client.Request("project", slugOrId, "members").GetJsonAsync<TeamMember[]>();
    }

    /// <inheritdoc />
    public async Task<TeamMember[]> GetAsync(string teamId)
    {
        return await _client.Request(TeamsPathSegment, teamId, "members").GetJsonAsync<TeamMember[]>();
    }

    /// <inheritdoc />
    public async Task<TeamMember[][]> GetMultipleAsync(IEnumerable<string> ids)
    {
        return await _client.Request("teams").SetQueryParam("ids", ids.ToModrinthQueryString()).GetJsonAsync<TeamMember[][]>();
    }
}