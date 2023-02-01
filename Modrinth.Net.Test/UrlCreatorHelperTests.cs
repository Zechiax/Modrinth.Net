using Modrinth.Extensions;
using Modrinth.Helpers;
using Modrinth.Models;
using Modrinth.Models.Enums;
using Version = Modrinth.Models.Version;

namespace Modrinth.Net.Test;

[TestFixture]
public class UrlCreatorHelperTests
{
    [Test]
    public void CreateDirectProjectUrl_WithValidId_ShouldReturnValidUrl()
    {
        var project = new Project
        {
            Id = "AABBCCDD",
            ProjectType = ProjectType.Mod
        };

        var url = project.GetDirectUrl();

        Assert.That(url, Is.Not.Null);
        Assert.That(url, Is.Not.Empty);
        Assert.That(url, Is.EqualTo($"https://modrinth.com/mod/{project.Id}"));
    }

    [Test]
    public void CreateDirectUserUrl_WithValidId_ShouldReturnValidUrl()
    {
        var user = new User
        {
            Id = "AABBCCDD"
        };

        var url = user.GetDirectUrl();

        Assert.That(url, Is.Not.Null);
        Assert.That(url, Is.Not.Empty);
        Assert.That(url, Is.EqualTo($"https://modrinth.com/user/{user.Id}"));
    }

    [Test]
    public void CreateDirectVersionUrl_WithValidId_ShouldReturnValidUrl()
    {
        var version = new Version
        {
            Id = "AABBCCDD"
        };

        var project = new Project
        {
            Id = "AABBCCDD",
            ProjectType = ProjectType.Mod
        };

        var url = version.GetUrl(project);

        Assert.That(url, Is.Not.Null);
        Assert.That(url, Is.Not.Empty);
        Assert.That(url, Is.EqualTo($"https://modrinth.com/mod/{project.Id}/version/{version.Id}"));
    }

    [Test]
    public void CreateDirectSearchResultUrl_WithValidId_ShouldReturnValidUrl()
    {
        var searchResult = new SearchResult
        {
            ProjectId = "AABBCCDD",
            ProjectType = ProjectType.Mod
        };

        var url = searchResult.GetDirectUrl();

        Assert.That(url, Is.Not.Null);
        Assert.That(url, Is.Not.Empty);
        Assert.That(url, Is.EqualTo($"https://modrinth.com/mod/{searchResult.ProjectId}"));
    }
}