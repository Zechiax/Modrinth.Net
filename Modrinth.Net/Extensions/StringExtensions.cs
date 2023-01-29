namespace Modrinth.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Add text in quotes if text contains specific char
    /// </summary>
    /// <param name="text"></param>
    /// <param name="contains">The char to check if the text contains</param>
    /// <returns></returns>
    public static string EscapeIfContains(this string text, char contains = ' ')
    {
        return text.Contains(contains) ? $"\"{text}\"" : text;
    }
}