#pragma warning disable CS8618
using Modrinth.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.Models;

/// <summary>
/// Team Member Model
/// </summary>
public class TeamMember
{
    /// <summary>
    /// A direct link to a user of this TeamMember
    /// </summary>
    public string Url => User.Url;
    
    /// <summary>
    /// The ID of the team this team member is a member of
    /// </summary>
    [JsonProperty("team_id")]
    public string TeamId { get; set; }

    /// <summary>
    /// The User
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// The user's role on the team
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// The user's permissions in bitflag format (requires authorization to view)
    /// </summary>
    public Permissions? Permissions { get; set; }

    /// <summary>
    /// Whether or not the user has accepted to be on the team (requires authorization to view)
    /// </summary>
    public bool Accepted { get; set; }
}