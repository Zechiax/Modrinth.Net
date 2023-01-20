namespace Modrinth.RestClient.Endpoints.User;

public interface IUserApi
{
    /// <summary>
    /// Gets user by his username or ID
    /// </summary>
    /// <param name="usernameOrId"></param>
    /// <returns></returns>
    Task<Models.User> GetUserAsync(string usernameOrId);
    
    /// <summary>
    /// Gets all projects of a user by their username or ID
    /// </summary>
    /// <param name="usernameOrId"></param>
    /// <returns></returns>
    Task<Models.Project[]> GetUsersProjectsByUserIdAsync(string usernameOrId);

    /// <summary>
    /// Gets multiple users by their IDs
    /// </summary>
    /// <param name="ids">The IDs of the projects</param>
    /// <returns></returns>
    Task<Models.User[]> GetMultipleUsersByIdAsync(IEnumerable<string> ids);
    
    /// <summary>
    /// Gets current user by authentication token
    /// </summary>
    /// <returns></returns>
    Task<Models.User> GetCurrentUserAsync();
}