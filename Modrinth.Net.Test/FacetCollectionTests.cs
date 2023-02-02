using Modrinth.Models.Facets;
using Newtonsoft.Json;

namespace Modrinth.Net.Test;

[TestFixture]
public class FacetCollectionTests
{
    [Test]
    public void CollectionWithOnlyOneFacet()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"));
        Assert.That(collection.ToString(), Is.EqualTo("[[\"category:test\"]]"));
    }
    
    [Test]
    public void CollectionWithMultipleFacets_OR()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"), Facet.Category("test2"));
        Assert.That(collection.ToString(), Is.EqualTo("[[\"category:test\",\"category:test2\"]]"));
    }
    
    [Test]
    public void CollectionWithMultipleFacets_AND()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"));
        collection.Add(Facet.Category("test2"));
        Assert.That(collection.ToString(), Is.EqualTo("[[\"category:test\"],[\"category:test2\"]]"));
    }
    
    [Test]
    public void CollectionWithMultipleFacets_AND_OR()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"));
        collection.Add(Facet.Category("test2"), Facet.Category("test3"));
        Assert.That(collection.ToString(), Is.EqualTo("[[\"category:test\"],[\"category:test2\",\"category:test3\"]]"));
    }
}