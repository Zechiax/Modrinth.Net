#pragma warning disable CS8618
namespace Modrinth.Models;

/// <summary>
///    A license that a project is under
/// </summary>
public class License
{
    /// <summary>
    ///     The SPDX license ID of a project
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     The long name of a license
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The URL to this license
    /// </summary>
    public string? Url { get; set; }
}