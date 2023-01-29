#pragma warning disable CS8618
using Modrinth.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.Models;

public class Dependency
{
    /// <summary>
    /// The ID of the version that this version depends on
    /// </summary>
    [JsonProperty("version_id")]
    public string? VersionId { get; set; }

    /// <summary>
    /// The ID of the project that this version depends on
    /// </summary>
    [JsonProperty("project_id")] 
    public string? ProjectId { get; set; }

    /// <summary>
    /// The file name of the dependency, mostly used for showing external dependencies on modpacks
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// The type of dependency that this version has
    /// </summary>
    [JsonProperty("dependency_type")]
    public DependencyType DependencyType { get; set; }
}