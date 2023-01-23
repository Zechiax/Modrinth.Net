namespace Modrinth.RestClient.Test.ModrinthApiTests;

[TestFixture]
public class TestProjectEndpoint : EndpointTests
{
    [Test]
    public async Task GetProject_WithValidId_ShouldReturnProject()
    {
        var project = await _client.Project.GetAsync("gravestones");

        Assert.That(project.Title, Is.EqualTo("Gravestones"));
    }
    
    [Test]
    public async Task CheckIdSlugValidity_WithValidId_ShouldReturnId()
    {
        var validity = await _client.Project.CheckIdSlugValidityAsync("gravestones");

        Assert.That(validity.Id, Is.Not.Empty);
    }

    [Test]
    public async Task FollowAndUnfollow_WithValidId_ShouldSuccessfullyFollowAndUnfollow()
    {
        // Will throw exception if not authorized / some other error
        await _client.Project.FollowAsync("gravestones");
        await _client.Project.UnfollowAsync("gravestones");
    }

    [Test]
    public async Task GetMultiple_WithValidIdList_ShouldReturnAllRequestedProjects()
    {
        var search = await _client.Project.SearchAsync("");
        var ids = search.Hits.Select(p => p.ProjectId).Take(5).ToList();
        var projects = await _client.Project.GetMultipleAsync(ids);
        
        Assert.That(projects, Is.Not.Null);
        // Check that all requested projects ids are present in the response
        Assert.That(projects.Select(p => p.Id).All(ids.Contains), Is.True);
    }
    
    // Multiple ids but with only 1 id
    [Test]
    public async Task GetMultiple_WithSingleId_ShouldReturnRequestedProject()
    {
        var search = await _client.Project.SearchAsync("");
        var ids = search.Hits.Select(p => p.ProjectId).Take(1).ToList();
        var projects = await _client.Project.GetMultipleAsync(ids);
        
        Assert.That(projects, Is.Not.Null);
        // Check that all requested projects ids are present in the response
        Assert.That(projects.Select(p => p.Id).All(ids.Contains), Is.True);
    }
    
    // Multiple ids but with empty list
    [Test]
    public async Task GetMultiple_WithNoId_ShouldSuccessfullyReturn()
    {
        var ids = new List<string>();
        var projects = await _client.Project.GetMultipleAsync(ids);
        
        Assert.That(projects, Is.Not.Null);
        // Check that all requested projects ids are present in the response
        Assert.That(projects.Select(p => p.Id).All(ids.Contains), Is.True);
    }
}