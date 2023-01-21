namespace Modrinth.RestClient.Test;

[TestFixture]
public class TestProjectEndpoint
{
    private ModrinthApi _client = null!;
    
    [SetUp]
    public void Setup()
    {
        _client = ModrinthApi.GetInstance();
    }

    [Test]
    public async Task TestEmptySearch()
    {
        var search = await _client.Project.SearchProjectsAsync("");
        
        Assert.That(search.TotalHits, Is.GreaterThan(0));
    }
    
    [Test]
    public async Task TestSearch()
    {
        var search = await _client.Project.SearchProjectsAsync("fabric");
        
        Assert.That(search.TotalHits, Is.GreaterThan(0));
    }
    
    [Test]
    public async Task TestGetProject()
    {
        var project = await _client.Project.GetAsync("gravestones");

        Assert.That(project.Title, Is.EqualTo("Gravestones"));
    }
    
    [Test]
    public async Task TestCheckIdSlugValidity()
    {
        var validity = await _client.Project.CheckIdSlugValidityAsync("gravestones");

        Assert.That(validity.Id, Is.Not.Empty);
    }

    [Test]
    public async Task TestFollowUnfollowProject()
    {
        // Will throw exception if not authorized / some other error
        await _client.Project.FollowAsync("gravestones");
        await _client.Project.UnfollowAsync("gravestones");
    }

    [Test]
    public async Task TestGetMultipleProjects()
    {
        var search = await _client.Project.SearchProjectsAsync("");
        var ids = search.Hits.Select(p => p.ProjectId).Take(5).ToList();
        var projects = await _client.Project.GetMultipleAsync(ids);
        
        Assert.That(projects, Is.Not.Null);
        // Check that all requested projects ids are present in the response
        Assert.That(projects.Select(p => p.Id).All(ids.Contains), Is.True);
    }
}