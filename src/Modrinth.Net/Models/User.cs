#pragma warning disable CS8618
using System.Text.Json.Serialization;
using Modrinth.Helpers;
using Modrinth.Models.Enums;

namespace Modrinth.Models;

/// <summary>
///     A user on Modrinth
/// </summary>
public class User
{
    /// <summary>
    ///     A direct link to this user
    /// </summary>
    public string Url => this.GetDirectUrl();

    /// <summary>
    ///     The user's username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     The user's display name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     The user's email (only displayed if requesting your own account).
    /// </summary>
    /// <remarks>
    /// Requires <c>USER_READ_EMAIL</c> PAT scope.
    /// </remarks>
    public string? Email { get; set; }

    /// <summary>
    ///     A description of the user
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    ///     The user's id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     The user's avatar url
    /// </summary>
    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }

    /// <summary>
    ///     The time at which the user was created
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    ///     The user's role
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    ///     Various data relating to the user's payouts status (only displayed if requesting your own account). 
    /// </summary>
    /// <remarks>
    /// Requires <c>PAYOUTS_READ</c> PAT scope.
    /// </remarks>
    public PayoutData? PayoutData { get; set; }
}