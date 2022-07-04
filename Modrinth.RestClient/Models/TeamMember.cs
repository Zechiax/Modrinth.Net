#pragma warning disable CS8618
using Modrinth.RestClient.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.RestClient.Models;

public class TeamMember
{
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
    public Permissions Permissions { get; set; }

    /// <summary>
    /// Whether or not the user has accepted to be on the team (requires authorization to view)
    /// </summary>
    public bool Accepted { get; set; }
}