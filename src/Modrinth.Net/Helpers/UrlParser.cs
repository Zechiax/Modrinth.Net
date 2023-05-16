using System.Text.RegularExpressions;

namespace Modrinth.Helpers;

/// <summary>
///     Helper class for parsing Modrinth urls
/// </summary>
public static class UrlParser
{
    /// <summary>
    ///     Regex for parsing Modrinth urls
    /// </summary>
    public static Regex ModrinthUrlRegex { get; } =
        new(
            @"(?:https:\/\/(?:www.)?modrinth.com\/(?:mod|modpack|resourcepack|plugin|shader|datapack)\/([\w!@$()`.+,""\-']{3,64}))",
            RegexOptions.Compiled);

    /// <summary>
    ///     Regex for validating slugs
    /// </summary>
    public static Regex ModrinthSlugRegex { get; } = new(@"(^[\w!@$()`.+,""\-']{3,64}$)", RegexOptions.Compiled);

    /// <summary>
    ///     Regex for validating ids
    /// </summary>
    public static Regex ModrinthIdRegex { get; } = new(@"(^[\w]{3,64}$)", RegexOptions.Compiled);

    /// <summary>
    ///     Tries to parse a Modrinth url
    /// </summary>
    /// <param name="url"> The url to parse </param>
    /// <param name="id"> The id of the project </param>
    /// <returns> Whether the url was successfully parsed </returns>
    public static bool TryParseModrinthUrl(string url, out string? id)
    {
        var match = ModrinthUrlRegex.Match(url);
        if (match.Success)
        {
            id = match.Groups[1].Value;
            return true;
        }

        id = null;
        return false;
    }

    /// <summary>
    ///     For client-side validation of slugs
    /// </summary>
    /// <param name="slug"> The slug to validate </param>
    /// <returns> Whether the slug is valid </returns>
    public static bool ValidateModrinthSlug(this string slug)
    {
        return ModrinthSlugRegex.IsMatch(slug);
    }

    /// <summary>
    ///     For client-side validation of ids
    /// </summary>
    /// <param name="id"> The id to validate </param>
    /// <returns> Whether the id is valid </returns>
    public static bool ValidateModrinthId(this string id)
    {
        return ModrinthIdRegex.IsMatch(id);
    }
}