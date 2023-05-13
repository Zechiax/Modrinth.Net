using Modrinth.Exceptions;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class ProjectEndpointTests : EndpointTests
{
    [Test]
    public async Task GetProject_WithValidId_ShouldReturnProject()
    {
        var project = await Client.Project.GetAsync(TestProjectSlug);

        Assert.That(project.Slug, Is.EqualTo(TestProjectSlug));
    }

    [Test]
    public async Task CheckIdSlugValidity_WithValidId_ShouldReturnId()
    {
        var validity = await Client.Project.CheckIdSlugValidityAsync(TestProjectSlug);

        Assert.That(validity, Is.Not.Empty);

        var project = await Client.Project.GetAsync(validity);

        Assert.That(project.Slug, Is.EqualTo(TestProjectSlug));
    }

    [Test]
    public async Task FollowAndUnfollow_WithValidId_ShouldSuccessfullyFollowAndUnfollow()
    {
        // Will throw exception if not authorized / some other error
        await Client.Project.FollowAsync(TestProjectSlug);
        await Client.Project.UnfollowAsync(TestProjectSlug);
    }

    [Test]
    public async Task GetMultiple_WithValidIdList_ShouldReturnAllRequestedProjects()
    {
        var search = await Client.Project.SearchAsync("");
        var ids = search.Hits.Select(p => p.ProjectId).Take(5).ToList();
        var projects = await Client.Project.GetMultipleAsync(ids);

        Assert.That(projects, Is.Not.Null);
        // Check that all requested projects ids are present in the response
        Assert.That(projects.Select(p => p.Id).All(ids.Contains), Is.True);
    }

    // Multiple ids but with only 1 id
    [Test]
    public async Task GetMultiple_WithSingleId_ShouldReturnRequestedProject()
    {
        var search = await Client.Project.SearchAsync("");
        var ids = search.Hits.Select(p => p.ProjectId).Take(1).ToList();
        var projects = await Client.Project.GetMultipleAsync(ids);

        Assert.That(projects, Is.Not.Null);
        // Check that all requested projects ids are present in the response
        Assert.That(projects.Select(p => p.Id).All(ids.Contains), Is.True);
    }

    // Multiple ids but with empty list
    [Test]
    public async Task GetMultiple_WithNoId_ShouldSuccessfullyReturn()
    {
        var ids = new List<string>();
        var projects = await Client.Project.GetMultipleAsync(ids);

        Assert.That(projects, Is.Not.Null);
        // Check that all requested projects ids are present in the response
        Assert.That(projects.Select(p => p.Id).All(ids.Contains), Is.True);
    }

    // Get dependencies
    [Test]
    public async Task GetDependencies_WithValidId_ShouldReturnDependencies()
    {
        var dependencies = await Client.Project.GetDependenciesAsync(TestProjectSlug);

        // Can be empty
        Assert.That(dependencies, Is.Not.Null);
    }

    // Random projects
    [Test]
    public async Task GetRandomProjects_WithValidCount_ShouldReturnProjects()
    {
        // BUG: Currently Modrinth less than the count of projects requested; relevant issue https://github.com/modrinth/labrinth/issues/548
        // TODO: Change count to default value when issue is fixed, currently making it 70 to have a higher chance of getting more than 10 projects
        var projects = await Client.Project.GetRandomAsync(70);

        Assert.That(projects, Is.Not.Null);
        Assert.That(projects, Is.Not.Empty);
    }

    [Test]
    public async Task ChangeIcon()
    {
        if (!Icon.Exists) Assert.Inconclusive("Icon file not found, path: " + Icon.FullName);

        await Client.Project.ChangeIconAsync(ModrinthNetTestProjectId, Icon.FullName);
    }

    [Test]
    public async Task DeleteIcon()
    {
        await Client.Project.DeleteIconAsync(ModrinthNetTestProjectId);
    }

    // Invalid id
    [Test]
    public void GetProject_WithInvalidId_ShouldThrowException()
    {
        Assert.ThrowsAsync<ModrinthApiException>(async () => await Client.Project.GetAsync("invalid-slug"));
    }
}