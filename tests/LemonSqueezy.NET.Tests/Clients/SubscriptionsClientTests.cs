using System.Net;
using System.Text.Json;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.Subscriptions;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class SubscriptionsClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "1",
            "type": "subscriptions",
            "attributes": {
              "store_id": 1,
              "customer_id": 1,
              "order_id": 1,
              "order_item_id": 1,
              "product_id": 1,
              "variant_id": 1,
              "product_name": "Pro Plan",
              "variant_name": "Monthly",
              "user_name": "John Doe",
              "user_email": "johndoe@example.com",
              "status": "active",
              "status_formatted": "Active",
              "card_brand": "visa",
              "card_last_four": "4242",
              "pause": null,
              "cancelled": false,
              "trial_ends_at": null,
              "billing_anchor": 15,
              "first_subscription_item": {
                "id": 100, "subscription_id": 1, "price_id": 200, "quantity": 1, "is_usage_based": false
              },
              "urls": {
                "update_payment_method": "https://acme.lemonsqueezy.com/billing/update",
                "customer_portal": "https://acme.lemonsqueezy.com/billing"
              },
              "renews_at": "2024-09-01T00:00:00Z",
              "ends_at": null,
              "test_mode": false
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "subscriptions", "attributes": { "user_email": "a@example.com", "status": "active" } },
            { "id": "2", "type": "subscriptions", "attributes": { "user_email": "b@example.com", "status": "cancelled" } }
          ],
          "links": { "next": "https://api.lemonsqueezy.com/v1/subscriptions?page[number]=2" },
          "meta": { "page": { "number": 1, "size": 2, "total": 7 } }
        }
        """;

    private static (SubscriptionsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new SubscriptionsClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_subscription_with_urls_and_first_item()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("1");

        result.Data!.Id.Should().Be("1");
        result.Data.Attributes!.Status.Should().Be(SubscriptionStatus.Active);
        result.Data.Attributes.CardBrand.Should().Be("visa");
        result.Data.Attributes.CardLastFour.Should().Be("4242");
        result.Data.Attributes.BillingAnchor.Should().Be(15);
        result.Data.Attributes.FirstSubscriptionItem!.PriceId.Should().Be(200);
        result.Data.Attributes.Urls!.CustomerPortal.Should().Be("https://acme.lemonsqueezy.com/billing");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/subscriptions/1");
    }

    [Fact]
    public async Task ListAsync_serialises_every_supported_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new SubscriptionListFilter
        {
            StoreId = "1",
            ProductId = "2",
            VariantId = "3",
            UserEmail = "a@example.com",
            Status = "active",
            PageNumber = 2,
        });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Bstore_id%5D=1");
        q.Should().Contain("filter%5Bproduct_id%5D=2");
        q.Should().Contain("filter%5Bvariant_id%5D=3");
        q.Should().Contain("filter%5Buser_email%5D=a%40example.com");
        q.Should().Contain("filter%5Bstatus%5D=active");
        q.Should().Contain("page%5Bnumber%5D=2");
    }

    [Fact]
    public async Task UpdateAsync_patches_with_only_set_attributes_including_pause()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        _ = await client.UpdateAsync("1", new UpdateSubscriptionRequest
        {
            VariantId = 99,
            BillingAnchor = 1,
            Pause = new SubscriptionPauseRequest { Mode = "free" },
        });

        var request = handler.Requests[0];
        request.Method.Method.Should().Be("PATCH");
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/subscriptions/1");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("subscriptions");
        data.GetProperty("id").GetString().Should().Be("1");
        var attrs = data.GetProperty("attributes");
        attrs.GetProperty("variant_id").GetInt64().Should().Be(99);
        attrs.GetProperty("billing_anchor").GetInt32().Should().Be(1);
        attrs.GetProperty("pause").GetProperty("mode").GetString().Should().Be("free");
        attrs.TryGetProperty("product_id", out _).Should().BeFalse("nulls must not serialise");
        attrs.TryGetProperty("cancelled", out _).Should().BeFalse();
    }

    [Fact]
    public async Task CancelAsync_sends_DELETE_and_returns_cancelled_subscription()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.CancelAsync("1");

        result.Data!.Id.Should().Be("1");
        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Delete);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/subscriptions/1");
        request.Content.Should().BeNull("DELETE must not carry a body");
    }

    [Fact]
    public async Task UpdateAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.UpdateAsync("", new UpdateSubscriptionRequest());

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }
}
