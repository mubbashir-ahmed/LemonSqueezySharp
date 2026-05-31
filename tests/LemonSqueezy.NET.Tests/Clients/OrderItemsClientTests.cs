using System.Net;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.OrderItems;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class OrderItemsClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "900",
            "type": "order-items",
            "attributes": {
              "order_id": 500,
              "product_id": 100,
              "variant_id": 10,
              "price_id": 200,
              "product_name": "Pro Plan",
              "variant_name": "Monthly",
              "price": 4900,
              "quantity": 1,
              "test_mode": false
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "order-items", "attributes": { "order_id": 500, "price": 100 } },
            { "id": "2", "type": "order-items", "attributes": { "order_id": 500, "price": 200 } }
          ],
          "links": { "next": "https://api.lemonsqueezy.com/v1/order-items?page[number]=2" },
          "meta": { "page": { "number": 1, "size": 2, "total": 4 } }
        }
        """;

    private static (OrderItemsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new OrderItemsClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_order_item_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("900");

        result.Data!.Id.Should().Be("900");
        result.Data.Attributes!.OrderId.Should().Be(500);
        result.Data.Attributes.ProductName.Should().Be("Pro Plan");
        result.Data.Attributes.Price.Should().Be(4900);
        result.Data.Attributes.Quantity.Should().Be(1);
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/order-items/900");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_all_three_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new OrderItemListFilter
        {
            OrderId = "500",
            ProductId = "100",
            VariantId = "10",
            PageSize = 50,
        });

        var query = handler.Requests[0].RequestUri!.Query;
        query.Should().Contain("filter%5Border_id%5D=500");
        query.Should().Contain("filter%5Bproduct_id%5D=100");
        query.Should().Contain("filter%5Bvariant_id%5D=10");
        query.Should().Contain("page%5Bsize%5D=50");
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        var result = await client.ListAsync();

        result.Items.Should().HaveCount(2);
        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/order-items");
    }
}
