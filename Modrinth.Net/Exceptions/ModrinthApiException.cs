using System.Net;

namespace Modrinth.Net.Exceptions;

public class ModrinthApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    
    public ModrinthApiException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}