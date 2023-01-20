using Flurl.Http;
using Modrinth.RestClient.Models.Tags;

namespace Modrinth.RestClient.Endpoints.Tag;

public class TagApi : ITagApi
{
    private readonly FlurlClient _client;

    public TagApi(FlurlClient client)
    {
        _client = client;
    }
    
    public async Task<Category[]> GetCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Loader[]> GetLoadersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<GameVersion[]> GetGameVersionsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<License[]> GetLicensesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<DonationPlatform[]> GetDonationPlatformsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<string[]> GetReportTypesAsync()
    {
        throw new NotImplementedException();
    }
}