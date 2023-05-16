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