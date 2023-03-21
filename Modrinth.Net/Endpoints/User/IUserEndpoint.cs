using Modrinth.Models;

namespace Modrinth.Endpoints.User;

public interface IUserEndpoint
{
    /// <summary>
    ///     Retrieves a user by their username or ID.
    /// </summary>
    /// <param name="usernameOrId">The username or ID of the user to retrieve.</param>
    /// <returns>A User object representing the requested user.</returns>
    Task<Models.User> GetAsync(string usernameOrId);

    /// <summary>
    ///     Gets all projects of a user by their username or ID
    /// </summary>
    /// <param name="usernameOrId"></param>
    /// <returns></returns>
    Task<Models.Project[]> GetProjectsAsync(string usernameOrId);

    /// <summary>
    ///     Gets multiple users by their IDs
    /// </summary>
    /// <param name="ids">The IDs of the projects</param>
    /// <returns></returns>
    Task<Models.User[]> GetMultipleAsync(IEnumerable<string> ids);

    /// <summary>
    ///     Gets current user by authentication token
    ///     Requires authentication
    /// </summary>
    /// <returns></returns>
    Task<Models.User> GetCurrentAsync();

    /// <summary>
    ///     Gets all notifications of a user by their username or ID
    ///     Requires authentication
    /// </summary>
    /// <param name="usernameOrId"> The username or ID of the user to retrieve notifications from </param>
    /// <returns> An array of notifications </returns>
    Task<Notification[]> GetNotificationsAsync(string usernameOrId);

    /// <summary>
    ///     Gets all followed projects of a user by their username or ID
    ///     Requires authentication
    /// </summary>
    /// <param name="usernameOrId"> The username or ID of the user to retrieve followed projects from </param>
    /// <returns> An array of followed projects </returns>
    Task<Models.Project[]> GetFollowedProjectsAsync(string usernameOrId);

    /// <summary>
    ///     Changes the icon of a user by their username or ID
    ///     The new avatar may be up to 2MiB in size.
    /// </summary>
    /// <param name="usernameOrId"> The username or ID of the user to change the icon of </param>
    /// <param name="iconPath"> The local path to the icon</param>
    /// <returns></returns>
    Task ChangeIconAsync(string usernameOrId, string iconPath);
}