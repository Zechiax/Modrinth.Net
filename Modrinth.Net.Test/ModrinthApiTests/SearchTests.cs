using Index = Modrinth.Models.Enums.Index;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
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
            Assert.That(search.Hits.Count, Is.EqualTo(limit));
            Assert.That(search.Hits, Is.Not.Null);
            if (limit == 0)
            {
                Assert.That(search.Hits, Is.Empty);
                return;
            }

            Assert.That(search.Hits, Is.Not.Empty);
        });
    }

    // Test different offset values
    [Test]
    [TestCase((ulong) 0)]
    [TestCase((ulong) 5)]
    [TestCase((ulong) 10)]
    [TestCase((ulong) 15)]
    [TestCase((ulong) 20)]
    public async Task Search_WithOffset_ShouldReturnOffsetList(ulong offset)
    {
        var search = await _client.Project.SearchAsync("", limit: offset + 5);
        var searchWithOffset = await _client.Project.SearchAsync("", offset: offset, limit: offset + 5);

        // Check that the offset list is not the same as the original list
        Assert.That(searchWithOffset.Hits, Is.Not.EqualTo(search.Hits));

        // Check that the offset list is the same as the original list with the first 5 elements removed
        // Check the ids of the first 5 elements
        Assert.That(
            searchWithOffset.Hits.Select(p => p.ProjectId).Take(5),
            Is.EqualTo(search.Hits.Select(p => p.ProjectId).Skip((int) offset).Take(5)));
    }

    // Test download sorting
    [Test]
    public async Task Search_WithDownloadsSort_ShouldReturnSortedByDownloadsList()
    {
        var search = await _client.Project.SearchAsync("", Index.Downloads);

        // Check that the list is sorted by downloads
        Assert.That(
            search.Hits.Select(p => p.Downloads),
            Is.EqualTo(search.Hits.Select(p => p.Downloads).OrderByDescending(d => d)));
    }

    // Test followers sorting
    [Test]
    public async Task Search_WithFollowersSort_ShouldReturnSortedByFollowersList()
    {
        var search = await _client.Project.SearchAsync("", Index.Follows);

        // Check that the list is sorted by followers
        Assert.That(
            search.Hits.Select(p => p.Followers),
            Is.EqualTo(search.Hits.Select(p => p.Followers).OrderByDescending(d => d)));
    }

    // Test newest sorting
    [Test]
    public async Task Search_WithNewestSort_ShouldReturnSortedByNewestList()
    {
        var search = await _client.Project.SearchAsync("", Index.Newest);

        // Check that the list is sorted by newest
        Assert.That(
            search.Hits.Select(p => p.DateCreated),
            Is.EqualTo(search.Hits.Select(p => p.DateCreated).OrderByDescending(d => d)));
    }

    // Test updated sorting
    [Test]
    public async Task Search_WithUpdatedSort_ShouldReturnSortedByUpdatedList()
    {
        var search = await _client.Project.SearchAsync("", Index.Updated);

        // Check that the list is sorted by updated
        Assert.That(
            search.Hits.Select(p => p.DateModified),
            Is.EqualTo(search.Hits.Select(p => p.DateModified).OrderByDescending(d => d)));
    }
}