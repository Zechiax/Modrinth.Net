using Modrinth.Http;

namespace Modrinth.Endpoints.Miscellaneous;

public class MiscellaneousEndpoint : IMiscellaneousEndpoint
{
    private readonly IRequester _client;

    public MiscellaneousEndpoint(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<ModrinthStatistics> GetStatisticsAsync()
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("statistics", UriKind.Relative);

        return await _client.GetJsonAsync<ModrinthStatistics>(reqMsg).ConfigureAwait(false);
    }
}