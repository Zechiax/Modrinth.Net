using Modrinth.Exceptions;
using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.VersionFile;

/// <summary>
///     Version file endpoints
/// </summary>
public interface IVersionFileEndpoint
{
    /// <summary>
    ///     Get specific version by file hash
    /// </summary>
    /// <param name="hash">The hash of the file, considering its byte content, and encoded in hexadecimal</param>
    /// <param name="hashAlgorithm"> The hash algorithm used to generate the hash </param>
    /// <returns></returns>
    /// <exception cref="ModrinthApiException"> Thrown when the API returns an error or the request fails </exception>
    Task<Models.Version> GetVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1);

    /// <summary>
    ///     Deletes a version by its file hash
    /// </summary>
    /// <param name="hash"> The hash of the file, considering its byte content, and encoded in hexadecimal </param>
    /// <param name="hashAlgorithm"> The hash algorithm used to generate the hash </param>
    /// <returns></returns>
    Task DeleteVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1);

    /// <summary>
    ///     Get multiple versions by file hash
    /// </summary>
    /// <param name="hashes"> The hashes of the files, considering their byte content, and encoded in hexadecimal </param>
    /// <param name="hashAlgorithm"> The hash algorithm used to generate the hash </param>
    /// <returns> A dictionary of hashes and their respective versions </returns>
    Task<IDictionary<string, Models.Version>> GetMultipleVersionsByHashAsync(string[] hashes,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1);
}