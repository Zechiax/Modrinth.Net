﻿using Modrinth.Exceptions;
using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.VersionFile;

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
    ///    Deletes a version by its file hash
    /// </summary>
    /// <param name="hash"> The hash of the file, considering its byte content, and encoded in hexadecimal </param>
    /// <param name="hashAlgorithm"> The hash algorithm used to generate the hash </param>
    /// <returns></returns>
    Task DeleteVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1);
}