#pragma warning disable CS8618
using Modrinth.RestClient.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.RestClient.Models;

/// <summary>
/// Search Result Model
/// </summary>
public class SearchResult
{
    /// <summary>
    /// URL to the project of this search result
    /// </summary>
    public string Url => $"{ModrinthApi.ModrinthUrl}/{ProjectType.ToString().ToLower()}/{ProjectId}";
    
    /// <summary>
    /// The slug of a project, used for vanity URLs
    /// </summary>
    public string? Slug { get; set; }
    
    /// <summary>
    /// The title or name of the project
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// A short description of the project
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// A list of the categories that the project is in
    /// </summary>
    public string[] Categories { get; set; }

    /// <summary>
    /// The client side support of the project
    /// </summary>
    [JsonProperty("client_side")]
    public Side? ClientSide { get; set; }
    
    /// <summary>
    /// The server side support of the project
    /// </summary>
    [JsonProperty("server_side")]
    public Side? ServerSide { get; set; }

    /// <summary>
    /// The project type of the project
    /// </summary>
    [JsonProperty("project_type")]
    public ProjectType ProjectType { get; set; }

    /// <summary>
    /// The total number of downloads of the project
    /// </summary>
    public int Downloads { get; set; }

    /// <summary>
    /// The URL of the project's icon
    /// </summary>
    [JsonProperty("icon_url")]
    public string? IconUrl { get; set; }

    /// <summary>
    /// The ID of the project
    /// </summary>
    [JsonProperty("project_id")]
    public string ProjectId { get; set; }

    /// <summary>
    /// The username of the project's author
    /// </summary>
    public string Author { get; set; }
    
    /// <summary>
    /// A list of the categories that the project has which are not secondary
    /// </summary>
    [JsonProperty("display_categories")]
    public string[] DisplayCategories { get; set; }

    /// <summary>
    /// The date the project was created
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// The date the project was last modified
    /// </summary>
    [JsonProperty("date_modified")]
    public DateTime DateModified { get; set; }

    /// <summary>
    /// The total number of users following the project
    /// </summary>
    [JsonProperty("follows")]
    public int Followers { get; set; }

    /// <summary>
    /// The latest version of minecraft that this project supports
    /// </summary>
    [JsonProperty("latest_version")]
    public string? LatestVersion { get; set; }

    /// <summary>
    /// The license of the project
    /// </summary>
    public string License { get; set; }

    /// <summary>
    /// A list of the minecraft versions supported by the project
    /// </summary>
    public string[] Versions { get; set; }

    /// <summary>
    /// A list of images that have been uploaded to the project's gallery
    /// </summary>
    public string[] Gallery { get; set; }
}