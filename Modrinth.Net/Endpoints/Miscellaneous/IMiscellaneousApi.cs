namespace Modrinth.Endpoints.Miscellaneous;

public interface IMiscellaneousApi
{
    /// <summary>
    ///     Various statistics about this Modrinth instance
    /// </summary>
    /// <returns></returns>
    public Task<ModrinthStatistics> GetStatisticsAsync();
}