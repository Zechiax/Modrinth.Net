using System.Net;
using System.Text;
using Modrinth.Exceptions;
using Modrinth.Http;
using Modrinth.Models;

namespace Modrinth.Net.Test;

[TestFixture]
public class RequesterTests
{
    private sealed class DelegateMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsync;

        public DelegateMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync)
        {
            _sendAsync = sendAsync;
        }

        public int Calls { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Calls++;
            return await _sendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }

    private static Requester CreateRequester(HttpMessageHandler handler, int retryCount = 5, int maxConcurrent = 10)
    {
        var config = new ModrinthClientConfig
        {
            BaseUrl = "http://test.com",
            UserAgent = "TestAgent",
            RateLimitRetryCount = retryCount,
            MaxConcurrentRequests = maxConcurrent
        };

        return new Requester(config, new HttpClient(handler));
    }

    [Test]
    public void Requester_ShouldWrap_HttpRequestException()
    {
        var handler = new DelegateMessageHandler((_, _) => throw new HttpRequestException("boom"));
        using var requester = CreateRequester(handler);

        var request = new HttpRequestMessage(HttpMethod.Get, "http://test.com");

        Assert.ThrowsAsync<ModrinthApiException>(async () => await requester.GetJsonAsync<object>(request));
    }

    [Test]
    public async Task SendAsync_429Then200_RetriesAndSucceeds()
    {
        var bodies = new List<string>();
        var handler = new DelegateMessageHandler(async (request, cancellationToken) =>
        {
            if (request.Content is not null)
                bodies.Add(await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));

            if (bodies.Count == 1)
            {
                var rateLimited = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
                rateLimited.Headers.Add("X-Ratelimit-Reset", "0");
                return rateLimited;
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            };
        });

        using var requester = CreateRequester(handler, retryCount: 2);

        var request = new HttpRequestMessage(HttpMethod.Post, "http://test.com")
        {
            Content = new StringContent("payload", Encoding.UTF8, "text/plain")
        };

        using var response = await requester.SendAsync(request);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(handler.Calls, Is.EqualTo(2));
            Assert.That(bodies, Is.EqualTo(new[] { "payload", "payload" }));
        }
    }

    [Test]
    public void SendAsync_429BeyondRetryCount_ThrowsModrinthApiException()
    {
        var handler = new DelegateMessageHandler((_, _) =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
            response.Headers.Add("X-Ratelimit-Reset", "0");
            return Task.FromResult(response);
        });

        using var requester = CreateRequester(handler, retryCount: 1);
        var request = new HttpRequestMessage(HttpMethod.Get, "http://test.com");

        Assert.ThrowsAsync<ModrinthApiException>(async () => await requester.SendAsync(request));
        Assert.That(handler.Calls, Is.EqualTo(2));
    }

    [Test]
    public void SendAsync_429WithoutResetHeader_DoesNotRetry()
    {
        var handler = new DelegateMessageHandler((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.TooManyRequests)));

        using var requester = CreateRequester(handler, retryCount: 5);
        var request = new HttpRequestMessage(HttpMethod.Get, "http://test.com");

        Assert.ThrowsAsync<ModrinthApiException>(async () => await requester.SendAsync(request));
        Assert.That(handler.Calls, Is.EqualTo(1));
    }

    [Test]
    public void SendAsync_CancelledDuringRateLimitDelay_ThrowsOperationCanceledException()
    {
        var handler = new DelegateMessageHandler((_, _) =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
            response.Headers.Add("X-Ratelimit-Reset", "5");
            return Task.FromResult(response);
        });

        using var requester = CreateRequester(handler, retryCount: 2);
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));
        var token = cts.Token;

        Assert.That(async () => await requester.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://test.com"), token),
            Throws.InstanceOf<OperationCanceledException>());
        Assert.That(handler.Calls, Is.EqualTo(1));
    }

    [Test]
    public async Task SendAsync_ReleasesSemaphoreAfterFailureOrCancellation()
    {
        var call = 0;
        var handler = new DelegateMessageHandler((_, _) =>
        {
            call++;
            if (call == 1)
                throw new HttpRequestException("first call fails");

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            });
        });

        using var requester = CreateRequester(handler, maxConcurrent: 1);

        var first = new HttpRequestMessage(HttpMethod.Get, "http://test.com/one");
        Assert.ThrowsAsync<ModrinthApiException>(async () => await requester.SendAsync(first));

        var second = new HttpRequestMessage(HttpMethod.Get, "http://test.com/two");
        using var response = await requester.SendAsync(second);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(handler.Calls, Is.EqualTo(2));
    }

    [Test]
    public async Task GetJsonAsync_GalleryOrderingAboveInt32Max_DeserializesSuccessfully()
    {
        const long expectedOrdering = 6844313514;
        var json =
            "[{\"url\":\"https://example.com/image.png\",\"featured\":false,\"title\":\"t\",\"description\":\"d\",\"created\":\"2026-01-01T00:00:00Z\",\"ordering\":" +
            expectedOrdering + "}]";

        var handler = new DelegateMessageHandler((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            }));

        using var requester = CreateRequester(handler);
        var request = new HttpRequestMessage(HttpMethod.Get, "http://test.com");

        var galleries = await requester.GetJsonAsync<Gallery[]>(request);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(galleries, Has.Length.EqualTo(1));
            Assert.That(galleries[0].Ordering, Is.EqualTo(expectedOrdering));
        }
    }

    [Test]
    public async Task GetJsonAsync_GalleryOrderingHugeInt64_DeserializesSuccessfully()
    {
        const long expectedOrdering = 68541332132131350;
        var json =
            "[{\"url\":\"https://example.com/image.png\",\"featured\":true,\"title\":\"t\",\"description\":\"d\",\"created\":\"2026-01-01T00:00:00Z\",\"ordering\":" +
            expectedOrdering + "}]";

        var handler = new DelegateMessageHandler((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            }));

        using var requester = CreateRequester(handler);
        var request = new HttpRequestMessage(HttpMethod.Get, "http://test.com");

        var galleries = await requester.GetJsonAsync<Gallery[]>(request);

        Assert.That(galleries[0].Ordering, Is.EqualTo(expectedOrdering));
    }

    [Test]
    public async Task GetJsonAsync_TeamMemberOrderingAboveInt32Max_DeserializesSuccessfully()
    {
        const long expectedOrdering = 6844313514;
        var json =
            "[{\"team_id\":\"team_1\",\"user\":{\"username\":\"user\",\"name\":\"User\",\"id\":\"u1\",\"github_id\":null,\"avatar_url\":\"https://example.com/avatar.png\",\"bio\":\"\",\"created\":\"2026-01-01T00:00:00Z\",\"role\":\"developer\"},\"role\":\"Owner\",\"permissions\":null,\"accepted\":true,\"payouts_split\":null,\"ordering\":" +
            expectedOrdering + "}]";

        var handler = new DelegateMessageHandler((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            }));

        using var requester = CreateRequester(handler);
        var request = new HttpRequestMessage(HttpMethod.Get, "http://test.com");

        var teamMembers = await requester.GetJsonAsync<TeamMember[]>(request);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(teamMembers, Has.Length.EqualTo(1));
            Assert.That(teamMembers[0].Ordering, Is.EqualTo(expectedOrdering));
        }
    }
}