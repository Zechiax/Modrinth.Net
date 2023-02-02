namespace Modrinth.Models.Facets;

public class Facet
{
    public FacetType Type { get; }
    public string Value { get; }
    
    private Facet(FacetType type, string value)
    {
        Type = type;
        Value = value;
    }
    
    public static Facet Category(string value) => new(FacetType.Categories, value);
    
    public static Facet Version(string value) => new(FacetType.Versions, value);
    
    public static Facet License(string value) => new(FacetType.License, value);
    
    public static Facet ProjectType(string value) => new(FacetType.ProjectType, value);

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