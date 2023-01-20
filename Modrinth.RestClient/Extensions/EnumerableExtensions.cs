﻿using System.Text;

namespace Modrinth.RestClient.Extensions;

public static class EnumerableExtensions
{
    public static string ToModrinthQueryString<T>(this IEnumerable<T> items)
    {
        // It needs to be in format like this: ["AABBCCDD", "EEFFGGHH"]
        var sb = new StringBuilder();
        sb.Append('[');
        foreach (var item in items)
        {
            sb.Append($"\"{item}\",");
        }
        sb.Remove(sb.Length - 1, 1);
        sb.Append(']');
        return sb.ToString();
    }
}