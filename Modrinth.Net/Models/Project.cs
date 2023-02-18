#pragma warning disable CS8618
using System.Drawing;
using Modrinth.Helpers;
using Modrinth.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.Models;

public class Project
{
    /// <summary>
    ///     A direct link to this project
    /// </summary>
    public string Url => this.GetDirectUrl();

    /// <summary>
    ///     The slug of a project, used for vanity URLs
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    ///     The title or name of the project
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     A short description of the project
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     A list of the categories that the project is in
    /// </summary>
    public string[] Categories { get; set; }

    /// <summary>
    ///     The client side support of the project
    /// </summary>
    [JsonProperty("client_side")]
    public Side ClientSide { get; set; }

    /// <summary>
    ///     The server side support of the project
    /// </summary>
    [JsonProperty("server_side")]
    public Side ServerSide { get; set; }

    /// <summary>
    ///     A long form description of the project
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    ///     An optional link to where to submit bugs or issues with the project
    /// </summary>
    [JsonProperty("issues_url")]
    public string? IssuesUrl { get; set; }

    /// <summary>
    ///     An optional link to the source code of the project
    /// </summary>
    [JsonProperty("source_url")]
    public string? SourceUrl { get; set; }

    /// <summary>
    ///     An optional link to the project's wiki page or other relevant information
    /// </summary>
    [JsonProperty("wiki_url")]
    public string? WikiUrl { get; set; }

    /// <summary>
    ///     An optional invite link to the project's discord
    /// </summary>
    [JsonProperty("discord_url")]
    public string? DiscordUrl { get; set; }

    /// <summary>
    ///     A list of donation links for the project
    /// </summary>
    [JsonProperty("donation_urls")]
    public DonationUrl[] DonationUrls { get; set; }

    /// <summary>
    ///     The project type of the project
    /// </summary>
    [JsonProperty("project_type")]
    public ProjectType ProjectType { get; set; }

    /// <summary>
    ///     The total number of downloads of the project
    /// </summary>
    public int Downloads { get; set; }

    /// <summary>
    ///     The URL of the project's icon
    /// </summary>
    [JsonProperty("icon_url")]
    public string? IconUrl { get; set; }

    /// <summary>
    ///     The ID of the project, encoded as a base62 string
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     The ID of the team that has ownership of this project
    /// </summary>
    public string Team { get; set; }

    /// <summary>
    ///     A message that a moderator sent regarding the project
    /// </summary>
    [JsonProperty("moderator_message")]
    public ModeratorMessage? ModeratorMessage { get; set; }

    /// <summary>
    ///     The date the project was published
    /// </summary>
    public DateTime Published { get; set; }

    /// <summary>
    ///     The date the project was last updated
    /// </summary>
    public DateTime Updated { get; set; }

    /// <summary>
    ///     The date the project's status was set to approved or unlisted
    /// </summary>
    public DateTime? Approved { get; set; }

    /// <summary>
    ///     The total number of users following the project
    /// </summary>
    public int Followers { get; set; }

    /// <summary>
    ///     The status of the project
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    ///     The SPDX license ID of a project
    /// </summary>
    public License? License { get; set; }

    /// <summary>
    ///     A list of the version IDs of the project (will never be empty unless draft status)
    /// </summary>
    public string[] Versions { get; set; }

    /// <summary>
    ///     A list of images that have been uploaded to the project's gallery
    /// </summary>
    public Gallery[]? Gallery { get; set; }
    
    /// <summary>
    /// The featured gallery image of the project
    /// </summary>
    [JsonProperty("featured_gallery")]
    public string? FeaturedGallery { get; set; }

    /// <summary>
    ///     The RGB color of the project, automatically generated from the project icon
    /// </summary>
    public Color? Color { get; set; }
    
    /// <summary>
    /// A list of all of the game versions supported by the project
    /// </summary>
    [JsonProperty("game_versions")]
    public string[] GameVersions { get; set; }
    
    /// <summary>
    ///A list of all of the loaders supported by the project
    /// </summary>
    [JsonProperty("loaders")]
    public string[] Loaders { get; set; }
}