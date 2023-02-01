#pragma warning disable CS8618
namespace Modrinth.Models.Enums;

public class License
{
    /// <summary>
    ///     The license id of a project, retrieved from the licenses get route
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