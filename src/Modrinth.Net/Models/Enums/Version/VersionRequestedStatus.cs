namespace Modrinth.Models.Enums.Version;

/// <summary>
///     The requested status of a version
/// </summary>
public enum VersionRequestedStatus
{
    /// <summary>
    ///     The version is requested to be listed
    /// </summary>
    Listed,

    /// <summary>
    ///     The version is requested to be archived
    /// </summary>
    Archived,

    /// <summary>
    ///     The version is requested to be in a draft
    /// </summary>
    Draft,

    /// <summary>
    ///     The version is requested to be unlisted
    /// </summary>
    Unlisted
}