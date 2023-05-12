using System.Text.Json.Serialization;
using Modrinth.Models.Enums.File;

#pragma warning disable CS8618
namespace Modrinth.Models;

public class File
{
    /// <summary>
    ///     Hashes of the file
    /// </summary>
    public Hashes Hashes { get; set; }

    /// <summary>
    ///     A direct link to the file
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     The name of the file
    /// </summary>
    public string FileName { get; set; }

    public bool Primary { get; set; }

    /// <summary>
    ///     The size of the file in bytes
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    ///     The type of the additional file, used mainly for adding resource packs to datapacks
    /// </summary>
    [JsonPropertyName("file_type")]
    public FileType? FileType { get; set; }
}