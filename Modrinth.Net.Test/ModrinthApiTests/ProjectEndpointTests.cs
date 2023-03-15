namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class TestProjectEndpoint : EndpointTests
{
    [Test]
    public async Task GetProject_WithValidId_ShouldReturnProject()
    {
        var project = await _client.Project.GetAsync(TestProjectSlug);

        Assert.That(project.Slug, Is.EqualTo(TestProjectSlug));
    }

    [Test]
    public async Task CheckIdSlugValidity_WithValidId_ShouldReturnId()
    {
        var validity = await _client.Project.CheckIdSlugValidityAsync(TestProjectSlug);

        Assert.That(validity.Id, Is.Not.Empty);
    }

    [Test]
    public async Task FollowAndUnfollow_WithValidId_ShouldSuccessfullyFollowAndUnfollow()
    {
        // Will throw exception if not authorized / some other error
        await _client.Project.FollowAsync(TestProjectSlug);
        await _client.Project.UnfollowAsync(TestProjectSlug);
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

    // Get dependencies
    [Test]
    public async Task GetDependencies_WithValidId_ShouldReturnDependencies()
    {
        var dependencies = await _client.Project.GetDependenciesAsync(TestProjectSlug);

        // Can be empty
        Assert.That(dependencies, Is.Not.Null);
    }

    // Random projects
    [Test]
    public async Task GetRandomProjects_WithValidCount_ShouldReturnProjects()
    {
        var projects = await _client.Project.GetRandomAsync();

        Assert.That(projects, Is.Not.Null);
        Assert.That(projects, Is.Not.Empty);
    }

    [Test]
    public async Task ChangeIcon()
    {
        var icon = new FileInfo("../../../../Modrinth.Net.Test/Assets/Icons/ModrinthNet.png");

        if (!icon.Exists) Assert.Inconclusive("Icon file not found, path: " + icon.FullName);

        await _client.Project.ChangeIconAsync(ModrinthNetTestProjectId, icon.FullName);
    }

    [Test]
    public async Task DeleteIcon()
    {
        await _client.Project.DeleteIconAsync(ModrinthNetTestProjectId);
    }
}