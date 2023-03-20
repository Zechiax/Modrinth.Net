using Flurl.Http;

namespace Modrinth.Endpoints.Miscellaneous;

public class MiscellaneousApi : IMiscellaneousApi
{
    private readonly IRequester _client;

    public MiscellaneousApi(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<ModrinthStatistics> GetStatisticsAsync()
    {
        return await _client.Request("statistics")
            .GetJsonAsync<ModrinthStatistics>();
    }
}