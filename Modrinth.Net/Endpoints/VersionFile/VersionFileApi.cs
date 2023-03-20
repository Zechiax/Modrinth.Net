using Modrinth.Models.Enums;

namespace Modrinth.Endpoints.VersionFile;

public class VersionFileApi : IVersionFile
{
    private const string VersionFilePathSegment = "version_file";
    private readonly IRequester _client;

    public VersionFileApi(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<System.Version> GetVersionByHashAsync(string hash,
        HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(VersionFilePathSegment+ '/' + hash, UriKind.Relative);

        var parameters = new ParameterBuilder()
        {
            {"algorithm", hashAlgorithm.ToString().ToLower()}
        };
        
        parameters.AddToRequest(reqMsg);
        
        return await _client.GetJsonAsync<System.Version>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteVersionByHashAsync(string hash, HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
    {
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Delete;
        reqMsg.RequestUri = new Uri(VersionFilePathSegment+ '/' + hash, UriKind.Relative);

        var parameters = new ParameterBuilder()
        {
            {"algorithm", hashAlgorithm.ToString().ToLower()}
        };
        
        parameters.AddToRequest(reqMsg);
        
        await _client.SendAsync(reqMsg).ConfigureAwait(false);
    }
}