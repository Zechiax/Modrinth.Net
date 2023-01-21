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
    public async Task TestSearch()
    {
        var search = await _api.Project.SearchProjectsAsync("fabric");
        
        Assert.That(search.TotalHits, Is.GreaterThan(0));
    }
    
    [Test]
    public async Task TestGetProject()
    {
        var project = await _api.Project.GetProjectAsync("gravestones");

        Assert.That(project.Title, Is.EqualTo("Gravestones"));
    }
    
    [Test]
    public async Task TestCheckIdSlugValidity()
    {
        var validity = await _api.Project.CheckProjectIdSlugValidityAsync("gravestones");

        Assert.That(validity.Id, Is.Not.Empty);
    }
}