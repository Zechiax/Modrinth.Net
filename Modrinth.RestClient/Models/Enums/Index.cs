using System.Runtime.Serialization;

namespace Modrinth.RestClient.Models.Enums;

/// <summary>
/// The sorting method used for sorting search results
/// </summary>
public enum Index
{
    [EnumMember(Value = "relevance")]
    Relevance,
    [EnumMember(Value = "downloads")]
    Downloads,
    [EnumMember(Value = "follows")]
    Follows,
    [EnumMember(Value = "newest")]
    Newest,
    [EnumMember(Value = "updated")]
    Updated
}