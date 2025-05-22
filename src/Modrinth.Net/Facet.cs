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
    /// <param name="facetOperator"> The operator to use for filtering (defaults to equals) </param>
    /// <returns> The created facet </returns>
    public static Facet<string> Category(string value, FacetOperator facetOperator = FacetOperator.Equals)
    {
        return new Facet<string>(FacetType.Categories, value, facetOperator);
    }

    /// <summary>
    ///     Creates a new facet for the filtering by Minecraft version
    /// </summary>
    /// <param name="value"> The minecraft version to filter the results from </param>
    /// <param name="facetOperator"> The operator to use for filtering (defaults to equals) </param>
    /// <returns> The created facet </returns>
    public static Facet<string> Version(string value, FacetOperator facetOperator = FacetOperator.Equals)
    {
        return new Facet<string>(FacetType.Versions, value, facetOperator);
    }

    /// <summary>
    ///     Creates a new facet for the filtering by license
    /// </summary>
    /// <param name="value"> The license ID to filter the results from </param>
    /// <param name="facetOperator"> The operator to use for filtering (defaults to equals) </param>
    /// <returns> The created facet </returns>
    public static Facet<string> License(string value, FacetOperator facetOperator = FacetOperator.Equals)
    {
        return new Facet<string>(FacetType.License, value, facetOperator);
    }

    /// <summary>
    ///     Creates a new facet for the filtering by project type
    /// </summary>
    /// <param name="projectType"> The project type to filter the results from </param>
    /// <param name="facetOperator"> The operator to use for filtering (defaults to equals) </param>
    /// <returns> The created facet </returns>
    public static Facet<string> ProjectType(ProjectType projectType, FacetOperator facetOperator = FacetOperator.Equals)
    {
        return new Facet<string>(FacetType.ProjectType, projectType.ToModrinthString(), facetOperator);
    }
}

/// <summary>
///     A facet for the filtering of results, with a specific type
/// </summary>
/// <typeparam name="T"></typeparam>
public class Facet<T> : Facet
{
    internal Facet(FacetType type, T value, FacetOperator facetOperator = FacetOperator.Equals)
    {
        Type = type;
        Value = value;
        Operator = facetOperator;
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
    ///     The operator to use for filtering
    /// </summary>
    public FacetOperator Operator { get; }

    /// <summary>
    ///     Returns a string representation of the facet, so that it is usable in API requests
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var facetKey = Type switch
        {
            FacetType.Categories => "categories",
            FacetType.Versions => "versions",
            FacetType.License => "license",
            FacetType.ProjectType => "project_type",
            _ => string.Empty
        };

        var operatorStr = Operator switch
        {
            FacetOperator.Equals => ":",
            FacetOperator.NotEquals => "!=",
            FacetOperator.GreaterThan => ">",
            FacetOperator.GreaterThanOrEqual => ">=",
            FacetOperator.LessThan => "<",
            FacetOperator.LessThanOrEqual => "<=",
            _ => "="
        };

        return $"{facetKey}{operatorStr}{Value}";
    }
}

/// <summary>
///     The type of facet
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

/// <summary>
///     The operator to use for filtering
/// </summary>
public enum FacetOperator
{
    /// <summary>
    ///     Equals (=)
    /// </summary>
    Equals,

    /// <summary>
    ///     Not equals (!=)
    /// </summary>
    NotEquals,

    /// <summary>
    ///     Greater than (>)
    /// </summary>
    GreaterThan,

    /// <summary>
    ///     Greater than or equal (>=)
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    ///     Less than (&lt;)
    /// </summary>
    LessThan,

    /// <summary>
    ///     Less than or equal (&lt;=)
    /// </summary>
    LessThanOrEqual
}

