using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Modrinth.Models.Enums;

public enum PayoutWalletType
{
    Email,
    Phone,
    [JsonPropertyName("user_handle")] UserHandle
}