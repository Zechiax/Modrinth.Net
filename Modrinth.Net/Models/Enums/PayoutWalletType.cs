using System.Text.Json.Serialization;

namespace Modrinth.Models.Enums;

public enum PayoutWalletType
{
    Email,
    Phone,
    [JsonPropertyName("user_handle")] UserHandle
}