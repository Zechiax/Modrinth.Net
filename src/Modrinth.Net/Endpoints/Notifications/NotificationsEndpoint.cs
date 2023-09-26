using Modrinth.Http;
using Modrinth.Models;

namespace Modrinth.Endpoints.Notifications;

/// <inheritdoc cref="Modrinth.Endpoints.Notifications.INotificationsEndpoint" />
public class NotificationsEndpoint : Endpoint, INotificationsEndpoint
{
    /// <inheritdoc />
    public NotificationsEndpoint(IRequester requester) : base(requester)
    {
    }

    /// <inheritdoc />
    public async Task<Notification[]> GetNotificationsAsync(string usernameOrId,
        CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("user" + '/' + usernameOrId + '/' + "notifications", UriKind.Relative);

        return await Requester.GetJsonAsync<Notification[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}