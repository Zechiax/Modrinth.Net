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
    public ModrinthApiException(string message, HttpResponseMessage response,
        ResponseError? error = null, Exception? innerException = null) : base(message, innerException)
    {
        Error = error;
        Response = response;
    }

    /// <summary>
    ///     The status code of the HTTP response.
    /// </summary>
    [Obsolete("Use Response.StatusCode instead. This property will be removed in a future version of the API.")]
    public HttpStatusCode StatusCode => Response.StatusCode;

    /// <summary>
    ///     The HTTP response content.
    /// </summary>
    [Obsolete("Use Response.Content instead. This property will be removed in a future version of the API.")]
    public HttpContent Content => Response.Content;

    /// <summary>
    ///     The HTTP response message.
    /// </summary>
    public HttpResponseMessage Response { get; }

    /// <summary>
    ///     The error returned by the Modrinth API.
    /// </summary>
    public ResponseError? Error { get; }
}