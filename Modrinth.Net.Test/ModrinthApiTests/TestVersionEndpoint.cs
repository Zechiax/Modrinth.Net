namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class TestVersionEndpoint : EndpointTests
{
    [Test]
    public async Task TestGetVersions()
    {
        var project = await Client.Project.GetAsync(TestProjectSlug);
        var version = await Client.Version.GetAsync(project.Versions[0]);
        Assert.That(version, Is.Not.Null);
        Assert.That(version.ProjectId, Is.EqualTo(project.Id));
    }

    [Test]
    public async Task TestGetProjectVersionList()
    {
        var versions = await Client.Version.GetProjectVersionListAsync(TestProjectSlug);
        Assert.That(versions, Is.Not.Null);
        // BUG: Test versions should not be empty, but they are for now
        // Assert.That(versions, Is.Not.Empty);
    }

    [Test]
    public async Task TestGetMultipleVersions()
    {
        // Not really a 'unit' test, but we need to find a project with more than one version
        var search = await Client.Project.SearchAsync("");

        // We will find the first project that has more than one version
        var projectWithMoreVersions = search.Hits.FirstOrDefault(p => p.DateCreated != p.DateModified);

        if (projectWithMoreVersions == null)
        {
            Assert.Fail("No project with more than one version found");
            return;
        }

        var project = await Client.Project.GetAsync(projectWithMoreVersions.ProjectId);

        if (project.Versions.Length < 2)
        {
            Assert.Fail("Project with more than one version has less than two versions");
            return;
        }

        var versions = await Client.Version.GetMultipleAsync(project.Versions);

        Assert.That(versions, Is.Not.Null);
        Assert.That(versions, Is.Not.Empty);
    }

    [Test]
    public async Task TestGetVersionByNumber()
    {
        // Actually we test by version id, but it's the same as using the version number
        var project = await Client.Project.GetAsync(TestProjectSlug);
        var version = await Client.Version.GetByVersionNumberAsync(TestProjectSlug, project.Versions[0]);
        Assert.That(version, Is.Not.Null);
        Assert.That(version.ProjectId, Is.EqualTo(project.Id));
    }

    [Test]
    [TestCase(new[] {"fabric"}, new[] {"1.19.2"}, false)]
    //TODO: Somehow, the API endpoint returns versions with Featured = false, even if we request Featured = true
    //[TestCase(new[] {"fabric"}, new[] {"1.19.2"}, true)]
    public async Task TestGetProjectVersionListWithFilters(string[]? loaders = null, string[]? gameVersions = null,
        bool? featured = null)
    {
        var versions =
            await Client.Version.GetProjectVersionListAsync(TestProjectSlug, loaders, gameVersions, featured);
        Assert.That(versions, Is.Not.Null);
        Assert.That(versions, Is.Not.Empty);
        foreach (var version in versions)
            Assert.Multiple(() =>
            {
                if (loaders != null)
                    Assert.That(version.Loaders, Is.SupersetOf(loaders));
                if (gameVersions != null)
                    Assert.That(version.GameVersions, Is.SupersetOf(gameVersions));
                if (featured != null)
                    Assert.That(version.Featured, Is.EqualTo(featured));
            });
    }
}