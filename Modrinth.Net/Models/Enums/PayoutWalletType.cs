using System.Text.Json.Serialization;

namespace Modrinth.Models.Enums;

/// <summary>
///     The type of user's wallet
/// </summary>
public enum PayoutWalletType
{
    /// <summary>
    ///     Email
    /// </summary>
    Email,

    /// <summary>
    ///     Phone
    /// </summary>
    Phone,

    /// <summary>
    ///     User handle
    /// </summary>
    [JsonPropertyName("user_handle")] UserHandle
}