using Modrinth.Extensions;
using Modrinth.Models;
using File = System.IO.File;

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
    {
        // return await _client.Request(UserPathSegment, usernameOrId).GetJsonAsync<Models.User>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment+ '/' + usernameOrId, UriKind.Relative);

        return await _client.GetJsonAsync<Models.User>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetProjectsAsync(string usernameOrId)
    {
        // return await _client.Request(UserPathSegment, usernameOrId, "projects").GetJsonAsync<Models.Project[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment+ '/' + usernameOrId + '/' + "projects", UriKind.Relative);

        return await _client.GetJsonAsync<Models.Project[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.User[]> GetMultipleAsync(IEnumerable<string> ids)
    {
        // return await _client.Request("users").SetQueryParam("ids", ids.ToModrinthQueryString())
        //     .GetJsonAsync<Models.User[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("users", UriKind.Relative);

        var parameters = new ParameterBuilder()
        {
            {"ids", ids.ToModrinthQueryString()}
        };
        
        parameters.AddToRequest(reqMsg);
        
        return await _client.GetJsonAsync<Models.User[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.User> GetCurrentAsync()
    {
        // return await _client.Request(UserPathSegment).GetJsonAsync<Models.User>();
        
                   var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment, UriKind.Relative);

        return await _client.GetJsonAsync<Models.User>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Notification[]> GetNotificationsAsync(string usernameOrId)
    {
        // return await _client.Request(UserPathSegment, usernameOrId, "notifications").GetJsonAsync<Notification[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment+ '/' + usernameOrId + '/' + "notifications", UriKind.Relative);

        return await _client.GetJsonAsync<Notification[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetFollowedProjectsAsync(string usernameOrId)
    {
        // return await _client.Request(UserPathSegment, usernameOrId, "follows").GetJsonAsync<Models.Project[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment+ '/' + usernameOrId + '/' + "follows", UriKind.Relative);

        return await _client.GetJsonAsync<Models.Project[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ChangeIconAsync(string usernameOrId, string iconPath)
    {
        // There needs to be a query parameter "ext" with the extension of the file - not documented

        // var extension = Path.GetExtension(iconPath).TrimStart('.');
        //
        // await using var stream = File.OpenRead(iconPath);
        // using var streamContent = new StreamContent(stream);
        //
        // await _client.Request(UserPathSegment, usernameOrId, "icon")
        //     .SetQueryParam("ext", extension).PatchAsync(streamContent);
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Patch;
        reqMsg.RequestUri = new Uri(UserPathSegment+ '/' + usernameOrId + '/' + "icon", UriKind.Relative);
        var extension = Path.GetExtension(iconPath).TrimStart('.');
        
        var parameters = new ParameterBuilder()
        {
            {"ext", extension}
        };
        
        parameters.AddToRequest(reqMsg);
        
        var stream = File.OpenRead(iconPath);
        var streamContent = new StreamContent(stream);

        reqMsg.Content = streamContent;
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }
}