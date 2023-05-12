using Modrinth.Models.Enums.Project;

namespace Modrinth.Extensions;

/// <summary>
///     Extensions for <see cref="ProjectType" />
/// </summary>
public static class ProjectTypeExtensions
{
    /// <summary>
    ///     Convert ProjectType to string for Modrinth API
    /// </summary>
    /// <param name="projectType"></param>
    /// <returns></returns>
    public static string ToModrinthString(this ProjectType projectType)
    {
        return projectType switch
        {
            ProjectType.Mod => "mod",
            ProjectType.Modpack => "modpack",
            ProjectType.Resourcepack => "resourcepack",
            ProjectType.Shader => "shader",
            // Return lower string, this should work for all, but it is not guaranteed
            _ => projectType.ToString().ToLower()
        };
    }
}