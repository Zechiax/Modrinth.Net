using Modrinth.Exceptions;
using Modrinth.Models;

namespace Modrinth.Endpoints.Notifications;

/// <summary>
///     Notifications endpoints
/// </summary>
public interface INotificationsEndpoint
{
    /// <summary>
    ///     Gets all notifications of a user by their username or ID
    ///     Requires authentication
    /// </summary>
    /// <param name="usernameOrId"> The username or ID of the user to retrieve notifications from </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns> An array of notifications </returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Notification[]> GetNotificationsAsync(string usernameOrId, CancellationToken cancellationToken = default);
}