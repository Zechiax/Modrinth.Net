namespace Modrinth.RestClient.Endpoints.Project;

public class VersionApi : IVersionApi
{
    public async Task<Version> GetVersionByIdAsync(string versionId)
    {
        throw new NotImplementedException();
    }

    public async Task<Version[]> GetProjectVersionListAsync(string slugOrId)
    {
        throw new NotImplementedException();
    }

    public async Task<Version[]> GetMultipleVersionsAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }
}