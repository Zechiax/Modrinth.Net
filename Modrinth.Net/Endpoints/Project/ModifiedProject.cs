using System.Text.Json.Serialization;
using Modrinth.Models;
using Modrinth.Models.Enums;
using Newtonsoft.Json;

namespace Modrinth.Endpoints.Project;

public class ModifiedProject
{
    [JsonPropertyName("slug")] public string Slug { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("categories")] public string[] Categories { get; set; }

    [JsonPropertyName("client_side")] public Side ClientSide { get; set; }

    [JsonPropertyName("server_side")] public Side ServerSide { get; set; }

    [JsonPropertyName("body")] public string Body { get; set; }

    [JsonPropertyName("additional_categories")]
    public string[][] AdditionalCategories { get; set; }

    [JsonPropertyName("issues_url")] public string IssuesUrl { get; set; }

    [JsonPropertyName("source_url")] public string SourceUrl { get; set; }

    [JsonPropertyName("wiki_url")] public string WikiUrl { get; set; }

    [JsonPropertyName("discord_url")] public string DiscordUrl { get; set; }

    [JsonPropertyName("donation_urls")] public DonationUrl[] DonationUrls { get; set; }

    [JsonPropertyName("license_id")] public string LicenseId { get; set; }

    [JsonPropertyName("license_url")] public string LicenseUrl { get; set; }

    [JsonPropertyName("status")] public ProjectStatus Status { get; set; }

    [JsonPropertyName("moderation_message")] public string ModerationMessage { get; set; }

    [JsonPropertyName("moderation_message_body")]
    public string ModerationMessageBody { get; set; }
}