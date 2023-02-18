using Modrinth.Extensions;
using Modrinth.Models.Enums;

namespace Modrinth.Models.Facets;

public abstract class Facet
{
    public static Facet<string> Category(string value)
    {
        return new Facet<string>(FacetType.Categories, value);
    }

    public static Facet<string> Version(string value)
    {
        return new Facet<string>(FacetType.Versions, value);
    }

    public static Facet<string> License(string value)
    {
        return new Facet<string>(FacetType.License, value);
    }

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