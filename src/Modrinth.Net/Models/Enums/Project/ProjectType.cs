namespace Modrinth.Models.Enums.Project;

/// <summary>
///     Type of project
/// </summary>
public enum ProjectType
{
    /// <summary>
    ///     Project is mod, also used for plugins and datapacks
    /// </summary>
    Mod,

    /// <summary>
    ///     Project is a modpack
    /// </summary>
    Modpack,

    /// <summary>
    ///     Project is a resourcepack
    /// </summary>
    Resourcepack,

    /// <summary>
    ///     Project is a shader
    /// </summary>
    Shader,

    /// <summary>
    ///     Project is a datapack
    /// </summary>
    Datapack,
    
    /// <summary>
    ///    Project is a plugin
    /// </summary>
    Plugin,
    
    /// <summary>
    ///   Generic project type
    /// </summary>
    Project
}