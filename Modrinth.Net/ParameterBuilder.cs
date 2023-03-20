using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace Modrinth;

public class ParameterBuilder : IEnumerable
{
    public ParameterBuilder()
    {
        Parameters = new NameValueCollection();
    }

    private NameValueCollection Parameters { get; }

    public IEnumerator GetEnumerator()
    {
        return Parameters.GetEnumerator();
    }

    public ParameterBuilder Add(string key, string? value, bool ignoreNull = true)
    {
        if (value is null && ignoreNull) return this;
        Parameters.Add(key, value ?? string.Empty);
        return this;
    }

    public ParameterBuilder Add(string key, object? value, bool ignoreNull = true)
    {
        if (value is null && ignoreNull) return this;
        Parameters.Add(key, value?.ToString() ?? string.Empty);
        return this;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (var key in Parameters.AllKeys)
        {
            if (key is null || key == string.Empty) continue;
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

    public void AddToRequest(HttpRequestMessage request)
    {
        var query = ToString();
        if (query == string.Empty) return;
        request.RequestUri = new Uri(request.RequestUri + "?" + query, UriKind.RelativeOrAbsolute);
    }
}