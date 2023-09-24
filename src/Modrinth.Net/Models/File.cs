using System.Text.Json.Serialization;
using Modrinth.JsonConverters;
using Modrinth.Models.Enums.File;

#pragma warning disable CS8618
namespace Modrinth.Models;

/// <summary>
///     A file of a project version
/// </summary>
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

    /// <summary>
    ///     Whether the file is the primary file of the version
    /// </summary>
    public bool Primary { get; set; }

    /// <summary>
    ///     The size of the file in bytes
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    ///     The type of the additional file, used mainly for adding resource packs to datapacks
    /// </summary>
    [JsonPropertyName("file_type")]
    [JsonConverter(typeof(JsonStringEnumConverterEx<FileType>))]
    public FileType? FileType { get; set; }
}