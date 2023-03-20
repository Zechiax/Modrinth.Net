namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class TestTeamEndpoint : EndpointTests
{
    [Test]
    public async Task GetProjectTeamAsync_WithValidSlugOrId_ShouldReturnProjectTeam()
    {
        var team = await Client.Team.GetProjectTeamAsync(TestProjectSlug);

        Assert.That(team, Is.Not.Null);
        Assert.That(team, Is.Not.Empty);
    }

    [Test]
    public async Task GetTeamMembersAsync_WithValidSlugOrId_ShouldReturnTeamMembers()
    {
        var project = await Client.Project.GetAsync(TestProjectSlug);

        var members = await Client.Team.GetAsync(project.Team);

        Assert.That(members, Is.Not.Null);
        Assert.That(members, Is.Not.Empty);
    }

    [Test]
    public async Task GetMultipleTeamsAsync_WithValidSlugOrId_ShouldReturnMultipleTeams()
    {
        var search = await Client.Project.SearchAsync("");
        var projectIds = search.Hits.Select(p => p.ProjectId).Take(5);
        var projects = await Client.Project.GetMultipleAsync(projectIds);

        var teamIds = projects.Select(p => p.Team).ToArray();

        var teams = await Client.Team.GetMultipleAsync(teamIds);

        Assert.That(teams, Is.Not.Null);
        Assert.That(teams, Is.Not.Empty);
    }
}