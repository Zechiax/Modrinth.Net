using Modrinth.Models.Tags;

namespace Modrinth.Endpoints.Tag;

/// <inheritdoc />
public class TagEndpoint : ITagEndpoint
{
    private const string TagPathSegment = "tag";
    private readonly IRequester _client;

    public TagEndpoint(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Category[]> GetCategoriesAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "category", UriKind.Relative);

        return await _client.GetJsonAsync<Category[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Loader[]> GetLoadersAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "loader", UriKind.Relative);

        return await _client.GetJsonAsync<Loader[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<GameVersion[]> GetGameVersionsAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "game_version", UriKind.Relative);

        return await _client.GetJsonAsync<GameVersion[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<License[]> GetLicensesAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "license", UriKind.Relative);

        return await _client.GetJsonAsync<License[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DonationPlatform[]> GetDonationPlatformsAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "donation_platform", UriKind.Relative);

        return await _client.GetJsonAsync<DonationPlatform[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string[]> GetReportTypesAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "report_type", UriKind.Relative);

        return await _client.GetJsonAsync<string[]>(reqMsg).ConfigureAwait(false);
    }
}