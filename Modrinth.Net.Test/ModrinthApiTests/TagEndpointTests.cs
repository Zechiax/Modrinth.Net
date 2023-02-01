namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class TestTagEndpoint : EndpointTests
{
    [Test]
    public async Task GetCategories_ShouldReturnCategories()
    {
        var categories = await _client.Tag.GetCategoriesAsync();
        Assert.That(categories, Is.Not.Null);
        Assert.That(categories, Is.Not.Empty);
    }

    [Test]
    public async Task GetLoaders_ShouldReturnLoaders()
    {
        var loaders = await _client.Tag.GetLoadersAsync();
        Assert.That(loaders, Is.Not.Null);
        Assert.That(loaders, Is.Not.Empty);
    }

    [Test]
    public async Task GetGameVersions_ShouldReturnGameVersions()
    {
        var gameVersions = await _client.Tag.GetGameVersionsAsync();
        Assert.That(gameVersions, Is.Not.Null);
        Assert.That(gameVersions, Is.Not.Empty);
    }

    [Test]
    public async Task GetLicenses_ShouldReturnLicenses()
    {
        var licenses = await _client.Tag.GetLicensesAsync();
        Assert.That(licenses, Is.Not.Null);
        Assert.That(licenses, Is.Not.Empty);
    }

    [Test]
    public async Task GetDonationPlatforms_ShouldReturnDonationPlatforms()
    {
        var donationPlatforms = await _client.Tag.GetDonationPlatformsAsync();
        Assert.That(donationPlatforms, Is.Not.Null);
        Assert.That(donationPlatforms, Is.Not.Empty);
    }

    [Test]
    public async Task GetReportTypes_ShouldReturnReportTypes()
    {
        var reportTypes = await _client.Tag.GetReportTypesAsync();
        Assert.That(reportTypes, Is.Not.Null);
        Assert.That(reportTypes, Is.Not.Empty);
    }
}