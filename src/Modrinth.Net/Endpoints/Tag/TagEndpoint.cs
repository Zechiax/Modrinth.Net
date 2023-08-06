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
    public async Task<LicenseTag> GetLicenseAsync(string id)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "license" + '/' + id, UriKind.Relative);
        
        var license = await Requester.GetJsonAsync<LicenseTag>(reqMsg).ConfigureAwait(false);
        
        license.Id = id;
        
        return license;
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

    /// <inheritdoc />
    public async Task<string[]> GetProjectTypesAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "project_type", UriKind.Relative);

        return await Requester.GetJsonAsync<string[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string[]> GetSideTypesAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "side_type", UriKind.Relative);

        return await Requester.GetJsonAsync<string[]>(reqMsg).ConfigureAwait(false);
    }
}