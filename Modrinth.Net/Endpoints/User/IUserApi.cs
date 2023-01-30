using Modrinth.Models;

namespace Modrinth.Endpoints.User;

public interface IUserApi
{
    /// <summary>
    ///     Retrieves a user by their username or ID.
    /// </summary>
    /// <param name="usernameOrId">The username or ID of the user to retrieve.</param>
    /// <returns>A task that, when awaited, returns a User object representing the requested user.</returns>
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
    /// </summary>
    /// <returns></returns>
    Task<Models.User> GetCurrentAsync();

    Task<Notification[]> GetNotificationsAsync(string usernameOrId);

    Task<Models.Project[]> GetFollowedProjectsAsync(string usernameOrId);
}