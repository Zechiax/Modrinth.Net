namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class TestTagEndpoint : EndpointTests
{
    [Test]
    public async Task GetCategories_ShouldReturnCategories()
    {
        var categories = await Client.Tag.GetCategoriesAsync();
        Assert.That(categories, Is.Not.Null);
        Assert.That(categories, Is.Not.Empty);
    }

    [Test]
    public async Task GetLoaders_ShouldReturnLoaders()
    {
        var loaders = await Client.Tag.GetLoadersAsync();
        Assert.That(loaders, Is.Not.Null);
        Assert.That(loaders, Is.Not.Empty);
    }

    [Test]
    public async Task GetGameVersions_ShouldReturnGameVersions()
    {
        var gameVersions = await Client.Tag.GetGameVersionsAsync();
        Assert.That(gameVersions, Is.Not.Null);
        Assert.That(gameVersions, Is.Not.Empty);
    }

    [Test]
    public async Task GetLicenses_ShouldReturnLicenses()
    {
        var licenses = await Client.Tag.GetLicensesAsync();
        Assert.That(licenses, Is.Not.Null);
        Assert.That(licenses, Is.Not.Empty);
    }

    [Test]
    public async Task GetDonationPlatforms_ShouldReturnDonationPlatforms()
    {
        var donationPlatforms = await Client.Tag.GetDonationPlatformsAsync();
        Assert.That(donationPlatforms, Is.Not.Null);
        Assert.That(donationPlatforms, Is.Not.Empty);
    }

    [Test]
    public async Task GetReportTypes_ShouldReturnReportTypes()
    {
        var reportTypes = await Client.Tag.GetReportTypesAsync();
        Assert.That(reportTypes, Is.Not.Null);
        Assert.That(reportTypes, Is.Not.Empty);
    }
}