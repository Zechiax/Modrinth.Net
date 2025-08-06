using Modrinth.Models.Enums;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class VersionFileTests : UnauthenticatedTestBase
{
    [Test]
    [TestCase(0)]
    [TestCase(1)]
    public async Task GetVersionFromHashSha512(int index)
    {
        var hash = TestData.ValidSha512Hashes[index];

        var version = await NoAuthClient.VersionFile.GetVersionByHashAsync(hash, HashAlgorithm.Sha512);

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
        var hash = TestData.ValidSha1Hashes[index];

        var version = await NoAuthClient.VersionFile.GetVersionByHashAsync(hash);

        Assert.That(version, Is.Not.Null);
        // Check that one of the files has the correct hash

        var file = version.Files.FirstOrDefault(f => f.Hashes.Sha1 == hash);

        Assert.That(file, Is.Not.Null);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    public async Task EnsureFileNameSerializationCorrect(int index)
    {
        var hash = TestData.ValidSha1Hashes[index];
        var fileName = TestData.ValidFileNames[index];

        var version = await NoAuthClient.VersionFile.GetVersionByHashAsync(hash);

        Assert.That(version, Is.Not.Null);

        var file = version.Files.FirstOrDefault(f => f.Hashes.Sha1 == hash && f.FileName == fileName);
        
        Assert.That(file, Is.Not.Null);
    }

    [Test]
    public async Task GetMultipleVersionsFromHashesSha1()
    {
        var hashes = TestData.ValidSha1Hashes;

        var versions = await NoAuthClient.VersionFile.GetMultipleVersionsByHashAsync(hashes);

        Assert.That(versions, Is.Not.Null);
        Assert.That(versions, Is.Not.Empty);

        foreach (var hash in hashes) Assert.That(versions.ContainsKey(hash), Is.True);
    }

    [Test]
    public async Task GetMultipleVersionsFromHashesSha512()
    {
        var hashes = TestData.ValidSha512Hashes;

        var versions = await NoAuthClient.VersionFile.GetMultipleVersionsByHashAsync(hashes, HashAlgorithm.Sha512);

        Assert.That(versions, Is.Not.Null);
        Assert.That(versions, Is.Not.Empty);

        foreach (var hash in hashes) Assert.That(versions.ContainsKey(hash), Is.True);
    }

    [Test]
    [TestCase(0, new[] {"fabric"}, new[] {"1.17.1"})]
    public async Task GetLatestVersionFromHashSha1WithFilters(int index, string[] loaders, string[] gameVersions)
    {
        var hash = TestData.ValidSha1Hashes[index];

        var version =
            await NoAuthClient.VersionFile.GetLatestVersionByHashAsync(hash, HashAlgorithm.Sha1, loaders, gameVersions);

        Assert.Multiple(() =>
        {
            Assert.That(version, Is.Not.Null);
            Assert.That(version.Loaders, Is.SupersetOf(loaders));
            Assert.That(version.GameVersions, Is.SupersetOf(gameVersions));
        });
    }

    [Test]
    [TestCase(0, new[] {"fabric"}, new[] {"1.17.1"})]
    public async Task GetLatestVersionFromHashSha512WithFilters(int index, string[] loaders, string[] gameVersions)
    {
        var hash = TestData.ValidSha512Hashes[index];

        var version =
            await NoAuthClient.VersionFile.GetLatestVersionByHashAsync(hash, HashAlgorithm.Sha512, loaders, gameVersions);

        Assert.Multiple(() =>
        {
            Assert.That(version, Is.Not.Null);
            Assert.That(version.Loaders, Is.SupersetOf(loaders));
            Assert.That(version.GameVersions, Is.SupersetOf(gameVersions));
        });
    }

    [Test]
    [TestCase(0, new[] {"fabric"}, new[] {"1.17.1"})]
    public async Task GetMultipleLatestVersionsFromHashSha1WithFilters(int index, string[] loaders,
        string[] gameVersions)
    {
        var hashes = TestData.ValidSha1Hashes;

        var versions =
            await NoAuthClient.VersionFile.GetMultipleLatestVersionsByHashAsync(hashes, HashAlgorithm.Sha1, loaders,
                gameVersions);

        Assert.Multiple(() =>
        {
            Assert.That(versions, Is.Not.Null);
            Assert.That(versions, Is.Not.Empty);

            foreach (var version in versions.Values)
            {
                Assert.That(version.Loaders, Is.SupersetOf(loaders));
                Assert.That(version.GameVersions, Is.SupersetOf(gameVersions));
            }
        });
    }

    [Test]
    [TestCase(0, new[] {"fabric"}, new[] {"1.17.1"})]
    public async Task GetMultipleLatestVersionsFromHashSha512WithFilters(int index, string[] loaders,
        string[] gameVersions)
    {
        var hashes = TestData.ValidSha512Hashes;

        var versions =
            await NoAuthClient.VersionFile.GetMultipleLatestVersionsByHashAsync(hashes, HashAlgorithm.Sha512, loaders,
                gameVersions);

        Assert.Multiple(() =>
        {
            Assert.That(versions, Is.Not.Null);
            Assert.That(versions, Is.Not.Empty);

            foreach (var version in versions.Values)
            {
                Assert.That(version.Loaders, Is.SupersetOf(loaders));
                Assert.That(version.GameVersions, Is.SupersetOf(gameVersions));
            }
        });
    }
}