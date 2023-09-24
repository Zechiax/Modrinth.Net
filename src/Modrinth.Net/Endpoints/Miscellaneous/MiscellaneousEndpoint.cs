using Modrinth.Http;

namespace Modrinth.Endpoints.Miscellaneous;

/// <inheritdoc cref="Modrinth.Endpoints.Miscellaneous.IMiscellaneousEndpoint" />
public class MiscellaneousEndpoint : Endpoint, IMiscellaneousEndpoint
{
    /// <inheritdoc />
    public MiscellaneousEndpoint(IRequester requester) : base(requester)
    {
    }

    /// <inheritdoc />
    public async Task<ModrinthStatistics> GetStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("statistics", UriKind.Relative);

        return await Requester.GetJsonAsync<ModrinthStatistics>(reqMsg, cancellationToken).ConfigureAwait(false);
    }
}