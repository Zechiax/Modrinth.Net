namespace Modrinth.Net.Test;

[TestFixture]
public class UserAgentTests
{
    [Test]
    public void CreateFullUserAgent()
    {
        var userAgent = new UserAgent
        {
            ProjectName = "ProjectName",
            ProjectVersion = "1.0.0",
            GitHubUsername = "Username",
            Contact = "contact@contact.com"
        };

        Assert.That(userAgent.ToString(), Is.EqualTo("Username/ProjectName-1.0.0 (contact@contact.com)"));
    }

    [Test]
    public void CreateMinimalUserAgent()
    {
        var userAgent = new UserAgent
        {
            ProjectName = "ProjectName",
            ProjectVersion = "1.0.0"
        };

        Assert.That(userAgent.ToString(), Is.EqualTo("ProjectName-1.0.0"));
    }

    [Test]
    public void CreateUserAgentWithNoVersion()
    {
        var userAgent = new UserAgent
        {
            ProjectName = "ProjectName"
        };

        Assert.That(userAgent.ToString(), Is.EqualTo("ProjectName"));
    }
}