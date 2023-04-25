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
        
        var version = await Client.VersionFile.GetVersionByHashAsync(hash, HashAlgorithm.Sha1);
        
        Assert.That(version, Is.Not.Null);
        // Check that one of the files has the correct hash
        
        var file = version.Files.FirstOrDefault(f => f.Hashes.Sha1 == hash);
        
        Assert.That(file, Is.Not.Null);
    }
}