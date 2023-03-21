using Modrinth.Extensions;
using Modrinth.Http;
using Modrinth.Models;

namespace Modrinth.Endpoints.Team;

/// <inheritdoc />
public class TeamEndpoint : ITeamEndpoint
{
    private const string TeamsPathSegment = "team";
    private readonly IRequester _client;

    public TeamEndpoint(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<TeamMember[]> GetProjectTeamAsync(string slugOrId)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("project" + '/' + slugOrId + '/' + "members", UriKind.Relative);

        return await _client.GetJsonAsync<TeamMember[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TeamMember[]> GetAsync(string teamId)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "members", UriKind.Relative);

        return await _client.GetJsonAsync<TeamMember[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TeamMember[][]> GetMultipleAsync(IEnumerable<string> ids)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("teams", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            {"ids", ids.ToModrinthQueryString()}
        };

        parameters.AddToRequest(reqMsg);

        return await _client.GetJsonAsync<TeamMember[][]>(reqMsg).ConfigureAwait(false);
    }
}