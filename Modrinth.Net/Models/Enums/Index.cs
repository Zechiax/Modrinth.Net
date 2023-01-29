using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Modrinth.Net.Models.Enums;

/// <summary>
/// The sorting method used for sorting search results
/// </summary>
public enum Index
{
    Relevance,
    Downloads,
    Follows,
    Newest,
    Updated
}