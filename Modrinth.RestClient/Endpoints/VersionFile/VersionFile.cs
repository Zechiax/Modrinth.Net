using Modrinth.RestClient.Models.Enums;

namespace Modrinth.RestClient.Endpoints.VersionFile;

public class VersionFile : IVersionFile
{
    public async Task<Version> GetVersionByHashAsync(string hash, HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        throw new NotImplementedException();
    }
}