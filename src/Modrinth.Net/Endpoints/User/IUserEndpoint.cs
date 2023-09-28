using Modrinth.Exceptions;
using Modrinth.Models;

namespace Modrinth.Endpoints.User;

/// <summary>
///     User endpoints
/// </summary>
public interface IUserEndpoint
{
    /// <summary>
    ///     Retrieves a user by their username or ID.
    /// </summary>
    /// <param name="usernameOrId">The username or ID of the user to retrieve.</param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns>A User object representing the requested user.</returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.User> GetAsync(string usernameOrId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all projects of a user by their username or ID
    /// </summary>
    /// <param name="usernameOrId"></param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Project[]> GetProjectsAsync(string usernameOrId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets multiple users by their IDs
    /// </summary>
    /// <param name="ids">The IDs of the projects</param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.User[]> GetMultipleAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets current user by authentication token
    ///     Requires authentication
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.User> GetCurrentAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all followed projects of a user by their username or ID
    ///     Requires authentication
    /// </summary>
    /// <param name="usernameOrId"> The username or ID of the user to retrieve followed projects from </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns> An array of followed projects </returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Project[]> GetFollowedProjectsAsync(string usernameOrId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Changes the icon of a user by their username or ID
    ///     The new avatar may be up to 2MiB in size.
    /// </summary>
    /// <param name="usernameOrId"> The username or ID of the user to change the icon of </param>
    /// <param name="iconPath"> The local path to the icon</param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task ChangeIconAsync(string usernameOrId, string iconPath, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the payout history of a user by their username or ID
    /// </summary>
    /// <param name="usernameOrId"> The username or ID of the user to retrieve the payout history from </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<PayoutHistory> GetPayoutHistoryAsync(string usernameOrId, CancellationToken cancellationToken = default);
}