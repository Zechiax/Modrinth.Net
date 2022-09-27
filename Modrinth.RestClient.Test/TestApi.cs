namespace Modrinth.RestClient.Test;

public class Tests
{
    private IModrinthApi _api = null!;
    
    [SetUp]
    public void Setup()
    {
        _api = ModrinthApi.NewClient(userAgent: "Zechiax/Modrinth.RestClient.Test");
    }

    [Test]
    public async Task TestEmptySearch()
    {
        var search = await _api.SearchProjectsAsync("");
        
        Assert.That(search.TotalHits, Is.GreaterThan(0));
    }

    [Test]
    public async Task TestMultipleSearchWithOneId()
    {
        var search = await _api.SearchProjectsAsync("");

        var projectId = search.Hits[0].ProjectId;
        
        var projects = await _api.GetMultipleProjectsAsync(new [] {projectId});
        
        Assert.That(projects, Is.Not.Empty);
    }
}