using Modrinth.Extensions;
using Modrinth.Models;

namespace Modrinth.Helpers;

/// <summary>
///     Class for creating direct links to Modrinth
/// </summary>
public static class UrlCreatorHelper
{
    /// <summary>
    ///     Base Modrinth URL
    /// </summary>
    public const string ModrinthUrl = "https://modrinth.com";

    

    /// <summary>
    ///     Return direct link to the user on Modrinth
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public static string GetDirectUrl(this Project project)
    {
        return $"{ModrinthUrl}/{project.ProjectType.ToModrinthString()}/{project.Id}";
    }

    /// <summary>
    ///     Return direct link to the user on Modrinth
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetDirectUrl(this User user)
    {
        return $"{ModrinthUrl}/user/{user.Id}";
    }

    /// <summary>
    ///     Return direct link to the project of this search result on Modrinth
    /// </summary>
    /// <param name="searchResult"></param>
    /// <returns></returns>
    public static string GetDirectUrl(this SearchResult searchResult)
    {
        return $"{ModrinthUrl}/{searchResult.ProjectType.ToModrinthString()}/{searchResult.ProjectId}";
    }
}