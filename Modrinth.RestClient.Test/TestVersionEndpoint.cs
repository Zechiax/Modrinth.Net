namespace Modrinth.RestClient.Test;

[TestFixture]
public class TestVersionEndpoint
{
    private ModrinthApi _client = null!;
    
    [SetUp]
    public void Setup()
    {
        _client = ModrinthApi.GetInstance();
    }
    
    [Test]
    public async Task TestGetVersions()
    {
        var project = await _client.Project.GetAsync("gravestones");
        var version = await _client.Version.GetAsync(project.Versions[0]);
        Assert.That(version, Is.Not.Null);
        Assert.That(version.ProjectId, Is.EqualTo(project.Id));
    }
    
    [Test]
    public async Task TestGetProjectVersionList()
    {
        var versions = await _client.Version.GetProjectVersionListAsync("gravestones");
        Assert.That(versions, Is.Not.Null);
        Assert.That(versions, Is.Not.Empty);
    }

    [Test]
    public async Task TestGetMultipleVersions()
    {
        // Not really a 'unit' test, but we need to find a project with more than one version
        var search = await _client.Project.SearchProjectsAsync("");
        
        // We will find the first project that has more than one version
        var projectWithMoreVersions = search.Hits.FirstOrDefault(p => p.DateCreated != p.DateModified);

        if (projectWithMoreVersions == null)
        {
            Assert.Fail("No project with more than one version found");
            return;
        }

        var project = await _client.Project.GetAsync(projectWithMoreVersions.ProjectId);
        
        if (project.Versions.Length < 2)
        {
            Assert.Fail("Project with more than one version has less than two versions");
            return;
        }
        
        var versions = await _client.Version.GetMultipleAsync(project.Versions);
        
        Assert.That(versions, Is.Not.Null);
        Assert.That(versions, Is.Not.Empty);
    }
}