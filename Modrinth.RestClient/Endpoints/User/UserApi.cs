using Flurl.Http;
using Modrinth.RestClient.Extensions;

namespace Modrinth.RestClient.Endpoints.User;

public class UserApi : IUserApi
{
    private const string UserPathSegment = "user";
    private readonly FlurlClient _client;
    public UserApi(FlurlClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.User> GetUserAsync(string usernameOrId)
    {
        return await _client.Request(UserPathSegment, usernameOrId).GetJsonAsync<Models.User>();
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetUsersProjectsByUserIdAsync(string usernameOrId)
    {
        return await _client.Request(UserPathSegment, usernameOrId, "projects").GetJsonAsync<Models.Project[]>();
    }

    /// <inheritdoc />
    public async Task<Models.User[]> GetMultipleUsersByIdAsync(IEnumerable<string> ids)
    {
        return await _client.Request("users").SetQueryParam("ids", ids.ToModrinthQueryString())
            .GetJsonAsync<Models.User[]>();
    }

    /// <inheritdoc />
    public async Task<Models.User> GetCurrentUserAsync()
    {
        return await _client.Request(UserPathSegment).GetJsonAsync<Models.User>();
    }
}