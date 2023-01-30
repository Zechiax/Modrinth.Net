using Modrinth.Models;
using Version = Modrinth.Models.Version;

namespace Modrinth.Extensions;

public static class ProjectExtensions
{
    /// <summary>
    ///     Returns a direct link to version details of specific version
    /// </summary>
    /// <param name="project"></param>
    /// <param name="version">Version for which the URL should be returned</param>
    /// <returns></returns>
    public static string GetVersionUrl(this Project project, Version version)
    {
        return project.GetVersionUrl(version.Id);
    }

    /// <summary>
    ///     Returns a direct link to version details of specific version
    /// </summary>
    /// <param name="project"></param>
    /// <param name="versionId">ID of the version</param>
    /// <returns></returns>
    public static string GetVersionUrl(this Project project, string versionId)
    {
        return $"{project.Url}/version/{versionId}";
    }
}