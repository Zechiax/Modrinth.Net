using System.Text.Json.Serialization;

namespace Modrinth.Models;

/// <summary>
///     Information about a user's payout history
/// </summary>
public class PayoutHistory
{
    /// <summary>
    ///     The all-time balance accrued by this user in USD
    /// </summary>
    /// <returns></returns>
    [JsonPropertyName("all_time")]
    public string? AllTime { get; init; }

    /// <summary>
    ///     The amount in USD made by the user in the previous 30 days
    /// </summary>
    [JsonPropertyName("last_month")]
    public string? LastMonth { get; init; }

    /// <summary>
    ///     A history of all of the user's past transactions
    /// </summary>
    public UserPayoutHistoryEntry[] Payouts { get; init; } = null!;
}