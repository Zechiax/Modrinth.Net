using Modrinth.Net.Models;
using Version = Modrinth.Net.Models.Version;

namespace Modrinth.Net.Extensions;

public static class VersionExtensions
{
    /// <summary>
    /// Returns a direct link to this version details
    /// </summary>
    /// <param name="version"></param>
    /// <param name="project">The project to which this version belongs</param>
    /// <returns></returns>
    public static string GetUrl(this Models.Version version, Project project)
    {
        return project.GetVersionUrl(version);
    }
}