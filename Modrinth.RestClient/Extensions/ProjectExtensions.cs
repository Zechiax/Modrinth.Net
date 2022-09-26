using Modrinth.RestClient.Models;
using Version = Modrinth.RestClient.Models.Version;

namespace Modrinth.RestClient.Extensions;

public static class ProjectExtensions
{
    public static string GetVersionUrl(this Project project, Version version)
    {
        return project.GetVersionUrl(version.Id);
    }
    
    public static string GetVersionUrl(this Project project, string versionId)
    {
        return $"{project.Url}/version/{versionId}";
    }
}