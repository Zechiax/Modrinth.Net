namespace Modrinth.Models.Enums;

/// <summary>
///     Side of a project (server or client)
/// </summary>
public enum Side
{
    /// <summary>
    ///     Needs to be installed on this side
    /// </summary>
    Required,

    /// <summary>
    ///     Optional to be installed on this side
    /// </summary>
    Optional,

    /// <summary>
    ///     Not supported on this side
    /// </summary>
    Unsupported,

    /// <summary>
    ///     Unknown side
    /// </summary>
    Unknown
}

/// <summary>
///    Extensions for <see cref="Side" />
/// </summary>
public static class SideExtensions
{
    /// <summary>
    ///     Convert Side to string for Modrinth API
    /// </summary>
    /// <param name="side"></param>
    /// <returns></returns>
    public static string ToModrinthString(this Side side)
    {
        return side switch
        {
            Side.Required => "required",
            Side.Optional => "optional",
            Side.Unsupported => "unsupported",
            Side.Unknown => "unknown",
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };
    }
}