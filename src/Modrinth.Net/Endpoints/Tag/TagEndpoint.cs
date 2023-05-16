using Modrinth.Http;
using Modrinth.Models.Tags;

namespace Modrinth.Endpoints.Tag;

/// <inheritdoc cref="Modrinth.Endpoints.Tag.ITagEndpoint" />
public class TagEndpoint : Endpoint, ITagEndpoint
{
    private const string TagPathSegment = "tag";

    /// <inheritdoc />
    public TagEndpoint(IRequester requester) : base(requester)
    {
    }

    /// <inheritdoc />
    public async Task<Category[]> GetCategoriesAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "category", UriKind.Relative);

        return await Requester.GetJsonAsync<Category[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Loader[]> GetLoadersAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "loader", UriKind.Relative);

        return await Requester.GetJsonAsync<Loader[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<GameVersion[]> GetGameVersionsAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "game_version", UriKind.Relative);

        return await Requester.GetJsonAsync<GameVersion[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<License[]> GetLicensesAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "license", UriKind.Relative);

        return await Requester.GetJsonAsync<License[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DonationPlatform[]> GetDonationPlatformsAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "donation_platform", UriKind.Relative);

        return await Requester.GetJsonAsync<DonationPlatform[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string[]> GetReportTypesAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "report_type", UriKind.Relative);

        return await Requester.GetJsonAsync<string[]>(reqMsg).ConfigureAwait(false);
    }
}