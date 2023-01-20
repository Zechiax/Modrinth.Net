using Flurl.Http.Testing;

namespace Modrinth.RestClient.Test;

public class TestClientConfiguration
{
    private HttpTest _httpTest;
    private ModrinthApi _client;
    
    [SetUp]
    public void Setup()
    {
        _httpTest = new();
        _client = ModrinthApi.GetInstance(ModrinthApi.StagingBaseUrl, userAgent: "Test-Agent", authorization: "Test-Token");
    }

    [Test]
    public async Task TestCall()
    {
        await _client.Tag.GetCategoriesAsync();
        
        _httpTest.ShouldHaveMadeACall()
            .WithHeader("User-Agent", "Test-Agent")
            .WithHeader("Authorization", "Test-Token");
    }

    [TearDown]
    public void TearDown()
    {
        _httpTest.Dispose();
    }
}