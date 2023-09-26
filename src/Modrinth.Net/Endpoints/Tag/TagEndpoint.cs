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
    public async Task<Category[]> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "category", UriKind.Relative);

        return await Requester.GetJsonAsync<Category[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Loader[]> GetLoadersAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "loader", UriKind.Relative);

        return await Requester.GetJsonAsync<Loader[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<GameVersion[]> GetGameVersionsAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "game_version", UriKind.Relative);

        return await Requester.GetJsonAsync<GameVersion[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<LicenseTag> GetLicenseAsync(string id, CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "license" + '/' + id, UriKind.Relative);

        var license = await Requester.GetJsonAsync<LicenseTag>(reqMsg, cancellationToken).ConfigureAwait(false);

        license.Id = id;

        return license;
    }


    /// <inheritdoc />
    public async Task<DonationPlatform[]> GetDonationPlatformsAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "donation_platform", UriKind.Relative);

        return await Requester.GetJsonAsync<DonationPlatform[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string[]> GetReportTypesAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "report_type", UriKind.Relative);

        return await Requester.GetJsonAsync<string[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string[]> GetProjectTypesAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "project_type", UriKind.Relative);

        return await Requester.GetJsonAsync<string[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string[]> GetSideTypesAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "side_type", UriKind.Relative);

        return await Requester.GetJsonAsync<string[]>(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}