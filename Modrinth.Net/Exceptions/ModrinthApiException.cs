using System.Net;
using Modrinth.Models.Errors;

namespace Modrinth.Exceptions;

public class ModrinthApiException : Exception
{
    public ModrinthApiException(string message, HttpStatusCode statusCode, HttpContent content,
        Exception? innerException, ResponseError? error = null) : base(message, innerException)
    {
        StatusCode = statusCode;
        Content = content;
        Error = error;
    }

    public HttpStatusCode StatusCode { get; }
    public HttpContent Content { get; }
    
    public ResponseError? Error { get; set; }
}