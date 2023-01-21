namespace Modrinth.RestClient.Test;

[TestFixture]
public class TestUserEndpoint : EndpointTests
{
    [Test]
    public async Task TestGetCurrentUser()
    {
        var user = await _client.User.GetCurrentAsync();
        
        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public async Task TestGetNotifications()
    {
        var current = await _client.User.GetCurrentAsync();
        
        var notifications = await _client.User.GetNotificationsAsync(current.Id);
        
        Assert.That(notifications, Is.Not.Null);
    }

    [Test]
    public async Task TestGetFollowedProjects()
    {
        var current = await _client.User.GetCurrentAsync();
        var projects = await _client.User.GetFollowedProjectsAsync(current.Id);
        
        Assert.That(projects, Is.Not.Null);
    }
    
    [Test]
    public async Task GetUserAsync_WithValidId_ShouldReturnUser()
    {
        var currentUser = await _client.User.GetCurrentAsync();
        
        var user = await _client.User.GetAsync(currentUser.Id);
        
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Id, Is.EqualTo(currentUser.Id));
    }
}