using Modrinth.Models.Enums.Project;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class SearchWithOperatorsTests : EndpointTests
{
    [Test]
    public async Task Search_WithNotEqualsOperator_ShouldFilterCorrectly()
    {
        var facets = new FacetCollection { Facet.Category("adventure", FacetOperator.NotEquals) };

        var search = await NoAuthClient.Project.SearchAsync("", facets: facets, limit: 20);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(search.TotalHits, Is.GreaterThan(0));
            Assert.That(search.Hits, Is.Not.Empty);
        }

        // Verify that none of the results have the "adventure" category
        foreach (var hit in search.Hits)
        {
            Assert.That(hit.Categories, Does.Not.Contain("adventure"));
        }
    }
}
