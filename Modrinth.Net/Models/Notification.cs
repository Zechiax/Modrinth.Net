using System.Text.Json.Serialization;
using Modrinth.Models.Enums;

namespace Modrinth.Models;

public class Notification
{
    /// <summary>
    ///     The ID of the notification
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     The ID of the user who received the notification
    /// </summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }

    /// <summary>
    ///     The type of notification
    /// </summary>
    public NotificationType? Type { get; set; }

    /// <summary>
    ///     The title of the notification
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     The body text of the notification
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     A link to the related project or version
    /// </summary>
    public string Link { get; set; }

    /// <summary>
    ///     Whether the notification has been read or not
    /// </summary>
    public bool Read { get; set; }

    /// <summary>
    ///     The time at which the notification was created
    /// </summary>
    public DateTime Created { get; set; }

    // TODO: Actions - haven't found model for this in the API docs
}