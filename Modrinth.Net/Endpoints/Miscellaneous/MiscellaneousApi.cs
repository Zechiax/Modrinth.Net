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
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri("statistics", UriKind.Relative);

        return await _client.GetJsonAsync<ModrinthStatistics>(reqMsg);
    }
}