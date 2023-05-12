#pragma warning disable CS8618
namespace Modrinth.Models;

/// <summary>
///     Hashes of a file
/// </summary>
public class Hashes
{
    /// <summary>
    ///     SHA-512 hash of the file
    /// </summary>
    public string Sha512 { get; set; }

    /// <summary>
    ///     SHA-1 hash of the file
    /// </summary>
    public string Sha1 { get; set; }
}