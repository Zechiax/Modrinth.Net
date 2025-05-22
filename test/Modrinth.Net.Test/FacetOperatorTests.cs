using Modrinth.Models.Enums.Project;

namespace Modrinth.Net.Test;

[TestFixture]
public class FacetOperatorTests
{
    [Test]
    public void Facet_WithEqualsOperator_ShouldFormatCorrectly()
    {
        var facet = Facet.Category("test", FacetOperator.Equals);
        Assert.That(facet.ToString(), Is.EqualTo("categories:test"));
    }

    [Test]
    public void Facet_WithNotEqualsOperator_ShouldFormatCorrectly()
    {
        var facet = Facet.Category("test", FacetOperator.NotEquals);
        Assert.That(facet.ToString(), Is.EqualTo("categories!=test"));
    }

    [Test]
    public void Facet_WithGreaterThanOperator_ShouldFormatCorrectly()
    {
        var facet = Facet.Version("1.19", FacetOperator.GreaterThan);
        Assert.That(facet.ToString(), Is.EqualTo("versions>1.19"));
    }

    [Test]
    public void Facet_WithGreaterThanOrEqualOperator_ShouldFormatCorrectly()
    {
        var facet = Facet.Version("1.19", FacetOperator.GreaterThanOrEqual);
        Assert.That(facet.ToString(), Is.EqualTo("versions>=1.19"));
    }

    [Test]
    public void Facet_WithLessThanOperator_ShouldFormatCorrectly()
    {
        var facet = Facet.Version("1.19", FacetOperator.LessThan);
        Assert.That(facet.ToString(), Is.EqualTo("versions<1.19"));
    }

    [Test]
    public void Facet_WithLessThanOrEqualOperator_ShouldFormatCorrectly()
    {
        var facet = Facet.Version("1.19", FacetOperator.LessThanOrEqual);
        Assert.That(facet.ToString(), Is.EqualTo("versions<=1.19"));
    }

    [Test]
    public void FacetCollection_WithDifferentOperators_ShouldFormatCorrectly()
    {
        var collection = new FacetCollection
        {
            Facet.Version("1.19", FacetOperator.GreaterThanOrEqual),
            Facet.Category("adventure")
        };
        Assert.That(collection.ToString(), Is.EqualTo("[[\"versions>=1.19\"],[\"categories=adventure\"]]"));
    }

    [Test]
    public void FacetCollection_WithMultipleOperatorsInSameGroup_ShouldFormatCorrectly()
    {
        var collection = new FacetCollection
        {
            { Facet.Version("1.18", FacetOperator.GreaterThanOrEqual), Facet.Version("1.20", FacetOperator.LessThan) }
        };
        Assert.That(collection.ToString(), Is.EqualTo("[[\"versions>=1.18\",\"versions<1.20\"]]"));
    }
}