using System.Net;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Models.DiscountRedemptions;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class DiscountRedemptionsClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "10",
            "type": "discount-redemptions",
            "attributes": {
              "discount_id": 77,
              "order_id": 500,
              "discount_name": "Black Friday",
              "discount_code": "BLACKFRIDAY",
              "discount_amount": 25,
              "discount_amount_type": "percent",
              "amount": 250,
              "created_at": "2024-11-29T00:00:00Z"
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "discount-redemptions", "attributes": { "discount_code": "WELCOME10", "amount": 100 } },
            { "id": "2", "type": "discount-redemptions", "attributes": { "discount_code": "WELCOME10", "amount": 100 } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 4 } }
        }
        """;

    private static (DiscountRedemptionsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new DiscountRedemptionsClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_discount_redemption()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("10");

        result.Data!.Attributes!.DiscountCode.Should().Be("BLACKFRIDAY");
        result.Data.Attributes.DiscountAmountType.Should().Be("percent");
        result.Data.Attributes.Amount.Should().Be(250);
        result.Data.Attributes.OrderId.Should().Be(500);
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/discount-redemptions/10");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_discount_and_order_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new DiscountRedemptionListFilter
        {
            DiscountId = "77",
            OrderId = "500",
        });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Bdiscount_id%5D=77");
        q.Should().Contain("filter%5Border_id%5D=500");
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        var result = await client.ListAsync();

        result.Items.Should().HaveCount(2);
        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/discount-redemptions");
    }
}
