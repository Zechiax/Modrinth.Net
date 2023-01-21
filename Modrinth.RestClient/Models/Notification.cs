using System.Text.Json.Serialization;
using Modrinth.RestClient.Models.Enums;

namespace Modrinth.RestClient.Endpoints.User;

public class Notification
{
    public string Id { get; set; }
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string Link { get; set; }
    public bool Read { get; set; }
    public DateTime Created { get; set; }
}