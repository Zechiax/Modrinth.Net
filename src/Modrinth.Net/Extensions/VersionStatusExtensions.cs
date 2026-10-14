using Modrinth.Models.Enums.Project;
using Modrinth.Models.Enums.Version;

namespace Modrinth.Extensions;

/// <summary>
///     Extensions for <see cref="VersionStatus" />
/// </summary>
public static class VersionStatusExtensions
{
    /// <summary>
    ///     Converts VersionStatus to a string fit for the Modrinth API
    /// </summary>
    /// <param name="versionStatus"></param>
    /// <returns></returns>
    public static string ToModrinthString(this VersionStatus versionStatus)
    {
        return versionStatus switch
        {
            VersionStatus.Archived => "archived",
            VersionStatus.Draft => "draft",
            VersionStatus.Listed => "listed",
            VersionStatus.Scheduled => "scheduled",
            VersionStatus.Unknown => "unknown",
            VersionStatus.Unlisted => "unlisted",
            // Return lower string, this should work for all, but it is not guaranteed
            _ => versionStatus.ToString().ToLower()
        };
    }
}