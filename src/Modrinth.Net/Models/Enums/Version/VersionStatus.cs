namespace Modrinth.Models.Enums.Version;

/// <summary>
///     The status of a version
/// </summary>
public enum VersionStatus
{
    /// <summary>
    ///     The version is listed
    /// </summary>
    Listed,

    /// <summary>
    ///     The version is archived
    /// </summary>
    Archived,

    /// <summary>
    ///     The version is in a draft
    /// </summary>
    Draft,

    /// <summary>
    ///     The version is unlisted
    /// </summary>
    Unlisted,

    /// <summary>
    ///     The version is scheduled for release
    /// </summary>
    Scheduled,

    /// <summary>
    ///     The version status is unknown
    /// </summary>
    Unknown
}