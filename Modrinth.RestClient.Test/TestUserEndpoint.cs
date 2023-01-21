namespace Modrinth.RestClient.Test;

[TestFixture]
public class TestUserEndpoint
{
    private ModrinthApi _client = null!;
    
    [SetUp]
    public void Setup()
    {
        _client = ModrinthApi.GetInstance();
    }

    [Test]
    public async Task TestGetCurrentUser()
    {
        var user = await _client.User.GetCurrentUserAsync();
        
        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public async Task TestGetNotifications()
    {
        var current = await _client.User.GetCurrentUserAsync();
        
        var notifications = await _client.User.GetNotificationsAsync(current.Id);
        
        Assert.That(notifications, Is.Not.Null);
    }

    [Test]
    public async Task TestGetFollowedProjects()
    {
        var current = await _client.User.GetCurrentUserAsync();
        
        var projects = await _client.User.GetFollowedProjectsAsync(current.Id);
        
        Assert.That(projects, Is.Not.Null);
    }
}