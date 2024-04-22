namespace Modrinth.Models.Enums.Project;

/// <summary>
///     Status of a project
/// </summary>
public enum ProjectStatus
{
    /// <summary>
    ///     Project is approved
    /// </summary>
    Approved,

    /// <summary>
    ///     Project is rejected
    /// </summary>
    Rejected,

    /// <summary>
    ///     Project is draft, not yet submitted
    /// </summary>
    Draft,

    /// <summary>
    ///     Project is unlisted
    /// </summary>
    Unlisted,

    /// <summary>
    ///     Project is archived
    /// </summary>
    Archived,

    /// <summary>
    ///     Project is being processed
    /// </summary>
    Processing,

    /// <summary>
    ///     Project status is unknown
    /// </summary>
    Unknown,
    
    /// <summary>
    ///     Project is withheld
    /// </summary>
    Withheld,
    
    /// <summary>
    ///     Project is scheduled for release
    /// </summary>
    Scheduled,
    
    /// <summary>
    ///    Project is private
    /// </summary>
    Private
}