using Modrinth.Extensions;
using Modrinth.Models.Enums;

namespace Modrinth.Models.Facets;

public abstract class Facet
{
    public static Facet<string> Category(string value) => new (FacetType.Categories, value);
    
    public static Facet<string> Version(string value) => new(FacetType.Versions, value);
    
    public static Facet<string> License(string value) => new(FacetType.License, value);
    
    public static Facet<string> ProjectType(ProjectType projectType) => new(FacetType.ProjectType, projectType.ToModrinthString());
}

public class Facet<T> : Facet
{
    public FacetType Type { get; }
    public T Value { get; }
    
    public Facet(FacetType type, T value)
    {
        Type = type;
        Value = value;
    }

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