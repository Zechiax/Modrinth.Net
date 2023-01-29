using Flurl.Http;
using Modrinth.Net.Models.Tags;

namespace Modrinth.Net.Endpoints.Tag;

/// <inheritdoc />
public class TagApi : ITagApi
{
    private readonly FlurlClient _client;

    private const string TagPathSegment = "tag"; 
    public TagApi(FlurlClient client)
    {
        _client = client;
    }
    
    public async Task<Category[]> GetCategoriesAsync()
    {
        return await _client.Request(TagPathSegment, "category")
            .GetJsonAsync<Category[]>();
    }

    public async Task<Loader[]> GetLoadersAsync()
    {
        return await _client.Request(TagPathSegment, "loader")
            .GetJsonAsync<Loader[]>();
    }

    public async Task<GameVersion[]> GetGameVersionsAsync()
    {
        return await _client.Request(TagPathSegment, "game_version")
            .GetJsonAsync<GameVersion[]>();
    }

    public async Task<License[]> GetLicensesAsync()
    {
        return await _client.Request(TagPathSegment, "license")
            .GetJsonAsync<License[]>();
    }

    public async Task<DonationPlatform[]> GetDonationPlatformsAsync()
    {
        return await _client.Request(TagPathSegment, "donation_platform")
            .GetJsonAsync<DonationPlatform[]>();
    }

    public async Task<string[]> GetReportTypesAsync()
    {
        return await _client.Request(TagPathSegment, "report_type")
            .GetJsonAsync<string[]>();
    }
}