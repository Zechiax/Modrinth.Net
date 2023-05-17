﻿namespace Modrinth.Models.Enums.Project;

/// <summary>
///     Type of a project
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
    Shader
}