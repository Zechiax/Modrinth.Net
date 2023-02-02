namespace Modrinth.Models.Facets;

public class FacetCollection
{
    private readonly List<Facet[]> _facets = new();
    public int Count => _facets.Count;

    /// <summary>
    ///     Adds a facets to the collection, it will be added as a new group
    ///     Facets in a group are OR'd together
    ///     Facets in different groups are AND'd together
    /// </summary>
    /// <param name="facets"></param>
    public void Add(params Facet[] facets)
    {
        if (facets.Length == 0)
            return;
        _facets.Add(facets);
    }

    public override string ToString()
    {
        // Serialize the facets into a Javascript array
        var serializedFacets = _facets.Select(
            facets =>
                $"[{string.Join(',', facets.Select(facet => $"\"{facet}\""))}]");

        return $"[{string.Join(',', serializedFacets)}]";
    }
}