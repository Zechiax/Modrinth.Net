namespace Modrinth.Models.Enums;

[Flags]
public enum Permissions
{
    UploadVersion = 0,
    DeleteVersion = 1,
    EditDetails = 2,
    EditBody = 3,
    ManageInvites = 4,
    RemoveMember = 5,
    EditMember = 6,
    DeleteProject = 7
}