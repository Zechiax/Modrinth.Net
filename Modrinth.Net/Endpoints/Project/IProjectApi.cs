﻿using Modrinth.Models;
using Modrinth.Models.Facets;
using Index = Modrinth.Models.Enums.Index;

namespace Modrinth.Endpoints.Project;

public interface IProjectApi
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
    Task<Models.Project> GetAsync(string slugOrId);

    /// <summary>
    ///     Get a list of random projects
    /// </summary>
    /// <param name="count">The number of projects to return</param>
    /// <returns></returns>
    Task<Models.Project[]> GetRandomAsync(ulong count = 10);

    /// <summary>
    ///     Deletes project by slug or ID
    /// </summary>
    /// <param name="slugOrId">The slug or id of the project to be deleted</param>
    /// <returns></returns>
    Task DeleteAsync(string slugOrId);

    /// <summary>
    ///     Gets multiple projects by their IDs
    /// </summary>
    /// <param name="ids">IEnumerable of string ids</param>
    /// <returns></returns>
    Task<Models.Project[]> GetMultipleAsync(IEnumerable<string> ids);

    /// <summary>
    ///     Check project slug/ID validity
    /// </summary>
    /// <returns></returns>
    Task<SlugIdValidity> CheckIdSlugValidityAsync(string slugOrId);

    /// <summary>
    ///     Gets the dependencies of a project by slug or ID
    /// </summary>
    /// <param name="slugOrId"> The ID or slug of the project</param>
    /// <returns></returns>
    Task<Dependencies> GetDependenciesAsync(string slugOrId);

    /// <summary>
    ///     Follows a project by slug or ID
    /// </summary>
    /// <param name="slugOrId"></param>
    /// <returns></returns>
    Task FollowAsync(string slugOrId);

    /// <summary>
    ///     Unfollows a project by slug or ID
    /// </summary>
    /// <param name="slugOrId"></param>
    /// <returns></returns>
    Task UnfollowAsync(string slugOrId);
}