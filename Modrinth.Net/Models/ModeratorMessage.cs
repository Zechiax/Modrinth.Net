#pragma warning disable CS8618
namespace Modrinth.Models;

public class ModeratorMessage
{
    /// <summary>
    /// The message that a moderator has left for the project
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// The longer body of the message that a moderator has left for the project
    /// </summary>
    public string? Body { get; set; }
}