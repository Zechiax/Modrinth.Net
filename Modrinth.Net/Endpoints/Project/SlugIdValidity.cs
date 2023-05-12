#pragma warning disable CS8618
namespace Modrinth.Endpoints.Project;

/// <summary>
///     Returned by <see cref="IProjectEndpoint.CheckIdSlugValidityAsync" /> if the slug or id is valid
/// </summary>
public class SlugIdValidity
{
    /// <summary>
    ///     The id of the project
    /// </summary>
    public string Id { get; set; }
}