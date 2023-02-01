using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.VersionFile;

public interface IVersionFile
{
    /// <summary>
    ///     Get specific version by file hash
    /// </summary>
    /// <param name="hash">The hash of the file, considering its byte content, and encoded in hexadecimal</param>
    /// <param name="hashAlgorithm"></param>
    /// <returns></returns>
    Task<System.Version> GetVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1);

    Task DeleteVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1);
}