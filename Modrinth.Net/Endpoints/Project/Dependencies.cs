namespace Modrinth.Endpoints.Project;

/// <summary>
///     The dependencies of a project
/// </summary>
public class Dependencies
{
    /// <summary>
    ///     An array of the projects that the project depends on
    /// </summary>
    public Models.Project[] Projects { get; set; } = null!;

    /// <summary>
    ///     The versions of the projects that the project depends on
    /// </summary>
    public System.Version[] Versions { get; set; } = null!;
}