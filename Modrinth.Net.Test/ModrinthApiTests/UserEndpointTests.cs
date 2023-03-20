using Modrinth.Exceptions;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class UserEndpointTests : EndpointTests
{
    [Test]
    public async Task TestGetCurrentUser()
    {
        var user = await Client.User.GetCurrentAsync();

        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public async Task TestGetNotifications()
    {
        var current = await Client.User.GetCurrentAsync();

        var notifications = await Client.User.GetNotificationsAsync(current.Id);

        Assert.That(notifications, Is.Not.Null);
    }

    [Test]
    public async Task TestGetFollowedProjects()
    {
        var current = await Client.User.GetCurrentAsync();
        var projects = await Client.User.GetFollowedProjectsAsync(current.Id);

        Assert.That(projects, Is.Not.Null);
    }

    [Test]
    public async Task GetUserAsync_WithValidId_ShouldReturnUser()
    {
        var currentUser = await Client.User.GetCurrentAsync();

        var user = await Client.User.GetAsync(currentUser.Id);

        Assert.That(user, Is.Not.Null);
        Assert.That(user.Id, Is.EqualTo(currentUser.Id));
    }

    // Test for current user with unauthenticated client
    [Test]
    public void TestGetCurrentUser_Unauthenticated()
    {
        Assert.ThrowsAsync<ModrinthApiException>(async () => await NoAuthClient.User.GetCurrentAsync());
    }

    [Test]
    public async Task ChangeIcon_WithValidId_ShouldSucceed()
    {
        if (!Icon.Exists) Assert.Inconclusive("Icon file not found, path: " + Icon.FullName);

        var current = await Client.User.GetCurrentAsync();
        await Client.User.ChangeIconAsync(current.Id, Icon.FullName);
    }
}