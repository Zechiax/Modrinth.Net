namespace Modrinth;

// where do I put this???
/// <summary>
/// Wraps a stream with a filename, used for uploading files to Modrinth.
/// </summary>
public class UploadableFile
{
    /// <summary>
    /// The name of our file
    /// </summary>
    public string FileName { get; }
    /// <summary>
    /// The stream to access our file data
    /// </summary>
    public Stream Stream { get; }

    /// <summary>
    /// Creates a new Uploadable file by wrapping a stream with a filename
    /// </summary>
    /// <param name="fileName">The filename to use when uploading to Modrinth</param>
    /// <param name="stream">The stream containing file data to upload</param>
    public UploadableFile(string fileName, Stream stream)
    {
        FileName = fileName;
        Stream = stream;
    }

    /// <summary>
    /// Creates a new Uploadable file by opening a FileStream with the provided path
    /// </summary>
    /// <param name="path">The file path to open a stream with</param>
    public UploadableFile(string path)
    {
        FileName = Path.GetFileName(path);

        if (!File.Exists(path))
            throw new FileNotFoundException();

        Stream = new FileStream(path, FileMode.Open);
    }
}