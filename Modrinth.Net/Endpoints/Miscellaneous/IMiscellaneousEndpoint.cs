using Modrinth.Exceptions;

namespace Modrinth.Endpoints.Miscellaneous;

public interface IMiscellaneousEndpoint
{
    /// <summary>
    ///     Various statistics about this Modrinth instance
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    public Task<ModrinthStatistics> GetStatisticsAsync();
}