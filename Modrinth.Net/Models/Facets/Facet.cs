using Modrinth.Extensions;
using Modrinth.Models.Enums;

namespace Modrinth.Models.Facets;

public abstract class Facet
{
    /// <summary>
    ///    Creates a new facet for the filtering by category or loader
    /// </summary>
    /// <param name="value"> The loader or category to filter the results from </param>
    /// <returns> The created facet </returns>
    public static Facet<string> Category(string value)
    {
        return new Facet<string>(FacetType.Categories, value);
    }

    /// <summary>
    ///  Creates a new facet for the filtering by Minecraft version
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
    ///    Creates a new facet for the filtering by project type
    /// </summary>
    /// <param name="projectType">  The project type to filter the results from </param>
    /// <returns> The created facet </returns>
    public static Facet<string> ProjectType(ProjectType projectType)
    {
        return new Facet<string>(FacetType.ProjectType, projectType.ToModrinthString());
    }
}

public class Facet<T> : Facet
{
    public Facet(FacetType type, T value)
    {
        Type = type;
        Value = value;
    }

    public FacetType Type { get; }
    public T Value { get; }

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

public enum FacetType
{
    Categories,
    Versions,
    License,
    ProjectType
}