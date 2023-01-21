namespace Modrinth.RestClient.Test;

[TestFixture]
public class TestTeamEndpoint : EndpointTests
{
    [Test]
    public async Task GetProjectTeamAsync_WithValidSlugOrId_ShouldReturnProjectTeam()
    {
        var team = await _client.Team.GetProjectTeamAsync("gravestones");
        
        Assert.That(team, Is.Not.Null);
        Assert.That(team, Is.Not.Empty);
    }
    
    [Test]
    public async Task GetTeamMembersAsync_WithValidSlugOrId_ShouldReturnTeamMembers()
    {
        var project = await _client.Project.GetAsync("gravestones");
        
        var members = await _client.Team.GetAsync(project.Team);
        
        Assert.That(members, Is.Not.Null);
        Assert.That(members, Is.Not.Empty);
    }
    
    [Test]
    public async Task GetMultipleTeamsAsync_WithValidSlugOrId_ShouldReturnMultipleTeams()
    {
        var search = await _client.Project.SearchAsync("");
        var projectIds = search.Hits.Select(p => p.ProjectId).Take(5);
        var projects = await _client.Project.GetMultipleAsync(projectIds);
        
        var teamIds = projects.Select(p => p.Team).ToArray();
        
        var teams = await _client.Team.GetMultipleAsync(teamIds);

        Assert.That(teams, Is.Not.Null);
        Assert.That(teams, Is.Not.Empty);
    }
}