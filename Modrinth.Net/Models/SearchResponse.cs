using Newtonsoft.Json;

#pragma warning disable CS8618
namespace Modrinth.Models;

public class SearchResponse
{
    /// <summary>
    ///     The list of results
    /// </summary>
    public SearchResult[] Hits { get; set; }

    /// <summary>
    ///     The number of results that were skipped by the query
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    ///     The number of results that were returned by the query
    /// </summary>
    public int Limit { get; set; }

    /// <summary>
    ///     The total number of results that match the query
    /// </summary>
    [JsonProperty("total_hits")]
    public int TotalHits { get; set; }
}