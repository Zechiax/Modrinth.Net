using Modrinth.Exceptions;
using Modrinth.Models.Tags;

namespace Modrinth.Endpoints.Tag;

/// <summary>
///     Endpoints for tags
///     Tags are common and reusable lists of metadata types such as categories or versions. Some can be applied to
///     projects and/or versions.
/// </summary>
public interface ITagEndpoint
{
    /// <summary>
    ///     Gets an array of categories, their icons, and applicable project types
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Category[]> GetCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets an array of loaders, their icons, and supported project types
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Loader[]> GetLoadersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets an array of game versions and information about them
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<GameVersion[]> GetGameVersionsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the text and title of a license
    /// </summary>
    /// <param name="id"> The ID of the license </param>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<LicenseTag> GetLicenseAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets an array of donation platforms and information about them
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<DonationPlatform[]> GetDonationPlatformsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets an array of valid report types
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<string[]> GetReportTypesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets an array of valid project types
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<string[]> GetProjectTypesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets an array of valid side types
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<string[]> GetSideTypesAsync(CancellationToken cancellationToken = default);
}