using System.Text.Json.Serialization;

namespace Modrinth.Models.Enums;

/// <summary>
/// The type of a notification
/// </summary>
public enum NotificationType
{
    /// <summary>
    ///   The project was updates
    /// </summary>
    [JsonPropertyName("project_update")] ProjectUpdate,
    /// <summary>
    /// A team invite received
    /// </summary>
    [JsonPropertyName("team_invite")] TeamInvite,
    /// <summary>
    /// Project status update
    /// </summary>
    [JsonPropertyName("status_update")] StatusUpdate
}