using Modrinth.Exceptions;
using Modrinth.Models;
using Index = Modrinth.Models.Enums.Index;

namespace Modrinth.Endpoints.Project;

/// <summary>
///     Project endpoints
/// </summary>
public interface IProjectEndpoint
{
    /// <summary>
    ///     Search Modrinth for project by it's name
    /// </summary>
    /// <param name="query">The query to search for</param>
    /// <param name="facets">Facets to filter the search by</param>
    /// <param name="index">The sorting method used for sorting search results</param>
    /// <param name="offset">The offset into the search. Skips this number of results</param>
    /// <param name="limit">The number of results returned by the search</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<SearchResponse> SearchAsync(
        string query,
        Index index = Index.Downloads,
        ulong offset = 0,
        ulong limit = 10,
        FacetCollection? facets = null);

    /// <summary>
    ///     Gets project by slug or ID
    /// </summary>
    /// <param name="slugOrId">The ID or slug of the project</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Project> GetAsync(string slugOrId);

    /// <summary>
    ///     Get a list of random projects
    /// </summary>
    /// <param name="count">The number of projects to return</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Project[]> GetRandomAsync(ulong count = 10);

    /// <summary>
    ///     Deletes project by slug or ID
    /// </summary>
    /// <param name="slugOrId">The slug or id of the project to be deleted</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task DeleteAsync(string slugOrId);

    /// <summary>
    ///     Gets multiple projects by their IDs
    /// </summary>
    /// <param name="ids">IEnumerable of string ids</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Project[]> GetMultipleAsync(IEnumerable<string> ids);

    /// <summary>
    ///     Check project slug/ID validity
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<SlugIdValidity> CheckIdSlugValidityAsync(string slugOrId);

    /// <summary>
    ///     Gets the dependencies of a project by slug or ID
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Dependencies> GetDependenciesAsync(string slugOrId);

    /// <summary>
    ///     Follows a project by slug or ID
    /// </summary>
    /// <param name="slugOrId"></param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task FollowAsync(string slugOrId);

    /// <summary>
    ///     Unfollows a project by slug or ID
    /// </summary>
    /// <param name="slugOrId"></param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task UnfollowAsync(string slugOrId);

    /// <summary>
    ///     Deletes a project's icon by slug or ID
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task DeleteIconAsync(string slugOrId);

    /// <summary>
    ///     Changes a project's icon by slug or ID
    ///     The new icon may be up to 256KiB in size.
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project</param>
    /// <param name="iconPath"> The local path to the icon</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task ChangeIconAsync(string slugOrId, string iconPath);

    /// <summary>
    ///     Adds a new gallery image to a project
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project</param>
    /// <param name="imagePath"> The local path to the image</param>
    /// <param name="featured"> Whether the image should be featured</param>
    /// <param name="title"> Optional title for the image</param>
    /// <param name="description"> Optional description for the image</param>
    /// <param name="ordering"> Optional ordering for the image</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task AddGalleryImageAsync(string slugOrId, string imagePath, bool featured, string? title = null,
        string? description = null, ulong? ordering = null);

    /// <summary>
    ///     Modifies an existing gallery image
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project</param>
    /// <param name="url"> URL link of the image to modify</param>
    /// <param name="featured"> Whether the image should be featured</param>
    /// <param name="title"> Optional new title for the image</param>
    /// <param name="description"> Optional new description for the image</param>
    /// <param name="ordering"> Optional new ordering for the image</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task ModifyGalleryImageAsync(string slugOrId, string url, bool? featured = null, string? title = null,
        string? description = null, ulong? ordering = null);

    /// <summary>
    ///     Deletes an existing gallery image
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project</param>
    /// <param name="url"> URL link of the image to delete</param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task DeleteGalleryImageAsync(string slugOrId, string url);
}