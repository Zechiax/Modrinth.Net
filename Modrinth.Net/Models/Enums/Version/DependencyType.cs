namespace Modrinth.Models.Enums.Version;

public enum DependencyType
{
    /// <summary>
    ///     Dependency is required
    /// </summary>
    Required,

    /// <summary>
    ///     Dependency is optional
    /// </summary>
    Optional,

    /// <summary>
    ///     Dependency is incompatible
    /// </summary>
    Incompatible,

    /// <summary>
    ///     Dependency is embedded
    /// </summary>
    Embedded
}