using Modrinth.Models;
using Version = Modrinth.Models.Version;

namespace Modrinth.Extensions;

/// <summary>
///     Extensions for <see cref="Models.Version" />
/// </summary>
public static class VersionExtensions
{
    /// <summary>
    ///     Returns a direct link to this version details
    /// </summary>
    /// <param name="version"></param>
    /// <param name="project">The project to which this version belongs</param>
    /// <returns></returns>
    public static string GetUrl(this Version version, Project project)
    {
        return project.GetVersionUrl(version);
    }
}