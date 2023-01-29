using System.Text.Json.Serialization;

namespace Modrinth.Net.Models.Enums;

public enum NotificationType
{
    [JsonPropertyName("project_update")]
    ProjectUpdate,
    [JsonPropertyName("team_invite")]
    TeamInvite,
}