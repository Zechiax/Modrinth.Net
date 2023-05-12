namespace Modrinth.Models.Enums;

/// <summary>
///    The type of a game version
/// </summary>
public enum GameVersionType
{
    /// <summary>
    ///     Alpha version
    /// </summary>
    Alpha,

    /// <summary>
    ///     Beta version
    /// </summary>
    Beta,

    /// <summary>
    ///     Stable game version
    /// </summary>
    Release,

    /// <summary>
    ///     Testing version
    /// </summary>
    Snapshot
}