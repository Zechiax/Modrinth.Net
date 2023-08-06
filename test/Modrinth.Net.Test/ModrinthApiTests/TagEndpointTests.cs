namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class TagEndpointTests : EndpointTests
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
    public async Task GetLicenseById_ShouldReturnLicense()
    {
        var licenseTag = await Client.Tag.GetLicenseAsync("MIT");
        
        Assert.Multiple(() =>
        {
            Assert.That(licenseTag, Is.Not.Null);
            Assert.That(licenseTag.Title, Is.EqualTo("MIT License"));
            Assert.That(licenseTag.Body, Is.Not.Null);
            Assert.That(licenseTag.Id, Is.EqualTo("MIT"));
        });
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
    
    [Test]
    public async Task GetProjectTypes_ShouldReturnProjectTypes()
    {
        var projectTypes = await Client.Tag.GetProjectTypesAsync();
        Assert.That(projectTypes, Is.Not.Null);
        Assert.That(projectTypes, Is.Not.Empty);
    }
    
    [Test]
    public async Task GetSideTypes_ShouldReturnSideTypes()
    {
        var sideTypes = await Client.Tag.GetSideTypesAsync();
        Assert.That(sideTypes, Is.Not.Null);
        Assert.That(sideTypes, Is.Not.Empty);
    }
}