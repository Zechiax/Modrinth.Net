using System.Text;
using Microsoft.Extensions.Http;
using Modrinth.RestClient.Extensions;
using Modrinth.RestClient.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using RestEase;
using Index = Modrinth.RestClient.Models.Enums.Index;

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
    /// Returns optimal serializer settings to be used when serializing queries
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static JsonSerializerSettings GetJsonSerializerSettings()
    {
        return new JsonSerializerSettings();
    }

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
            QueryStringBuilder = new ModrinthQueryBuilder(),
            JsonSerializerSettings = GetJsonSerializerSettings()
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
    [Obsolete("NewClient with Polly resiliency policy will be removed in Modrinth.RestClient >= 2.6.0 to use the least amount of dependencies")]
    public static IModrinthApi NewClient(IAsyncPolicy<HttpResponseMessage> policy, string url = BaseUrl, string userAgent = "")
    {
        var api = new RestEase.RestClient(url, new PolicyHttpMessageHandler(policy))
        {
            // Custom query builder to comply with Modrinth's API specification
            QueryStringBuilder = new ModrinthQueryBuilder(),
            JsonSerializerSettings = GetJsonSerializerSettings()
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
        return !info.QueryParams.Any() ? 
            string.Empty 
            : 
            BuildTheQueryString(GetKeyValues(info.QueryParams));
    }

    private static string BuildTheQueryString(IDictionary<string, IList<string>> queryParams)
    {
        var sb = new StringBuilder();
        var counter = queryParams.Count;
        
        // Let's build the query string
        foreach (var (key, list) in queryParams)
        {
            counter--;
            
            // When we use ids, we have to make it into an array, even with only 1 value
            if (list.Count > 1 || key == "ids")
            {
                sb.Append($"{key}=[");
                var ids = list.Select(x => string.Concat('"', x, '"'));
                sb.Append(string.Join(',', ids));
                sb.Append(']');
            }
            else
            {
                // TODO: Make better solution, this is because RestEase call .ToString() on enums and that will parse it with capital letters, but Modrinth won't work with that, so we have to lower it
                sb.Append(Enum.TryParse<Index>(list.First(), out _) || Enum.TryParse<HashAlgorithm>(list.First(), out _)
                    ? $"{key}={list.First().ToLower().EscapeIfContains()}"
                    : $"{key}={list.First().EscapeIfContains()}");
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
            if (query.ContainsKey(key) && (value is not null))
            {
                var list = query[key];
                list.Add(value);
            }
            // First time we see the key
            else if (value is not null || key == "ids")
            {
                query.Add(key, value is null ? new List<string>() : new List<string> {value});
            }
        }

        return query;
    }
}