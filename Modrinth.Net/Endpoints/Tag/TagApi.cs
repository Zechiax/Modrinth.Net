using Modrinth.Models.Tags;

namespace Modrinth.Endpoints.Tag;

/// <inheritdoc />
public class TagApi : ITagApi
{
    private const string TagPathSegment = "tag";
    private readonly IRequester _client;

    public TagApi(IRequester client)
    {
        _client = client;
    }

    public async Task<Category[]> GetCategoriesAsync()
    {
        throw new NotImplementedException();
        // return await _client.Request(TagPathSegment, "category")
        //     .GetJsonAsync<Category[]>();
    }

    public async Task<Loader[]> GetLoadersAsync()
    {
        throw new NotImplementedException();
        // return await _client.Request(TagPathSegment, "loader")
        //     .GetJsonAsync<Loader[]>();
    }

    public async Task<GameVersion[]> GetGameVersionsAsync()
    {throw new NotImplementedException();
        // return await _client.Request(TagPathSegment, "game_version")
        //     .GetJsonAsync<GameVersion[]>();
    }

    public async Task<License[]> GetLicensesAsync()
    {
        throw new NotImplementedException();
        // return await _client.Request(TagPathSegment, "license")
        //     .GetJsonAsync<License[]>();
    }

    public async Task<DonationPlatform[]> GetDonationPlatformsAsync()
    {
        throw new NotImplementedException();
        // return await _client.Request(TagPathSegment, "donation_platform")
        //     .GetJsonAsync<DonationPlatform[]>();
    }

    public async Task<string[]> GetReportTypesAsync()
    {
        throw new NotImplementedException();
        // return await _client.Request(TagPathSegment, "report_type")
        //     .GetJsonAsync<string[]>();
    }
}