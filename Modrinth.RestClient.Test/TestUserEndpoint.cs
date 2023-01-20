namespace Modrinth.RestClient.Test;

public class TestUserEndpoint
{
    private ModrinthApi _client = null!;
    
    [SetUp]
    public void Setup()
    {
        var token = Environment.GetEnvironmentVariable("AUTH_TOKEN");
        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("No auth token provided");
        _client = ModrinthApi.GetInstance();
    }

    [Test]
    public async Task TestGetCurrentUser()
    {
        var user = await _client.User.GetCurrentUserAsync();
        
        Assert.That(user, Is.Not.Null);
    }
}