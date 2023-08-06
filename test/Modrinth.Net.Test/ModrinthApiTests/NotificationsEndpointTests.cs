namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class NotificationsEndpointTests : EndpointTests
{
    [Test]
    public async Task TestGetNotifications()
    {
        var current = await Client.User.GetCurrentAsync();

        var notifications = await Client.Notification.GetNotificationsAsync(current.Id);

        Assert.That(notifications, Is.Not.Null);
    }
}