using System.Text;

namespace Modrinth.Extensions;

/// <summary>
///     Extensions for <see cref="IEnumerable{T}" />
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    ///     Converts an <see cref="IEnumerable{T}" /> to a query string for Modrinth
    ///     <para> It needs to be in format like this: ["AABBCCDD", "EEFFGGHH"] </para>
    /// </summary>
    /// <param name="items"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string ToModrinthQueryString<T>(this IEnumerable<T> items)
    {
        // It needs to be in format like this: ["AABBCCDD", "EEFFGGHH"]
        var sb = new StringBuilder();
        sb.Append('[');

        var any = false;
        foreach (var item in items)
        {
            sb.Append($"\"{item}\",");
            any = true;
        }

        if (any) sb.Remove(sb.Length - 1, 1);


        sb.Append(']');
        return sb.ToString();
    }
}