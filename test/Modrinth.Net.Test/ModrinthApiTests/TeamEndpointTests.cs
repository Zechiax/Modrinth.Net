namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class TeamEndpointTests : UnauthenticatedTestBase
{
    [Test]
    public async Task GetProjectTeamAsync_WithValidSlugOrId_ShouldReturnProjectTeam()
    {
        var team = await NoAuthClient.Team.GetProjectTeamAsync(TestData.TestProjectSlug);

        Assert.That(team, Is.Not.Null);
        Assert.That(team, Is.Not.Empty);
    }

    [Test]
    public async Task GetTeamMembersAsync_WithValidSlugOrId_ShouldReturnTeamMembers()
    {
        var project = await NoAuthClient.Project.GetAsync(TestData.TestProjectSlug);

        var members = await NoAuthClient.Team.GetAsync(project.Team);

        Assert.That(members, Is.Not.Null);
        Assert.That(members, Is.Not.Empty);
    }

    [Test]
    public async Task GetMultipleTeamsAsync_WithValidSlugOrId_ShouldReturnMultipleTeams()
    {
        var search = await NoAuthClient.Project.SearchAsync("");
        var projectIds = search.Hits.Select(p => p.ProjectId).Take(5);
        var projects = await NoAuthClient.Project.GetMultipleAsync(projectIds);

        var teamIds = projects.Select(p => p.Team).ToArray();

        var teams = await NoAuthClient.Team.GetMultipleAsync(teamIds);

        Assert.That(teams, Is.Not.Null);
        Assert.That(teams, Is.Not.Empty);
    }
}