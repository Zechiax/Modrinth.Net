using System.Text.Json.Serialization;

namespace Modrinth.Models.Enums.File;

public enum FileType
{
    [JsonPropertyName("required-resource-pack")]
    RequiredResourcePack,

    [JsonPropertyName("optional-resource-pack")]
    OptionalResourcePack
}