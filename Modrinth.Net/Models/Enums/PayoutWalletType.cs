using Newtonsoft.Json;

namespace Modrinth.Models.Enums;

public enum PayoutWalletType
{
    Email,
    Phone,
    [JsonProperty("user_handle")] UserHandle
}