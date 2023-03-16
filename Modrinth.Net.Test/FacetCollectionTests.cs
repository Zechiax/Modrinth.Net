using Modrinth.Models.Facets;

namespace Modrinth.Net.Test;

[TestFixture]
public class FacetCollectionTests
{
    [Test]
    public void CollectionWithOnlyOneFacet()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"));
        Assert.That(collection.ToString(), Is.EqualTo("[[\"categories:test\"]]"));
    }

    [Test]
    public void CollectionWithMultipleFacets_OR()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"), Facet.Category("test2"));
        Assert.That(collection.ToString(), Is.EqualTo("[[\"categories:test\",\"categories:test2\"]]"));
    }

    [Test]
    public void CollectionWithMultipleFacets_AND()
    {
        var collection = new FacetCollection
        {
            Facet.Category("test"),
            Facet.Category("test2")
        };
        Assert.That(collection.ToString(), Is.EqualTo("[[\"categories:test\"],[\"categories:test2\"]]"));
    }

    [Test]
    public void CollectionWithMultipleFacets_AND_OR()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"));
        collection.Add(Facet.Category("test2"), Facet.Category("test3"));
        Assert.That(collection.ToString(),
            Is.EqualTo("[[\"categories:test\"],[\"categories:test2\",\"categories:test3\"]]"));
    }

    [Test]
    public void CollectionWithMultipleFacets_AND_OR_AND()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"));
        collection.Add(Facet.Category("test2"), Facet.Category("test3"));
        collection.Add(Facet.Category("test4"));
        Assert.That(collection.ToString(),
            Is.EqualTo("[[\"categories:test\"],[\"categories:test2\",\"categories:test3\"],[\"categories:test4\"]]"));
    }

    [Test]
    public void FacetCollection_Clear()
    {
        var collection = new FacetCollection();
        collection.Add(Facet.Category("test"));
        collection.Add(Facet.Category("test2"), Facet.Category("test3"));
        collection.Add(Facet.Category("test4"));
        collection.Clear();
        Assert.Multiple(() =>
        {
            Assert.That(collection.ToString(), Is.EqualTo("[]"));
            Assert.That(collection, Is.Empty);
        });
    }
}