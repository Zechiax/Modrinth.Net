using Modrinth.RestClient.Models.Enums;

namespace Modrinth.RestClient.Test;

public class TestProjectEndpoint
{
    private ModrinthApi _api = null!;
    
    [SetUp]
    public void Setup()
    {
        _api = ModrinthApi.GetInstance(url: ModrinthApi.StagingBaseUrl, userAgent: "Modrinth.RestClient.Test");
    }

    [Test]
    public async Task TestEmptySearch()
    {
        var search = await _api.Project.SearchProjectsAsync("");
        
        Assert.That(search.TotalHits, Is.GreaterThan(0));
    }
    
    [Test]
    public async Task TestProjectSearch()
    {
        var project = await _api.Project.GetProjectAsync("gravestones");

        Assert.That(project.Title, Is.EqualTo("Gravestones"));
    }
}