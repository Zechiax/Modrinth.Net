using Modrinth.Models;

namespace Modrinth.Endpoints.User;

public class UserApi : IUserApi
{
    private const string UserPathSegment = "user";
    private readonly IRequester _client;

    public UserApi(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Models.User> GetAsync(string usernameOrId)
    {throw new NotImplementedException();
        // return await _client.Request(UserPathSegment, usernameOrId).GetJsonAsync<Models.User>();
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetProjectsAsync(string usernameOrId)
    {throw new NotImplementedException();
        // return await _client.Request(UserPathSegment, usernameOrId, "projects").GetJsonAsync<Models.Project[]>();
    }

    /// <inheritdoc />
    public async Task<Models.User[]> GetMultipleAsync(IEnumerable<string> ids)
    {throw new NotImplementedException();
        // return await _client.Request("users").SetQueryParam("ids", ids.ToModrinthQueryString())
        //     .GetJsonAsync<Models.User[]>();
    }

    /// <inheritdoc />
    public async Task<Models.User> GetCurrentAsync()
    {throw new NotImplementedException();
        // return await _client.Request(UserPathSegment).GetJsonAsync<Models.User>();
    }

    /// <inheritdoc />
    public async Task<Notification[]> GetNotificationsAsync(string usernameOrId)
    {throw new NotImplementedException();
        // return await _client.Request(UserPathSegment, usernameOrId, "notifications").GetJsonAsync<Notification[]>();
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetFollowedProjectsAsync(string usernameOrId)
    {throw new NotImplementedException();
        // return await _client.Request(UserPathSegment, usernameOrId, "follows").GetJsonAsync<Models.Project[]>();
    }

    /// <inheritdoc />
    public async Task ChangeIconAsync(string usernameOrId, string iconPath)
    {throw new NotImplementedException();
        // There needs to be a query parameter "ext" with the extension of the file - not documented

        // var extension = Path.GetExtension(iconPath).TrimStart('.');
        //
        // await using var stream = File.OpenRead(iconPath);
        // using var streamContent = new StreamContent(stream);
        //
        // await _client.Request(UserPathSegment, usernameOrId, "icon")
        //     .SetQueryParam("ext", extension).PatchAsync(streamContent);
    }
}