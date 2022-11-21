using Modrinth.RestClient.Models.Enums;

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
    
    [Test]
    public async Task GetMultipleProjects()
    {
        var search = await _api.SearchProjectsAsync("");

        var ids = search.Hits.Select(x => x.ProjectId);

        var projects = await _api.GetMultipleProjectsAsync(ids);
    }

    [Test]
    public async Task GetVersionByHashSha1()
    {
        var search = await _api.SearchProjectsAsync("");

        var first = search.Hits.First();

        var versionById = await _api.GetVersionByIdAsync((await _api.GetProjectAsync(first.ProjectId)).Versions.First());

        var hashesSha1 = versionById.Files.First().Hashes.Sha1;

        var versionByHash = await _api.GetVersionByHashAsync(hashesSha1);
        
        Assert.That(versionById.Id, Is.EqualTo(versionByHash.Id));
    }
    
    [Test]
    public async Task GetVersionByHashSha512()
    {
        var search = await _api.SearchProjectsAsync("");

        var first = search.Hits.First();

        var versionById = await _api.GetVersionByIdAsync((await _api.GetProjectAsync(first.ProjectId)).Versions.First());

        var hashesSha1 = versionById.Files.First().Hashes.Sha512;

        var versionByHash = await _api.GetVersionByHashAsync(hashesSha1, HashAlgorithm.Sha512);
        
        Assert.That(versionById.Id, Is.EqualTo(versionByHash.Id));
    }
}