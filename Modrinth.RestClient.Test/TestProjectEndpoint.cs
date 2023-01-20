namespace Modrinth.RestClient.Test;

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
        var project = await _client.Project.GetProjectAsync("gravestones");

        Assert.That(project.Title, Is.EqualTo("Gravestones"));
    }
    
    [Test]
    public async Task TestCheckIdSlugValidity()
    {
        var validity = await _client.Project.CheckProjectIdSlugValidityAsync("gravestones");

        Assert.That(validity.Id, Is.Not.Empty);
    }

    [Test]
    public async Task TestFollowUnfollowProject()
    {
        // Will throw exception if not authorized / some other error
        await _client.Project.FollowProjectAsync("gravestones");
        await _client.Project.UnfollowProjectAsync("gravestones");
    }
}