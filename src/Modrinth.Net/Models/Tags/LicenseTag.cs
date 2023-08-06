namespace Modrinth.Models.Tags;

#pragma warning disable CS8618

/// <summary>
///     A license
/// </summary>
public class LicenseTag
{
    /// <summary>
    ///    The ID of the license
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// The full name of the license
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     The body of the license
    /// </summary>
    public string Body { get; set; }
}