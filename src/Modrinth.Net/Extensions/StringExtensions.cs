namespace Modrinth.Extensions;

/// <summary>
///     Extensions for <see cref="string" />
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Escapes the specified input string with double quotes if it contains the specified character.
    /// </summary>
    /// <param name="text">The input string to check for the specified character.</param>
    /// <param name="contains">The character to check if it is contained in the input string. Defaults to a space character.</param>
    /// <returns>
    ///     The input string enclosed within double quotes if it contains the specified character;
    ///     otherwise, the input string as is.
    /// </returns>
    public static string EscapeIfContains(this string text, char contains = ' ')
    {
        return !string.IsNullOrEmpty(text) && text.Contains(contains) ? string.Concat('"', text, '"') : text;
    }
}