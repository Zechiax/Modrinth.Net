using System.Text.Json.Serialization;

namespace Modrinth.Models.Enums.File;

/// <summary>
///     The type of a additional file, used mainly for adding resource packs to datapacks
/// </summary>
public enum FileType
{
    /// <summary>
    ///     Required resource pack
    /// </summary>
    [JsonPropertyName("required-resource-pack")]
    RequiredResourcePack,

    /// <summary>
    ///     Optional resource pack
    /// </summary>
    [JsonPropertyName("optional-resource-pack")]
    OptionalResourcePack
}