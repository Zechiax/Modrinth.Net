using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Modrinth.Models.Enums.File;

public enum FileType
{
    [JsonPropertyName("required-resource-pack")]
    RequiredResourcePack,

    [JsonPropertyName("optional-resource-pack")]
    OptionalResourcePack
}