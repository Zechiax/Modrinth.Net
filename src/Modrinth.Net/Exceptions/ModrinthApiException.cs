using System.Net;
using Modrinth.Models.Errors;

namespace Modrinth.Exceptions;

/// <summary>
///     Represents an error that occurs when a request to the Modrinth API fails.
/// </summary>
public class ModrinthApiException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ModrinthApiException" /> class.
    /// </summary>
    /// <param name="message"> The message of the exception. </param>
    /// <param name="response"> The HTTP response message. </param>
    /// <param name="error"> The error returned by the Modrinth API. </param>
    /// <param name="innerException"> The inner exception. </param>
    public ModrinthApiException(string message, HttpResponseMessage? response = null,
        ResponseError? error = null, Exception? innerException = null) : base(message, innerException)
    {
        Error = error;
        Response = response;
    }

    /// <summary>
    ///     The HTTP response message.
    /// </summary>
    public HttpResponseMessage? Response { get; }

    /// <summary>
    ///     The error returned by the Modrinth API.
    /// </summary>
    public ResponseError? Error { get; }
}