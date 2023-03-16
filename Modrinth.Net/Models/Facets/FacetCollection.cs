using System.Collections;

namespace Modrinth.Models.Facets;

/// <summary>
///     A collection of facets
/// </summary>
public class FacetCollection : ICollection<Facet[]>
{
    private readonly List<Facet[]> _facets = new();

    /// <inheritdoc />
    public bool Remove(Facet[] item)
    {
        return _facets.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _facets.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

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

    /// <inheritdoc />
    public void Clear()
    {
        _facets.Clear();
    }

    /// <inheritdoc />
    public bool Contains(Facet[] item)
    {
        return _facets.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(Facet[][] array, int arrayIndex)
    {
        _facets.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public IEnumerator<Facet[]> GetEnumerator()
    {
        return _facets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    ///     Serializes the collection into a Javascript array
    /// </summary>
    /// <returns> A Javascript array string </returns>
    public override string ToString()
    {
        // Serialize the facets into a Javascript array
        var serializedFacets = _facets.Select(
            facets =>
                $"[{string.Join(',', facets.Select(facet => $"\"{facet}\""))}]");

        return $"[{string.Join(',', serializedFacets)}]";
    }
}