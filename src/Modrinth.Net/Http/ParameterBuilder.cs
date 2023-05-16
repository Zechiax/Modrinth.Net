using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace Modrinth.Http;

/// <summary>
///     A class used to build a collection of parameters
/// </summary>
public class ParameterBuilder : IEnumerable
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ParameterBuilder" /> class
    /// </summary>
    public ParameterBuilder()
    {
        Parameters = new NameValueCollection();
    }

    private NameValueCollection Parameters { get; }

    /// <inheritdoc />
    public IEnumerator GetEnumerator()
    {
        return Parameters.GetEnumerator();
    }

    /// <summary>
    ///     Adds a string parameter to the collection
    /// </summary>
    /// <param name="key"> The key of the parameter </param>
    /// <param name="value"> The value of the parameter </param>
    /// <param name="ignoreNull"> Whether or not to ignore null values </param>
    /// <returns> The current instance of <see cref="ParameterBuilder" /> </returns>
    public ParameterBuilder Add(string key, string? value, bool ignoreNull = true)
    {
        if (value is null && ignoreNull) return this;
        Parameters.Add(key, value ?? string.Empty);
        return this;
    }

    /// <summary>
    ///     Adds a parameter to the collection, ToString() is called on the value
    /// </summary>
    /// <param name="key"> The key of the parameter </param>
    /// <param name="value"> The value of the parameter </param>
    /// <param name="ignoreNull"> Whether or not to ignore null values </param>
    /// <returns> The current instance of <see cref="ParameterBuilder" /> </returns>
    public ParameterBuilder Add(string key, object? value, bool ignoreNull = true)
    {
        if (value is null && ignoreNull) return this;
        Parameters.Add(key, value?.ToString() ?? string.Empty);
        return this;
    }

    /// <summary>
    ///     Returns a string representation of the parameters, empty or null values are ignored
    /// </summary>
    /// <returns> A string representation of the parameters </returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (var key in Parameters.AllKeys)
        {
            if (string.IsNullOrEmpty(key)) continue;
            var values = Parameters.GetValues(key);
            if (values is null) continue;
            foreach (var value in values)
            {
                if (value == string.Empty) continue;
                sb.Append($"{key}={value}&");
            }
        }

        return sb.ToString();
    }

    /// <summary>
    ///     Adds the parameters to the request URI of the specified <see cref="HttpRequestMessage" /> as a query string
    /// </summary>
    /// <param name="request"></param>
    public void AddToRequest(HttpRequestMessage request)
    {
        var query = ToString();
        if (query == string.Empty) return;
        request.RequestUri = new Uri(request.RequestUri + "?" + query, UriKind.RelativeOrAbsolute);
    }
}