using Newtonsoft.Json;

namespace Modrinth.Models.Enums.File;

public enum FileType
{
    [JsonProperty("required-resource-pack")]
    RequiredResourcePack,
    [JsonProperty("optional-resource-pack")]
    OptionalResourcePack
}