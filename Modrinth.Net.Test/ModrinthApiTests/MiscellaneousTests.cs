namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class MiscellaneousTests : EndpointTests
{
    [Test]
    public async Task GetStatisticsAsync()
    {
        var statistics = await _client.Miscellaneous.GetStatisticsAsync();
        Assert.That(statistics, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(statistics.Projects, Is.GreaterThan(0));
            Assert.That(statistics.Versions, Is.GreaterThan(0));
            Assert.That(statistics.Files, Is.GreaterThan(0));
            Assert.That(statistics.Authors, Is.GreaterThan(0));
        });
    }
}