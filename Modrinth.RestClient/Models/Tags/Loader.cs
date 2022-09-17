using Modrinth.RestClient.Models.Enums;
using Newtonsoft.Json;

#pragma warning disable CS8618
namespace Modrinth.RestClient.Models.Tags;

public class Loader
{
    /// <summary>
    /// The SVG icon of a loader
    /// </summary>
    public string Icon { get; set; }
    
    /// <summary>
    /// The name of the loader
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The project types that this loader is applicable to
    /// </summary>
    [JsonProperty("supported_project_types")]
    public ProjectType[] SupportedProjectTypes { get; set; }
}