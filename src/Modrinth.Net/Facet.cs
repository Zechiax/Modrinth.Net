using Modrinth.Extensions;
using Modrinth.Models.Enums.Project;

namespace Modrinth;

/// <summary>
///     A facet for the filtering of results
/// </summary>
public abstract class Facet
{
    /// <summary>
    ///     Creates a new facet for the filtering by category or loader
    /// </summary>
    /// <param name="value"> The loader or category to filter the results from </param>
    /// <returns> The created facet </returns>
    public static Facet<string> Category(string value)
    {
        return new Facet<string>(FacetType.Categories, value);
    }

    /// <summary>
    ///     Creates a new facet for the filtering by Minecraft version
    /// </summary>
    /// <param name="value"> The minecraft version to filter the results from </param>
    /// <returns> The created facet </returns>
    public static Facet<string> Version(string value)
    {
        return new Facet<string>(FacetType.Versions, value);
    }

    /// <summary>
    ///     Creates a new facet for the filtering by license
    /// </summary>
    /// <param name="value"> The license ID to filter the results from </param>
    /// <returns> The created facet </returns>
    public static Facet<string> License(string value)
    {
        return new Facet<string>(FacetType.License, value);
    }

    /// <summary>
    ///     Creates a new facet for the filtering by project type
    /// </summary>
    /// <param name="projectType">  The project type to filter the results from </param>
    /// <returns> The created facet </returns>
    public static Facet<string> ProjectType(ProjectType projectType)
    {
        return new Facet<string>(FacetType.ProjectType, projectType.ToModrinthString());
    }
}

/// <summary>
///     A facet for the filtering of results, with a specific type
/// </summary>
/// <typeparam name="T"></typeparam>
public class Facet<T> : Facet
{
    internal Facet(FacetType type, T value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    ///     The type of the facet
    /// </summary>
    public FacetType Type { get; }

    /// <summary>
    ///     The value of the facet
    /// </summary>
    public T Value { get; }

    /// <summary>
    ///     Returns a string representation of the facet, so that it is usable in API requests
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Type switch
        {
            FacetType.Categories => $"categories:{Value}",
            FacetType.Versions => $"versions:{Value}",
            FacetType.License => $"license:{Value}",
            FacetType.ProjectType => $"project_type:{Value}",
            _ => string.Empty
        };
    }
}

/// <summary>
///     The type of a facet
/// </summary>
public enum FacetType
{
    /// <summary>
    ///     The facet is for filtering by category
    /// </summary>
    Categories,

    /// <summary>
    ///     The facet is for filtering by Minecraft version
    /// </summary>
    Versions,

    /// <summary>
    ///     The facet is for filtering by license
    /// </summary>
    License,

    /// <summary>
    ///     The facet is for filtering by project type
    /// </summary>
    ProjectType
}