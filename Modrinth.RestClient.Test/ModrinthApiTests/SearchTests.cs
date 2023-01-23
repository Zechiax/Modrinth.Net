namespace Modrinth.RestClient.Test.ModrinthApiTests;

public class SearchTests : EndpointTests
{
    [Test]
    public async Task Search_WithEmptySearchTerm_ShouldReturnNonEmptyList()
    {
        var search = await _client.Project.SearchAsync("");
        Assert.Multiple(() =>
        {
            Assert.That(search.TotalHits, Is.GreaterThan(0));
            Assert.That(search.Hits, Is.Not.Null);
            Assert.That(search.Hits, Is.Not.Empty);
        });
    }

    [Test]
    public async Task Search_WithFabricSearchTerm_ShouldReturnNonEmptyList()
    {
        var search = await _client.Project.SearchAsync("fabric");
        
        Assert.Multiple(() =>
        {
            Assert.That(search.TotalHits, Is.GreaterThan(0));
            Assert.That(search.Hits, Is.Not.Null);
            Assert.That(search.Hits, Is.Not.Empty);
        });
    }
    
    // Test different limit values
    [Test]
    [TestCase((ulong) 0)]
    [TestCase((ulong) 1)]
    [TestCase((ulong) 5)]
    [TestCase((ulong) 10)]
    [TestCase((ulong) 20)]
    public async Task Search_WithLimit_ShouldReturnLimitedList(ulong limit)
    {
        var search = await _client.Project.SearchAsync("", limit: limit);
        
        Assert.Multiple(() =>
        {
            Assert.That(search.TotalHits, Is.GreaterThan(0));
            Assert.That(search.Hits, Is.Not.Null);
            Assert.That(search.Hits, Is.Not.Empty);
            Assert.That(search.Hits.Count, Is.EqualTo(limit));
        });
    }
}