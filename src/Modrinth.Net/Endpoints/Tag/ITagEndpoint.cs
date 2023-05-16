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
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Category[]> GetCategoriesAsync();

    /// <summary>
    ///     Gets an array of loaders, their icons, and supported project types
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Loader[]> GetLoadersAsync();

    /// <summary>
    ///     Gets an array of game versions and information about them
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<GameVersion[]> GetGameVersionsAsync();

    /// <summary>
    ///     Gets an array of licenses and information about them
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<License[]> GetLicensesAsync();

    /// <summary>
    ///     Gets an array of donation platforms and information about them
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<DonationPlatform[]> GetDonationPlatformsAsync();

    /// <summary>
    ///     Gets an array of valid report types
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<string[]> GetReportTypesAsync();
}