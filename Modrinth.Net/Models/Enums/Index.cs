namespace Modrinth.Models.Enums;

/// <summary>
///     The sorting method used for sorting search results
/// </summary>
public enum Index
{
    /// <summary>
    ///    Sort by relevance
    /// </summary>
    Relevance,
    /// <summary>
    ///   Sort by number of downloads
    /// </summary>
    Downloads,
    /// <summary>
    ///  Sort by number of followers
    /// </summary>
    Follows,
    /// <summary>
    /// Sort by release date from newest to oldest
    /// </summary>
    Newest,
    /// <summary>
    ///  Sort by last updated date from newest to oldest
    /// </summary>
    Updated
}