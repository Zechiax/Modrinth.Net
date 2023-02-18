using Newtonsoft.Json;

namespace Modrinth.Models.Enums;

public enum NotificationType
{
    [JsonProperty("project_update")] ProjectUpdate,
    [JsonProperty("team_invite")] TeamInvite,
    [JsonProperty("status_update")] StatusUpdate
}