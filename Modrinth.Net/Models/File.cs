#pragma warning disable CS8618
namespace Modrinth.Models;

public class File
{
    /// <summary>
    /// A map of hashes of the file. The key is the hashing algorithm and the value is the string version of the hash.
    /// </summary>
    public Hashes Hashes { get; set; }

    /// <summary>
    /// A direct link to the file
    /// </summary>
    public string Url { get; set; }
    
    /// <summary>
    /// The name of the file
    /// </summary>
    public string FileName { get; set; }
    
    public bool Primary { get; set; }
    
    /// <summary>
    /// The size of the file in bytes
    /// </summary>
    public int Size { get; set; }
}