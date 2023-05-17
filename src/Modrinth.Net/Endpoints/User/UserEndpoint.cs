﻿using Modrinth.Extensions;
using Modrinth.Http;
using Modrinth.Models;
using File = System.IO.File;

namespace Modrinth.Endpoints.User;

/// <inheritdoc cref="Modrinth.Endpoints.User.IUserEndpoint" />
public class UserEndpoint : Endpoint, IUserEndpoint
{
    private const string UserPathSegment = "user";

    /// <inheritdoc />
    public UserEndpoint(IRequester requester) : base(requester)
    {
    }

    /// <inheritdoc />
    public async Task<Models.User> GetAsync(string usernameOrId)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId, UriKind.Relative);

        return await Requester.GetJsonAsync<Models.User>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetProjectsAsync(string usernameOrId)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId + '/' + "projects", UriKind.Relative);

        return await Requester.GetJsonAsync<Models.Project[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.User[]> GetMultipleAsync(IEnumerable<string> ids)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("users", UriKind.Relative);

        var parameters = new ParameterBuilder
        {
            {"ids", ids.ToModrinthQueryString()}
        };

        parameters.AddToRequest(reqMsg);

        return await Requester.GetJsonAsync<Models.User[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.User> GetCurrentAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment, UriKind.Relative);

        return await Requester.GetJsonAsync<Models.User>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Notification[]> GetNotificationsAsync(string usernameOrId)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId + '/' + "notifications", UriKind.Relative);

        return await Requester.GetJsonAsync<Notification[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Models.Project[]> GetFollowedProjectsAsync(string usernameOrId)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(UserPathSegment + '/' + usernameOrId + '/' + "follows", UriKind.Relative);

        return await Requester.GetJsonAsync<Models.Project[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ChangeIconAsync(string usernameOrId, string iconPath)
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

        await Requester.SendAsync(reqMsg).ConfigureAwait(false);
    }
}