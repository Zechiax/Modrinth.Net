namespace Modrinth.Models.Enums;

/// <summary>
/// The permissions that a user has on a project
/// </summary>
[Flags]
public enum Permissions
{
    /// <summary>
    /// Permission to upload a new version
    /// </summary>
    UploadVersion = 0,
    /// <summary>
    /// Permission to delete a version
    /// </summary>
    DeleteVersion = 1,
    /// <summary>
    /// Permission to edit project's details
    /// </summary>
    EditDetails = 2,
    /// <summary>
    /// Permission to edit project's description
    /// </summary>
    EditBody = 3,
    /// <summary>
    /// Permission to manage invites
    /// </summary>
    ManageInvites = 4,
    /// <summary>
    /// Permission to remove a member
    /// </summary>
    RemoveMember = 5,
    /// <summary>
    /// Permission to edit a member's permissions
    /// </summary>
    EditMember = 6,
    /// <summary>
    /// Permission to delete a project
    /// </summary>
    DeleteProject = 7,
    /// <summary>
    /// Permission to view project analytics
    /// </summary>
    ViewAnalytics = 8,
    /// <summary>
    /// Permission to view payouts
    /// </summary>
    ViewPayouts = 9
}