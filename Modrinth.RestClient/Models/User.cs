#pragma warning disable CS8618
using Modrinth.RestClient.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.RestClient.Models;

public class User
{
    /// <summary>
    /// Direct URL of this user
    /// </summary>
    public string Url => $"{ModrinthApi.ModrinthUrl}/user/{Id}";
    
    /// <summary>
    /// The user's username
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// The user's display name
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// The user's email (only your own is ever displayed)
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// A description of the user
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// The user's id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The user's github id
    /// </summary>
    [JsonProperty("github_id")]
    public int GithubId { get; set; }

    /// <summary>
    /// The user's avatar url
    /// </summary>
    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; }

    /// <summary>
    /// The time at which the user was created
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// The user's role
    /// </summary>
    public Role Role { get; set; }
}