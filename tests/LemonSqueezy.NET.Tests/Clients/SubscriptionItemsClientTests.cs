using System.Net;
using System.Text.Json;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.SubscriptionItems;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class SubscriptionItemsClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "100",
            "type": "subscription-items",
            "attributes": {
              "subscription_id": 1,
              "price_id": 200,
              "quantity": 5,
              "is_usage_based": true
            }
          }
        }
        """;

    private const string CurrentUsagePayload = """
        {
          "meta": {
            "period_start": "2024-01-01T00:00:00.000000Z",
            "period_end":   "2024-02-01T00:00:00.000000Z",
            "quantity": 50,
            "interval_unit": "month",
            "interval_quantity": 1
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "subscription-items", "attributes": { "subscription_id": 1, "quantity": 1 } },
            { "id": "2", "type": "subscription-items", "attributes": { "subscription_id": 1, "quantity": 2 } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 3 } }
        }
        """;

    private static (SubscriptionItemsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new SubscriptionItemsClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_subscription_item()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("100");

        result.Data!.Attributes!.PriceId.Should().Be(200);
        result.Data.Attributes.Quantity.Should().Be(5);
        result.Data.Attributes.IsUsageBased.Should().BeTrue();
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/subscription-items/100");
    }

    [Fact]
    public async Task GetCurrentUsageAsync_returns_meta_block()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, CurrentUsagePayload);

        var usage = await client.GetCurrentUsageAsync("100");

        usage.Should().NotBeNull();
        usage!.Quantity.Should().Be(50);
        usage.IntervalUnit.Should().Be("month");
        usage.IntervalQuantity.Should().Be(1);
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/subscription-items/100/current-usage");
    }

    [Fact]
    public async Task UpdateAsync_patches_with_quantity_only()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        _ = await client.UpdateAsync("100", new UpdateSubscriptionItemRequest
        {
            Quantity = 10,
            InvoiceImmediately = true,
        });

        var request = handler.Requests[0];
        request.Method.Method.Should().Be("PATCH");
        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var attrs = sent.GetProperty("data").GetProperty("attributes");
        attrs.GetProperty("quantity").GetInt32().Should().Be(10);
        attrs.GetProperty("invoice_immediately").GetBoolean().Should().BeTrue();
        attrs.TryGetProperty("disable_prorations", out _).Should().BeFalse("nulls must not serialise");
    }

    [Fact]
    public async Task ListAsync_serialises_subscription_and_price_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new SubscriptionItemListFilter
        {
            SubscriptionId = "1",
            PriceId = "200",
        });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Bsubscription_id%5D=1");
        q.Should().Contain("filter%5Bprice_id%5D=200");
    }
}
