using System.Net;
using System.Text.Json;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.UsageRecords;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class UsageRecordsClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "999",
            "type": "usage-records",
            "attributes": {
              "subscription_item_id": 100,
              "quantity": 50,
              "action": "increment",
              "created_at": "2024-01-15T00:00:00Z"
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "usage-records", "attributes": { "quantity": 10 } },
            { "id": "2", "type": "usage-records", "attributes": { "quantity": 20 } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 8 } }
        }
        """;

    private static (UsageRecordsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new UsageRecordsClient(httpClient), handler);
    }

    [Fact]
    public async Task CreateAsync_posts_jsonapi_envelope_with_subscription_item_relationship()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.Created, SinglePayload);

        var result = await client.CreateAsync(new CreateUsageRecordRequest
        {
            SubscriptionItemId = "100",
            Quantity = 50,
            Action = "increment",
        });

        result.Data!.Id.Should().Be("999");

        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/usage-records");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("usage-records");
        data.GetProperty("attributes").GetProperty("quantity").GetInt32().Should().Be(50);
        data.GetProperty("attributes").GetProperty("action").GetString().Should().Be("increment");
        var rel = data.GetProperty("relationships").GetProperty("subscription-item").GetProperty("data");
        rel.GetProperty("type").GetString().Should().Be("subscription-items");
        rel.GetProperty("id").GetString().Should().Be("100");
    }

    [Theory]
    [InlineData("", 5)]
    [InlineData("100", 0)]
    [InlineData("100", -1)]
    public async Task CreateAsync_rejects_invalid_inputs(string subscriptionItemId, int quantity)
    {
        var (client, _) = CreateSut();

        var act = () => client.CreateAsync(new CreateUsageRecordRequest
        {
            SubscriptionItemId = subscriptionItemId,
            Quantity = quantity,
        });

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("request");
    }

    [Fact]
    public async Task GetAsync_returns_usage_record()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("999");

        result.Data!.Attributes!.Quantity.Should().Be(50);
        result.Data.Attributes.Action.Should().Be("increment");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/usage-records/999");
    }

    [Fact]
    public async Task ListAsync_serialises_subscription_item_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new UsageRecordListFilter { SubscriptionItemId = "100" });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Bsubscription_item_id%5D=100");
    }
}
