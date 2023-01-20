namespace Modrinth.RestClient.Endpoints.User;

public class UserApi : IUserApi
{
    public async Task<Models.User> GetUserAsync(string usernameOrId)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Project[]> GetUsersProjectsByUserIdAsync(string usernameOrId)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.User[]> GetMultipleUsersByIdAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }
}