﻿using System.Text.Json.Serialization;
using Modrinth.Models.Enums;

namespace Modrinth.Models.Tags;

#pragma warning disable CS8618

public class GameVersion
{
    /// <summary>
    ///     The name/number of the game version
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    ///     The type of the game version
    /// </summary>
    [JsonPropertyName("version_type")]
    public GameVersionType VersionType { get; set; }

    /// <summary>
    ///     The date of the game version release
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Whether or not this is a major version, used for Featured Versions
    /// </summary>
    public bool Major { get; set; }
}