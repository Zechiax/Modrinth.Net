#pragma warning disable CS8618
using Modrinth.RestClient.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.RestClient.Models;

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
    /// The type of dependency that this version has
    /// </summary>
    [JsonProperty("dependency_type")]
    public DependencyType DependencyType { get; set; }
}