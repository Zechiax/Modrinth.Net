namespace Modrinth.Endpoints.VersionFile;

public interface IVersionFile
{
    /// <summary>
    /// Get specific version by file hash
    /// </summary>
    /// <param name="hash">The hash of the file, considering its byte content, and encoded in hexadecimal</param>
    /// <param name="hashAlgorithm"></param>
    /// <returns></returns>
    Task<System.Version> GetVersionByHashAsync(string hash,
        Models.Enums.HashAlgorithm hashAlgorithm = Models.Enums.HashAlgorithm.Sha1);

    Task DeleteVersionByHashAsync(string hash,
        Models.Enums.HashAlgorithm hashAlgorithm = Models.Enums.HashAlgorithm.Sha1);
}