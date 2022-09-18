using System.Text;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Polly;
using RestEase;

namespace Modrinth.RestClient;


/// <summary>
/// Base for creating new clients using RestEase from <see cref="IModrinthApi"/> interface
/// </summary>
public static class ModrinthApi
{
    /// <summary>
    /// API Url of the production server
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string BaseUrl = "https://api.modrinth.com/v2";

    /// <summary>
    /// API Url of the staging server
    /// </summary>
    public const string StagingBaseUrl = "https://staging-api.modrinth.com/v2";

    /// <summary>
    /// Initializes new RestClient from <see cref="IModrinthApi"/>
    /// </summary>
    /// <param name="userAgent">User-Agent header you want to use while communicating with Modrinth API, it's recommended to set 'a uniquely-identifying' one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)</param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl"/></param>
    /// <returns>New RestEase RestClient from <see cref="IModrinthApi"/> interface</returns>
    public static IModrinthApi NewClient(string url = BaseUrl, string userAgent = "")
    {
        var api = new RestEase.RestClient(url)
        {
            // Custom query builder to comply with Modrinth's API specification
            QueryStringBuilder = new ModrinthQueryBuilder()
        }.For<IModrinthApi>();

        api.UserAgentHeader = userAgent;

        return api;
    }
    
    /// <summary>
    /// Initializes new RestClient from <see cref="IModrinthApi"/> with specified policy
    /// </summary>
    /// <param name="userAgent">User-Agent header you want to use while communicating with Modrinth API, it's recommended to set 'a uniquely-identifying' one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)</param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl"/></param>
    /// <param name="policy">Use custom Polly resiliency policy <a href="https://github.com/App-vNext/Polly#resilience-policies=">see Polly</a></param>
    /// <returns>New RestEase RestClient from <see cref="IModrinthApi"/> interface</returns>
    public static IModrinthApi NewClient(IAsyncPolicy<HttpResponseMessage> policy, string url = BaseUrl, string userAgent = "")
    {
        var api = new RestEase.RestClient(url, new PolicyHttpMessageHandler(policy))
        {
            // Custom query builder to comply with Modrinth's API specification
            QueryStringBuilder = new ModrinthQueryBuilder()
        }.For<IModrinthApi>();

        api.UserAgentHeader = userAgent;

        return api;
    }
}

/// <summary>
/// Custom query builder for Modrinth, as Modrinth requires arrays to be formatted as ids=["someId1", "someId2"]
/// </summary>
public class ModrinthQueryBuilder : QueryStringBuilder
{
    /// <summary>
    /// Override of the Build method for QueryStringBuilder to provide formatting for Modrinth
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public override string Build(QueryStringBuilderInfo info)
    {
        if (!info.QueryParams.Any())
        {
            return string.Empty;
        }
        
        var query = GetKeyValues(info.QueryParams);
        
        return BuildTheQueryString(query);
    }

    private static string BuildTheQueryString(IDictionary<string, IList<string>> queryParams)
    {
        var sb = new StringBuilder();
        var counter = queryParams.Count;
        
        // Let's build the query string
        foreach (var (key, list) in queryParams)
        {
            counter--;
            if (list.Count > 1)
            {
                sb.Append($"{key}=[");
                var ids = list.Select(x => string.Concat('"', x, '"'));
                sb.Append(string.Join(',', ids));
                sb.Append(']');
            }
            else
            {
                sb.Append($"{key}={list.First()}");
            }

            // If there are other values, add &
            if (counter > 0)
            {
                sb.Append('&');
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Joins values of the same key to only 1 key, for id:5, id:6 it will create id:[5,6] and so on
    /// </summary>
    /// <param name="kvp"></param>
    /// <returns></returns>
    private static SortedDictionary<string, IList<string>> GetKeyValues(IEnumerable<KeyValuePair<string, string?>> kvp)
    {
        var query = new SortedDictionary<string, IList<string>>();
        var keyValuePairs = kvp as KeyValuePair<string, string?>[] ?? kvp.ToArray();
        
        // Each key will have list of values as its value
        foreach (var (key, value) in keyValuePairs)
        {
            // Key is already in the list
            if (query.ContainsKey(key) && value is not null)
            {
                var list = query[key];
                list.Add(value);
            }
            // First time we see the key
            else if (value is not null)
            {
                query.Add(key, new List<string> {value});
            }
        }

        return query;
    }
}