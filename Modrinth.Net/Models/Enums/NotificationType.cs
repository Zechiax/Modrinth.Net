using System.Text.Json.Serialization;

namespace Modrinth.Models.Enums;

public enum NotificationType
{
    [JsonPropertyName("project_update")] ProjectUpdate,
    [JsonPropertyName("team_invite")] TeamInvite,
    [JsonPropertyName("status_update")] StatusUpdate
}