using Modrinth.Exceptions;
using Modrinth.Http;

namespace Modrinth.Net.Test;

[TestFixture]
public class RequesterTests
{
    [Test]
    public void Requester_ShouldNotThrow_HttpException()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        mockHttpClient.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .Throws(new HttpRequestException("An error occurred while sending the request."));

        var config = new ModrinthClientConfig
        {
            BaseUrl = "http://test.com",
            UserAgent = "TestAgent",
            ModrinthToken = "TestToken"
        };

        var requester = new Requester(config, mockHttpClient.Object);

        var request = new HttpRequestMessage(HttpMethod.Get, "http://test.com");
        
        Assert.ThrowsAsync<ModrinthApiException>(async () => await requester.GetJsonAsync<object>(request));
    }

}