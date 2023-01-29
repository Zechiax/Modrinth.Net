using Modrinth.Models.Tags;

namespace Modrinth.Endpoints.Tag;

public interface ITagApi
{
    /// <summary>
    /// Gets an array of categories, their icons, and applicable project types
    /// </summary>
    /// <returns></returns>
    Task<Category[]> GetCategoriesAsync();
    
    /// <summary>
    /// Gets an array of loaders, their icons, and supported project types
    /// </summary>
    /// <returns></returns>
    Task<Loader[]> GetLoadersAsync();
    
    /// <summary>
    /// Gets an array of game versions and information about them
    /// </summary>
    /// <returns></returns>
    Task<GameVersion[]> GetGameVersionsAsync();
    
    /// <summary>
    /// Gets an array of licenses and information about them
    /// </summary>
    /// <returns></returns>
    Task<License[]> GetLicensesAsync();
    
    /// <summary>
    /// Gets an array of donation platforms and information about them
    /// </summary>
    /// <returns></returns>
    Task<DonationPlatform[]> GetDonationPlatformsAsync();
    
    /// <summary>
    /// Gets an array of valid report types
    /// </summary>
    /// <returns></returns>
    Task<string[]> GetReportTypesAsync();
}