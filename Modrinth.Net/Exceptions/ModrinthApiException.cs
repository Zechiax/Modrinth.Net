using System.Net;

namespace Modrinth.Net.Exceptions;

public class ModrinthApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public HttpContent Content { get; }
    
    public ModrinthApiException(string message, HttpStatusCode statusCode, HttpContent content) : base(message)
    {
        StatusCode = statusCode;
        Content = content;
    }
}