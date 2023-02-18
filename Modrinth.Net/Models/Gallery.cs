#pragma warning disable CS8618
namespace Modrinth.Models;

public class Gallery
{
    /// <summary>
    ///     The URL of the gallery image
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     Whether the image is featured in the gallery
    /// </summary>
    public bool Featured { get; set; }

    /// <summary>
    ///     The title of the gallery image
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     The description of the gallery image
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     The date and time the gallery image was created
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// The order of the gallery image. Gallery images are sorted by this field and then alphabetically by title.
    /// </summary>
    public int Ordering { get; set; }
}