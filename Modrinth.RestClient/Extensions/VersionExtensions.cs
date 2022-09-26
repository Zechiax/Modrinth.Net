using Modrinth.RestClient.Models;
using Version = Modrinth.RestClient.Models.Version;

namespace Modrinth.RestClient.Extensions;

public static class VersionExtensions
{
    public static string GetUrl(this Version version, Project project)
    {
        return project.GetVersionUrl(version);
    }
}