using Modrinth.Models.Enums;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class TestVersionFile : EndpointTests
{
    [Test]
    [TestCase(0)]
    [TestCase(1)]
    public async Task GetVersionFromHashSha512(int index)
    {
        var hash = ValidSha512Hashes[index];

        var version = await Client.VersionFile.GetVersionByHashAsync(hash, HashAlgorithm.Sha512);

        Assert.That(version, Is.Not.Null);
        // Check that one of the files has the correct hash

        var file = version.Files.FirstOrDefault(f => f.Hashes.Sha512 == hash);

        Assert.That(file, Is.Not.Null);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    public async Task GetVersionFromHashSha1(int index)
    {
        var hash = ValidSha1Hashes[index];

        var version = await Client.VersionFile.GetVersionByHashAsync(hash);

        Assert.That(version, Is.Not.Null);
        // Check that one of the files has the correct hash

        var file = version.Files.FirstOrDefault(f => f.Hashes.Sha1 == hash);

        Assert.That(file, Is.Not.Null);
    }

    [Test]
    public async Task GetMultipleVersionsFromHashesSha1()
    {
        var hashes = ValidSha1Hashes;

        var versions = await Client.VersionFile.GetMultipleVersionsByHashAsync(hashes);

        Assert.That(versions, Is.Not.Null);
        Assert.That(versions, Is.Not.Empty);

        foreach (var hash in hashes) Assert.That(versions.ContainsKey(hash), Is.True);
    }

    [Test]
    public async Task GetMultipleVersionsFromHashesSha512()
    {
        var hashes = ValidSha512Hashes;

        var versions = await Client.VersionFile.GetMultipleVersionsByHashAsync(hashes, HashAlgorithm.Sha512);

        Assert.That(versions, Is.Not.Null);
        Assert.That(versions, Is.Not.Empty);

        foreach (var hash in hashes) Assert.That(versions.ContainsKey(hash), Is.True);
    }
    
    [Test]
    [TestCase(0)]
    [TestCase(1)]
    public async Task GetLatestVersionFromHashSha1(int index)
    {
        var hash = ValidSha1Hashes[index];

        var version = await Client.VersionFile.GetLatestVersionByHashAsync(hash);

        Assert.That(version, Is.Not.Null);
        
        var versions = await Client.Version.GetProjectVersionListAsync(version.ProjectId);
        
        Assert.Multiple(() =>
        {
            Assert.That(versions, Is.Not.Null);
            Assert.That(versions, Is.Not.Empty);

            // We check that the version is the latest version
            Assert.That(version.Id, Is.EqualTo(versions[0].Id));
        });
    }
    
    [Test]
    [TestCase(0)]
    [TestCase(1)]
    public async Task GetLatestVersionFromHashSha512(int index)
    {
        var hash = ValidSha512Hashes[index];

        var version = await Client.VersionFile.GetLatestVersionByHashAsync(hash, HashAlgorithm.Sha512);

        Assert.That(version, Is.Not.Null);
        
        var versions = await Client.Version.GetProjectVersionListAsync(version.ProjectId);
        
        Assert.Multiple(() =>
        {
            Assert.That(versions, Is.Not.Null);
            Assert.That(versions, Is.Not.Empty);

            // We check that the version is the latest version
            Assert.That(version.Id, Is.EqualTo(versions[0].Id));
        });
    }
    
    [Test]
    [TestCase(0)]
    public async Task GetLatestVersionFromHashSha1WithFilters(int index, string[] loaders, string[] gameVersions)
    {
        var hash = ValidSha1Hashes[index];

        var version = await Client.VersionFile.GetLatestVersionByHashAsync(hash, HashAlgorithm.Sha1, loaders, gameVersions);

        Assert.That(version, Is.Not.Null);
        
        var versions = await Client.Version.GetProjectVersionListAsync(version.ProjectId);
        
        Assert.Multiple(() =>
        {
            Assert.That(versions, Is.Not.Null);
            Assert.That(versions, Is.Not.Empty);

            // We check that the version is the latest version
            Assert.That(version.Id, Is.EqualTo(versions[0].Id));
        });
    }
    
}