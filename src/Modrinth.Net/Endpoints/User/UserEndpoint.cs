using Modrinth.Extensions;
using Modrinth.Helpers;
using Modrinth.Http;
using Modrinth.Models;
using File = System.IO.File;

namespace Modrinth.Endpoints.User;

/// <inheritdoc cref="Modrinth.Endpoints.User.IUserEndpoint" />
public class UserEndpoint : Endpoint, IUserEndpoint
{
    private const string UserPathSegment = "user";

    /// <inheritdoc />
    public UserEndpoint(IRequester requester, ModrinthClientConfig config) : base(requester, config)
    {
    }

    /// <inheritdoc />
    public async Task<Models.User> GetAsync(string usernameOrId, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId, UriKind.Relative);

        return await Requester.GetJsonAsync<Models.User>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetProjectsAsync(string usernameOrId,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId + '/' + "projects", UriKind.Relative);

        return await Requester.GetJsonAsync<Models.Project[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.User[]> GetMultipleAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
    {
        return await BatchingHelper.GetFromBatchesAsync(ids, FetchUserBatchAsync, Config.BatchSize, cancellationToken);

        async Task<Models.User[]> FetchUserBatchAsync(string[] batch, CancellationToken ct)
        {
            var reqMsg = new HttpRequestMessage();
            reqMsg.Method = HttpMethod.Get;
            reqMsg.RequestUri = new Uri("users", UriKind.Relative);

            var parameters = new ParameterBuilder
            {
                {"ids", batch.ToModrinthQueryString()}
            };

            parameters.AddToRequest(reqMsg);

            return await Requester.GetJsonAsync<Models.User[]>(reqMsg, ct).ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async Task<Models.User> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment, UriKind.Relative);

        return await Requester.GetJsonAsync<Models.User>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetFollowedProjectsAsync(string usernameOrId,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId + '/' + "follows", UriKind.Relative);

        return await Requester.GetJsonAsync<Models.Project[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ChangeIconAsync(string usernameOrId, string iconPath,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Patch;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId + '/' + "icon", UriKind.Relative);
        var extension = Path.GetExtension(iconPath).TrimStart('.');

        var parameters = new ParameterBuilder
        {
            {"ext", extension}
        };

        parameters.AddToRequest(reqMsg);

        var stream = File.OpenRead(iconPath);
        var streamContent = new StreamContent(stream);

        reqMsg.Content = streamContent;

        await Requester.SendAsync(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<PayoutHistory> GetPayoutHistoryAsync(string usernameOrId,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId + '/' + "payouts", UriKind.Relative);

        return await Requester.GetJsonAsync<PayoutHistory>(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}