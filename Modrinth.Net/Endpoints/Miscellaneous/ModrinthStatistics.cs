namespace Modrinth.Endpoints.Miscellaneous;

public class ModrinthStatistics
{
    /// <summary>
    /// Number of projects on Modrinth
    /// </summary>
    public int Projects { get; set; }
    /// <summary>
    ///   Number of versions on Modrinth
    /// </summary>
    public int Versions { get; set; }
    /// <summary>
    /// Number of version files on Modrinth
    /// </summary>
    public int Files { get; set; }
    /// <summary>
    /// Number of authors (users with projects) on Modrinth
    /// </summary>
    public int Authors { get; set; }
}