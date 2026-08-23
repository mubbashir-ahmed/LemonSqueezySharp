using System.Net;
using System.Text.Json;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Models.Webhooks;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class WebhooksClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "33",
            "type": "webhooks",
            "attributes": {
              "store_id": 1,
              "url": "https://example.com/webhook",
              "events": ["order_created", "subscription_created"],
              "last_sent_at": "2024-06-01T00:00:00Z",
              "test_mode": false,
              "created_at": "2024-01-01T00:00:00Z",
              "updated_at": "2024-06-01T00:00:00Z"
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "webhooks", "attributes": { "url": "https://example.com/a" } },
            { "id": "2", "type": "webhooks", "attributes": { "url": "https://example.com/b" } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 2 } }
        }
        """;

    private static (WebhooksClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new WebhooksClient(httpClient), handler);
    }

    [Fact]
    public async Task CreateAsync_posts_jsonapi_envelope_with_store_relationship()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.Created, SinglePayload);

        var result = await client.CreateAsync(new CreateWebhookRequest
        {
            StoreId = "1",
            Url = "https://example.com/webhook",
            Events = new[] { "order_created", "subscription_created" },
            Secret = "whsec_supersecret",
        });

        result.Data!.Id.Should().Be("33");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("webhooks");
        var attrs = data.GetProperty("attributes");
        attrs.GetProperty("url").GetString().Should().Be("https://example.com/webhook");
        attrs.GetProperty("secret").GetString().Should().Be("whsec_supersecret");
        var events = attrs.GetProperty("events");
        events.GetArrayLength().Should().Be(2);
        events[0].GetString().Should().Be("order_created");
        var store = data.GetProperty("relationships").GetProperty("store").GetProperty("data");
        store.GetProperty("type").GetString().Should().Be("stores");
        store.GetProperty("id").GetString().Should().Be("1");

        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/webhooks");
    }

    [Theory]
    [InlineData("", "https://example.com", "order_created")]
    [InlineData("1", "", "order_created")]
    [InlineData("1", "https://example.com", null)]
    public async Task CreateAsync_rejects_missing_required_fields(string storeId, string url, string? singleEvent)
    {
        var (client, _) = CreateSut();

        var act = () => client.CreateAsync(new CreateWebhookRequest
        {
            StoreId = storeId,
            Url = url,
            Events = singleEvent is null ? Array.Empty<string>() : new[] { singleEvent },
        });

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("request");
    }

    [Fact]
    public async Task GetAsync_returns_webhook()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("33");

        result.Data!.Attributes!.Url.Should().Be("https://example.com/webhook");
        result.Data.Attributes.Events.Should().Contain("order_created");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/webhooks/33");
    }

    [Fact]
    public async Task UpdateAsync_patches_with_only_set_fields()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        _ = await client.UpdateAsync("33", new UpdateWebhookRequest
        {
            Url = "https://example.com/new-endpoint",
        });

        var request = handler.Requests[0];
        request.Method.Method.Should().Be("PATCH");
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/webhooks/33");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("webhooks");
        data.GetProperty("id").GetString().Should().Be("33");
        var attrs = data.GetProperty("attributes");
        attrs.GetProperty("url").GetString().Should().Be("https://example.com/new-endpoint");
        attrs.TryGetProperty("events", out _).Should().BeFalse("nulls must not serialise");
        attrs.TryGetProperty("secret", out _).Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_sends_DELETE_and_completes_on_204()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueStatus(HttpStatusCode.NoContent);

        await client.DeleteAsync("33");

        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Delete);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/webhooks/33");
    }

    [Fact]
    public async Task ListAsync_serialises_store_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new WebhookListFilter { StoreId = "1" });

        handler.Requests[0].RequestUri!.Query.Should().Contain("filter%5Bstore_id%5D=1");
    }
}
