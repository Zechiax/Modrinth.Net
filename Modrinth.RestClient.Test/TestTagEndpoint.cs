namespace Modrinth.RestClient.Test;

[TestFixture]
public class TestTagEndpoint
{
    private IModrinthApi _client = null!;
    
    [SetUp]
    public void Setup()
    {
        _client = ModrinthApi.GetInstance();
    }
    
    [Test]
    public async Task TestGetCategories()
    {
        var categories = await _client.Tag.GetCategoriesAsync();
        Assert.That(categories, Is.Not.Null);
        Assert.That(categories, Is.Not.Empty);
    }

    [Test]
    public async Task TestGetLoaders()
    {
        var loaders = await _client.Tag.GetLoadersAsync();
        Assert.That(loaders, Is.Not.Null);
        Assert.That(loaders, Is.Not.Empty);
    }
    
    [Test]
    public async Task TestGetGameVersions()
    {
        var gameVersions = await _client.Tag.GetGameVersionsAsync();
        Assert.That(gameVersions, Is.Not.Null);
        Assert.That(gameVersions, Is.Not.Empty);
    }
    
    [Test]
    public async Task TestGetLicense()
    {
        var licenses = await _client.Tag.GetLicensesAsync();
        Assert.That(licenses, Is.Not.Null);
        Assert.That(licenses, Is.Not.Empty);
    }
    
    [Test]
    public async Task TestGetDonationPlatforms()
    {
        var donationPlatforms = await _client.Tag.GetDonationPlatformsAsync();
        Assert.That(donationPlatforms, Is.Not.Null);
        Assert.That(donationPlatforms, Is.Not.Empty);
    }
    
    [Test]
    public async Task TestGetReportTypes()
    {
        var reportTypes = await _client.Tag.GetReportTypesAsync();
        Assert.That(reportTypes, Is.Not.Null);
        Assert.That(reportTypes, Is.Not.Empty);
    }
}