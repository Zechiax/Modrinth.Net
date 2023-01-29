namespace Modrinth.Net.Models.Tags;

#pragma warning disable CS8618

public class License
{
    /// <summary>
    /// The short identifier of the license
    /// </summary>
    public string Short { get; set; }
    
    /// <summary>
    /// The full name of the license
    /// </summary>
    public string Name { get; set; }
}