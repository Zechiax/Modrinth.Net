using System.Text;
using System.Text.Json;
using Modrinth.Extensions;
using Modrinth.Helpers;
using Modrinth.Http;
using Modrinth.Models;
using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.Team;

/// <inheritdoc cref="Modrinth.Endpoints.Team.ITeamEndpoint" />
public class TeamEndpoint : Endpoint, ITeamEndpoint
{
    private const string TeamsPathSegment = "team";

    /// <inheritdoc />
    public TeamEndpoint(IRequester requester, ModrinthClientConfig config) : base(requester, config)
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
        return await BatchingHelper.GetFromBatchesAsync(ids, FetchTeamBatchAsync, Config.BatchSize, cancellationToken);

        async Task<TeamMember[][]> FetchTeamBatchAsync(string[] batch, CancellationToken ct)
        {
            var reqMsg = new HttpRequestMessage();
            reqMsg.Method = HttpMethod.Get;
            reqMsg.RequestUri = new Uri("teams", UriKind.Relative);

            var parameters = new ParameterBuilder
            {
                {"ids", batch.ToModrinthQueryString()}
            };

            parameters.AddToRequest(reqMsg);

            return await Requester.GetJsonAsync<TeamMember[][]>(reqMsg, ct).ConfigureAwait(false);
        }
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
    public async Task TransferOwnershipAsync(string teamId, string userId,
        CancellationToken cancellationToken = default)
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

    /// <inheritdoc />
    public async Task ModifyMemberAsync(string teamId, string userId, string role, Permissions permissions,
        int payoutsSplit,
        int ordering, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Patch;
        reqMsg.RequestUri = new Uri(TeamsPathSegment + '/' + teamId + '/' + "members" + '/' + userId, UriKind.Relative);

        var requestBody = new
        {
            role,
            permissions,
            payouts_split = payoutsSplit,
            ordering
        };

        reqMsg.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}