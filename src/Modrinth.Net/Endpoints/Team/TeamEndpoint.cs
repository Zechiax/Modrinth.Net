using System.Text;
using System.Text.Json;
using Modrinth.Extensions;
using Modrinth.Http;
using Modrinth.Models;

namespace Modrinth.Endpoints.Team;

/// <inheritdoc cref="Modrinth.Endpoints.Team.ITeamEndpoint" />
public class TeamEndpoint : Endpoint, ITeamEndpoint
{
    private const string TeamsPathSegment = "team";

    /// <inheritdoc />
    public TeamEndpoint(IRequester requester) : base(requester)
    {
    }

    /// <inheritdoc />
    public async Task<TeamMember[]> GetProjectTeamAsync(string slugOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("project" + '/' + slugOrId + '/' + "members", UriKind.Relative);

        return await Requester.GetJsonAsync<TeamMember[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TeamMember[]> GetAsync(string teamId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "members", UriKind.Relative);

        return await Requester.GetJsonAsync<TeamMember[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TeamMember[][]> GetMultipleAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("teams", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            {"ids", ids.ToModrinthQueryString()}
        };

        parameters.AddToRequest(reqMsg);

        return await Requester.GetJsonAsync<TeamMember[][]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task AddUserAsync(string teamId, string userId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "members", UriKind.Relative);
        
        var requestBody = new
        {
            user_id = userId
        };
        
        reqMsg.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        
        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task RemoveMemberAsync(string teamId, string userId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "members" + '/' + userId, UriKind.Relative);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task JoinAsync(string teamId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "join", UriKind.Relative);

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task TransferOwnershipAsync(string teamId, string userId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "owner", UriKind.Relative);
        
        var requestBody = new
        {
            user_id = userId
        };
        
        reqMsg.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        
        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}