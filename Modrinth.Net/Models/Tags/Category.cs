#pragma warning disable CS8618
using System.Text.Json.Serialization;
using Modrinth.Models.Enums.Project;

namespace Modrinth.Models.Tags;

/// <summary>
///  A category of a project
/// </summary>
public class Category
{
    /// <summary>
    ///     The SVG icon of a category
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    ///     The name of the category
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The project type this category is applicable to
    /// </summary>
    [JsonPropertyName("project_type")]
    public ProjectType ProjectType { get; set; }

    /// <summary>
    ///     The header under which the category should go
    /// </summary>
    public string Header { get; set; }
}