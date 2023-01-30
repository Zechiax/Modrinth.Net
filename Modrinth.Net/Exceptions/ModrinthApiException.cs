using System.Net;

namespace Modrinth.Exceptions;

public class ModrinthApiException : Exception
{
    public ModrinthApiException(string message, HttpStatusCode statusCode, HttpContent content) : base(message)
    {
        StatusCode = statusCode;
        Content = content;
    }

    public HttpStatusCode StatusCode { get; }
    public HttpContent Content { get; }
}