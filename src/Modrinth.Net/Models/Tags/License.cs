namespace Modrinth.Models.Tags;

#pragma warning disable CS8618

/// <summary>
///     A license
/// </summary>
public class License
{
    /// <summary>
    ///     The short identifier of the license
    /// </summary>
    public string Short { get; set; }

    /// <summary>
    ///     The full name of the license
    /// </summary>
    public string Name { get; set; }
}