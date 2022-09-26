using Modrinth.RestClient.Models;
using Version = Modrinth.RestClient.Models.Version;

namespace Modrinth.RestClient.Extensions;

public static class VersionExtensions
{
    /// <summary>
    /// Returns the URL to this version details
    /// </summary>
    /// <param name="version"></param>
    /// <param name="project">The project to which this version belongs</param>
    /// <returns></returns>
    public static string GetUrl(this Version version, Project project)
    {
        return project.GetVersionUrl(version);
    }
}