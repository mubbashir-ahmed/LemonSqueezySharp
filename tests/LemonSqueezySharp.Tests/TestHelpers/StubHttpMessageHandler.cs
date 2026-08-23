using System.Net;
using System.Text;

namespace LemonSqueezySharp.Tests.TestHelpers;

/// <summary>
/// A tiny <see cref="HttpMessageHandler"/> that returns a queued sequence of canned
/// responses. Records every request it receives (plus the captured body string) so
/// tests can assert on URL, method, headers, and payload.
/// </summary>
internal sealed class StubHttpMessageHandler : HttpMessageHandler
{
    private readonly Queue<Func<HttpRequestMessage, HttpResponseMessage>> _responses = new();

    public List<HttpRequestMessage> Requests { get; } = new();

    public List<string?> RequestBodies { get; } = new();

    public StubHttpMessageHandler EnqueueJson(HttpStatusCode statusCode, string json)
    {
        _responses.Enqueue(_ => new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/vnd.api+json"),
        });
        return this;
    }

    public StubHttpMessageHandler EnqueueStatus(HttpStatusCode statusCode, string? body = null)
    {
        _responses.Enqueue(_ => new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(body ?? string.Empty, Encoding.UTF8, "application/vnd.api+json"),
        });
        return this;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request);

        if (request.Content is not null)
        {
#if NET10_0_OR_GREATER
            RequestBodies.Add(await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
#else
            RequestBodies.Add(await request.Content.ReadAsStringAsync().ConfigureAwait(false));
#endif
        }
        else
        {
            RequestBodies.Add(null);
        }

        if (_responses.Count == 0)
        {
            throw new InvalidOperationException("No queued responses left for StubHttpMessageHandler.");
        }

        var factory = _responses.Dequeue();
        return factory(request);
    }
}
