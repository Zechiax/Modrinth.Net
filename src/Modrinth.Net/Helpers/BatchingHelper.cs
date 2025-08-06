namespace Modrinth.Helpers;

internal static class BatchingHelper
{
    internal static async Task<TResult[]> GetFromBatchesAsync<TResult>(
        IEnumerable<string> ids,
        Func<string[], CancellationToken, Task<TResult[]>> fetchBatchAsync,
        int batchSize = 100,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ids);
        ArgumentNullException.ThrowIfNull(fetchBatchAsync);

        var idBatches = ids.Chunk(batchSize).ToArray();
        var tasks = idBatches.Select(batch => fetchBatchAsync(batch, cancellationToken));
        var results = await Task.WhenAll(tasks).ConfigureAwait(false);
        
        // Flatten the results from all batches into a single array
        return results.SelectMany(x => x).ToArray();
    }
}