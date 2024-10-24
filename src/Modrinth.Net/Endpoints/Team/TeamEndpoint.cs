using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Modrinth.Extensions;
using Modrinth.Http;
using Modrinth.Models;
using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.Team;

[JsonSerializable(typeof(AddUserRequest))]
[JsonSerializable(typeof(ModifyMemberRequest))]
[JsonSerializable(typeof(TransferOwnershipRequest))]
internal partial class TeamEndpointJsonContext : JsonSerializerContext;
    
// ReSharper disable InconsistentNaming
internal record AddUserRequest(string user_id);
internal record ModifyMemberRequest(string role, Permissions permissions, int payouts_split, int ordering);
internal record TransferOwnershipRequest(string user_id);
// ReSharper restore InconsistentNaming

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
    public async Task<TeamMember[][]> GetMultipleAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
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

        var requestBody = new AddUserRequest(userId);

        var json = JsonSerializer.Serialize(requestBody, TeamEndpointJsonContext.Default.AddUserRequest);
        reqMsg.Content = new StringContent(json, Encoding.UTF8, "application/json");

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
    public async Task TransferOwnershipAsync(string teamId, string userId,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Post;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "owner", UriKind.Relative);

        var requestBody = new TransferOwnershipRequest(userId);
        
        var json = JsonSerializer.Serialize(requestBody, TeamEndpointJsonContext.Default.TransferOwnershipRequest);
        reqMsg.Content = new StringContent(json, Encoding.UTF8, "application/json");

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ModifyMemberAsync(string teamId, string userId, string role, Permissions permissions,
        int payoutsSplit,
        int ordering, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Patch;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "members" + '/' + userId, UriKind.Relative);

        var requestBody = new ModifyMemberRequest(role, permissions, payoutsSplit, ordering);

        var json = JsonSerializer.Serialize(requestBody, TeamEndpointJsonContext.Default.ModifyMemberRequest);
        reqMsg.Content = new StringContent(json, Encoding.UTF8, "application/json");

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}