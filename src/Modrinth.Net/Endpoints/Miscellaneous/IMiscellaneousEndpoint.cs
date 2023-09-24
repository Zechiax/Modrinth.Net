using Modrinth.Exceptions;

namespace Modrinth.Endpoints.Miscellaneous;

/// <summary>
///     Miscellaneous endpoints
/// </summary>
public interface IMiscellaneousEndpoint
{
    /// <summary>
    ///     Various statistics about this Modrinth instance
    /// </summary>
    /// <returns></returns>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    public Task<ModrinthStatistics> GetStatisticsAsync(CancellationToken cancellationToken = default);
}