namespace Modrinth.Models.Errors;

/// <summary>
///     An error response returned by the API
/// </summary>
public class ResponseError
{
    /// <summary>
    ///     The name of the error
    /// </summary>
    public string Error { get; set; } = null!;

    /// <summary>
    ///     The contents of the error
    /// </summary>
    public string Description { get; set; } = null!;
}