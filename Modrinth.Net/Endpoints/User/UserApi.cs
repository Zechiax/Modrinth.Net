using Flurl.Http;
using Modrinth.Extensions;
using Modrinth.Models;

namespace Modrinth.Endpoints.User;

public class UserApi : IUserApi
{
    private const string UserPathSegment = "user";
    private readonly FlurlClient _client;

    public UserApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.User> GetAsync(string usernameOrId)
    {
        return await _client.Request(UserPathSegment, usernameOrId).GetJsonAsync<Models.User>();
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetProjectsAsync(string usernameOrId)
    {
        return await _client.Request(UserPathSegment, usernameOrId, "projects").GetJsonAsync<Models.Project[]>();
    }

    /// <inheritdoc />
    public async Task<Models.User[]> GetMultipleAsync(IEnumerable<string> ids)
    {
        return await _client.Request("users").SetQueryParam("ids", ids.ToModrinthQueryString())
            .GetJsonAsync<Models.User[]>();
    }

    /// <inheritdoc />
    public async Task<Models.User> GetCurrentAsync()
    {
        return await _client.Request(UserPathSegment).GetJsonAsync<Models.User>();
    }

    /// <inheritdoc />
    public async Task<Notification[]> GetNotificationsAsync(string usernameOrId)
    {
        return await _client.Request(UserPathSegment, usernameOrId, "notifications").GetJsonAsync<Notification[]>();
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetFollowedProjectsAsync(string usernameOrId)
    {
        return await _client.Request(UserPathSegment, usernameOrId, "follows").GetJsonAsync<Models.Project[]>();
    }
}