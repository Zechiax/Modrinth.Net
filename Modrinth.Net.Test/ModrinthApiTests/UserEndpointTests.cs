using Modrinth.Exceptions;

namespace Modrinth.Net.Test.ModrinthApiTests;

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

    // Test for current user with unauthenticated client
    [Test]
    public void TestGetCurrentUser_Unauthenticated()
    {
        Assert.ThrowsAsync<ModrinthApiException>(async () => await _noAuthClient.User.GetCurrentAsync());
    }

    [Test]
    public async Task ChangeIcon_WithValidId_ShouldSucceed()
    {
        if (!Icon.Exists) Assert.Inconclusive("Icon file not found, path: " + Icon.FullName);

        var current = await _client.User.GetCurrentAsync();
        await _client.User.ChangeIconAsync(current.Id, Icon.FullName);
    }
}