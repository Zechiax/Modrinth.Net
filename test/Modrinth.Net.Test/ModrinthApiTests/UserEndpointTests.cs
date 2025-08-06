using Modrinth.Exceptions;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class UserEndpointTests : UnauthenticatedTestBase
{
    // Test for current user with unauthenticated client
    [Test]
    public void TestGetCurrentUser_Unauthenticated()
    {
        Assert.ThrowsAsync<ModrinthApiException>(async () => await NoAuthClient.User.GetCurrentAsync());
    }
    
    // [Test]
    // public async Task GetCurrentUserPayoutHistory()
    // {
    //     var curUser = await Client.User.GetCurrentAsync();
    //     var history = await Client.User.GetPayoutHistoryAsync(curUser.Id);
    //
    //     Assert.That(history, Is.Not.Null);
    // }
}

public class AuthenticatedUserEndpointTest : AuthenticatedTestBase
{
    [Test]
    public async Task TestGetCurrentUser()
    {
        var user = await Client.User.GetCurrentAsync();

        Assert.That(user, Is.Not.Null);
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

        var user = await NoAuthClient.User.GetAsync(currentUser.Id);

        Assert.That(user, Is.Not.Null);
        Assert.That(user.Id, Is.EqualTo(currentUser.Id));
    }
    
    [Test]
    public async Task ChangeIcon_WithValidId_ShouldSucceed()
    {
        if (!TestData.Icon.Exists) Assert.Inconclusive("Icon file not found, path: " + TestData.Icon.FullName);

        var current = await Client.User.GetCurrentAsync();
        await Client.User.ChangeIconAsync(current.Id, TestData.Icon.FullName);
    }

    [Test]
    public async Task GetUsersProjects()
    {
        var projects = await Client.User.GetProjectsAsync(TestData.TestUserIds[0]);

        Assert.That(projects, Is.Not.Null);
        Assert.That(projects, Is.Not.Empty);
    }

    [Test]
    public async Task GetMultipleUsers()
    {
        var users = await Client.User.GetMultipleAsync(TestData.TestUserIds);

        Assert.That(users, Is.Not.Null);
        Assert.That(users, Is.Not.Empty);

        foreach (var user in users)
        {
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Id, Is.Not.Null);
        }
    }
}