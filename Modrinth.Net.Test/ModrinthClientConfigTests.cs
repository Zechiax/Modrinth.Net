namespace Modrinth.Net.Test;

[TestFixture]
public class ModrinthClientConfigTests
{
    [Test]
    public void DefaultUserAgentIsPresent()
    {
        var config = new ModrinthClientConfig();

        Assert.That(config.UserAgent, Does.StartWith("Modrinth.Net"));
    }

    [Test]
    public void UserAgentCanBeSet()
    {
        var config = new ModrinthClientConfig
        {
            UserAgent = "Test"
        };

        Assert.That(config.UserAgent, Is.EqualTo("Test"));
    }

    [Test]
    public void ConfigCorrectlySetsUserAgent()
    {
        using var httpClient = new HttpClient();

        var config = new ModrinthClientConfig
        {
            UserAgent = "Test"
        };

        using var client = new ModrinthClient(config, httpClient);

        Assert.That(httpClient.DefaultRequestHeaders.UserAgent.ToString(), Is.EqualTo("Test"));
    }

    [Test]
    public void ConfigCorrectlySetsBaseAddress()
    {
        using var httpClient = new HttpClient();

        var config = new ModrinthClientConfig
        {
            BaseUrl = "https://example.com"
        };

        using var client = new ModrinthClient(config, httpClient);

        Assert.That(httpClient.BaseAddress, Is.EqualTo(new Uri("https://example.com")));
    }
}