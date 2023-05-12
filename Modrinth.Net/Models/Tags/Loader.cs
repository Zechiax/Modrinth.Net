using System.Text.Json.Serialization;
using Modrinth.Models.Enums.Project;

#pragma warning disable CS8618
namespace Modrinth.Models.Tags;

/// <summary>
///     A mod loader
/// </summary>
public class Loader
{
    /// <summary>
    ///     The SVG icon of a loader
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    ///     The name of the loader
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The project types that this loader is applicable to
    /// </summary>
    [JsonPropertyName("supported_project_types")]
    public ProjectType[] SupportedProjectTypes { get; set; }
}