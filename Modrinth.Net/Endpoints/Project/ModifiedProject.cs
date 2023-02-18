using Modrinth.Models;
using Modrinth.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.Endpoints.Project;

public class ModifiedProject
{
    [JsonProperty("slug")] public string Slug { get; set; }

    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("description")] public string Description { get; set; }

    [JsonProperty("categories")] public string[] Categories { get; set; }

    [JsonProperty("client_side")] public Side ClientSide { get; set; }

    [JsonProperty("server_side")] public Side ServerSide { get; set; }

    [JsonProperty("body")] public string Body { get; set; }

    [JsonProperty("additional_categories")]
    public string[][] AdditionalCategories { get; set; }

    [JsonProperty("issues_url")] public string IssuesUrl { get; set; }

    [JsonProperty("source_url")] public string SourceUrl { get; set; }

    [JsonProperty("wiki_url")] public string WikiUrl { get; set; }

    [JsonProperty("discord_url")] public string DiscordUrl { get; set; }

    [JsonProperty("donation_urls")] public DonationUrl[] DonationUrls { get; set; }

    [JsonProperty("license_id")] public string LicenseId { get; set; }

    [JsonProperty("license_url")] public string LicenseUrl { get; set; }

    [JsonProperty("status")] public ProjectStatus Status { get; set; }

    [JsonProperty("moderation_message")] public string ModerationMessage { get; set; }

    [JsonProperty("moderation_message_body")]
    public string ModerationMessageBody { get; set; }
}