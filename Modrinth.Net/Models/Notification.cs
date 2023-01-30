using Modrinth.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.Models;

public class Notification
{
    public string Id { get; set; }

    [JsonProperty("user_id")] public string UserId { get; set; }

    public NotificationType Type { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string Link { get; set; }
    public bool Read { get; set; }
    public DateTime Created { get; set; }
}