using System.Net;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.Prices;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class PricesClientTests
{
    private const string SinglePricePayload = """
        {
          "data": {
            "id": "200",
            "type": "prices",
            "attributes": {
              "variant_id": 10,
              "category": "subscription",
              "scheme": "graduated",
              "unit_price": 1900,
              "unit_price_decimal": "19.00",
              "setup_fee_enabled": false,
              "package_size": 1,
              "renewal_interval_unit": "month",
              "renewal_interval_quantity": 1,
              "tiers": [
                { "last_unit": "10",  "unit_price": 1000 },
                { "last_unit": "inf", "unit_price": 800  }
              ],
              "tax_code": "txcd_10000000",
              "created_at": "2024-01-01T00:00:00Z",
              "updated_at": "2024-02-01T00:00:00Z"
            }
          }
        }
        """;

    private const string PriceListPayload = """
        {
          "data": [
            { "id": "1", "type": "prices", "attributes": { "variant_id": 10, "unit_price": 1000 } },
            { "id": "2", "type": "prices", "attributes": { "variant_id": 10, "unit_price": 2000 } }
          ],
          "links": { "next": "https://api.lemonsqueezy.com/v1/prices?page[number]=2" },
          "meta": { "page": { "number": 1, "size": 2, "total": 6 } }
        }
        """;

    private static (PricesClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new PricesClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_price_by_id_with_tiers()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePricePayload);

        var result = await client.GetAsync("200");

        result.Data!.Id.Should().Be("200");
        result.Data.Attributes!.Category.Should().Be("subscription");
        result.Data.Attributes.Scheme.Should().Be("graduated");
        result.Data.Attributes.UnitPrice.Should().Be(1900);
        result.Data.Attributes.Tiers.Should().HaveCount(2);
        result.Data.Attributes.Tiers![0].UnitPrice.Should().Be(1000);
        result.Data.Attributes.Tiers[1].LastUnit.Should().Be("inf");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/prices/200");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_variant_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, PriceListPayload);

        _ = await client.ListAsync(new PriceListFilter
        {
            VariantId = "10",
            PageSize = 25,
        });

        var query = handler.Requests[0].RequestUri!.Query;
        query.Should().Contain("filter%5Bvariant_id%5D=10");
        query.Should().Contain("page%5Bsize%5D=25");
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_prices_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, PriceListPayload);

        var result = await client.ListAsync();

        result.Items.Should().HaveCount(2);
        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/prices");
    }
}
