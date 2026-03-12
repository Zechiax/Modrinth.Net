using Modrinth.Models.Enums.Project;

namespace Modrinth.Extensions;

/// <summary>
///     Extensions for <see cref="ProjectVersionType" />
/// </summary>
public static class ProjectVersionTypeExtensions
{
    /// <summary>
    ///     Converts ProjectVersionType to a string fit for the Modrinth API
    /// </summary>
    /// <param name="projectVersionType"></param>
    /// <returns></returns>
    public static string ToModrinthString(this ProjectVersionType projectVersionType)
    {
        return projectVersionType switch
        {
            ProjectVersionType.Alpha => "alpha",
            ProjectVersionType.Beta  => "beta",
            ProjectVersionType.Release  => "release",
            // Return lower string, this should work for all, but it is not guaranteed
            _ => projectVersionType.ToString().ToLower()
        };
    }
}