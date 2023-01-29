using Modrinth.Models;
using Modrinth.Models.Enums;

namespace Modrinth.Helpers;

/// <summary>
/// Class for creating direct links to Modrinth
/// </summary>
public static class UrlCreatorHelper
{
    /// <summary>
    /// Base Modrinth URL
    /// </summary>
    public const string ModrinthUrl = "https://modrinth.com";

    /// <summary>
    /// Returns formatted type used in Modrinth links to specific project
    /// </summary>
    /// <param name="projectType"></param>
    /// <returns></returns>
    private static string GetProjectUrlType(ProjectType projectType)
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

    /// <summary>
    /// Return direct link to the user on Modrinth
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public static string GetDirectUrl(this Project project)
    {
        return $"{ModrinthUrl}/{GetProjectUrlType(project.ProjectType)}/{project.Id}";
    }
    
    /// <summary>
    /// Return direct link to the user on Modrinth
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetDirectUrl(this User user)
    {
        return $"{ModrinthUrl}/user/{user.Id}";
    }

    /// <summary>
    /// Return direct link to the project of this search result on Modrinth
    /// </summary>
    /// <param name="searchResult"></param>
    /// <returns></returns>
    public static string GetDirectUrl(this SearchResult searchResult)
    {
        return $"{ModrinthUrl}/{GetProjectUrlType(searchResult.ProjectType)}/{searchResult.ProjectId}";
    }
}