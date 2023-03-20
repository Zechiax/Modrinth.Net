using Flurl.Http;
using Modrinth.Extensions;
using Modrinth.Models;

namespace Modrinth.Endpoints.Team;

/// <inheritdoc />
public class TeamApi : ITeamApi
{
    private const string TeamsPathSegment = "team";
    private readonly IRequester _client;

    public TeamApi(IRequester client)
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
        return await _client.Request("teams").SetQueryParam("ids", ids.ToModrinthQueryString())
            .GetJsonAsync<TeamMember[][]>();
    }
}