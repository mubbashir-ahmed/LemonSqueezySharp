using System.Net;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Exceptions;
using LemonSqueezy.NET.Models.Products;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class ProductsClientTests
{
    private const string SingleProductPayload = """
        {
          "data": {
            "id": "100",
            "type": "products",
            "attributes": {
              "store_id": 1,
              "name": "Pro Plan",
              "slug": "pro-plan",
              "description": "<p>The pro plan.</p>",
              "status": "published",
              "status_formatted": "Published",
              "thumb_url": "https://example.com/thumb.png",
              "large_thumb_url": "https://example.com/large.png",
              "price": 4900,
              "price_formatted": "$49.00",
              "from_price": null,
              "to_price": null,
              "pay_what_you_want": false,
              "buy_now_url": "https://acme.lemonsqueezy.com/buy/abc",
              "test_mode": false,
              "created_at": "2024-01-02T03:04:05Z",
              "updated_at": "2024-02-03T04:05:06Z"
            }
          }
        }
        """;

    private const string ProductListPayload = """
        {
          "data": [
            { "id": "1", "type": "products", "attributes": { "name": "Starter", "price": 900 } },
            { "id": "2", "type": "products", "attributes": { "name": "Pro",     "price": 4900 } }
          ],
          "links": {
            "first": "https://api.lemonsqueezy.com/v1/products?page[number]=1",
            "next":  "https://api.lemonsqueezy.com/v1/products?page[number]=2"
          },
          "meta": { "page": { "number": 1, "size": 2, "total": 7, "lastPage": 4 } }
        }
        """;

    private static (ProductsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new ProductsClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_product_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleProductPayload);

        var result = await client.GetAsync("100");

        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be("100");
        result.Data.Type.Should().Be("products");
        result.Data.Attributes!.Name.Should().Be("Pro Plan");
        result.Data.Attributes.Price.Should().Be(4900);
        result.Data.Attributes.PriceFormatted.Should().Be("$49.00");
        result.Data.Attributes.Status.Should().Be("published");
        result.Data.Attributes.PayWhatYouWant.Should().BeFalse();
        result.Data.Attributes.BuyNowUrl.Should().Be("https://acme.lemonsqueezy.com/buy/abc");

        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/products/100");
        handler.Requests[0].Method.Should().Be(HttpMethod.Get);
    }

    [Fact]
    public async Task GetAsync_url_encodes_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleProductPayload);

        _ = await client.GetAsync("a b/c");

        handler.Requests[0].RequestUri!.PathAndQuery.Should().EndWith("/v1/products/a%20b%2Fc");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("   ");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task GetAsync_for_missing_product_throws_NotFound()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(
            HttpStatusCode.NotFound,
            """{"errors":[{"status":"404","title":"Not Found","detail":"Product not found"}]}""");

        var act = () => client.GetAsync("missing");

        await act.Should().ThrowAsync<LemonSqueezyNotFoundException>()
            .Where(ex => ex.Message.Contains("Product not found"));
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_products_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ProductListPayload);

        var result = await client.ListAsync();

        result.Items.Should().HaveCount(2);
        result.Items[0].Attributes!.Name.Should().Be("Starter");
        result.Items[1].Attributes!.Price.Should().Be(4900);

        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/products");
    }

    [Fact]
    public async Task ListAsync_serialises_paging_and_store_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ProductListPayload);

        _ = await client.ListAsync(new ProductListFilter
        {
            PageNumber = 2,
            PageSize = 10,
            StoreId = "1",
            Include = "variants",
        });

        var query = handler.Requests[0].RequestUri!.Query;
        query.Should().Contain("page%5Bnumber%5D=2");
        query.Should().Contain("page%5Bsize%5D=10");
        query.Should().Contain("filter%5Bstore_id%5D=1");
        query.Should().Contain("include=variants");
    }

    [Fact]
    public async Task ListAsync_exposes_paging_cursors_from_envelope()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ProductListPayload);

        var result = await client.ListAsync();

        result.HasNext.Should().BeTrue();
        result.HasPrevious.Should().BeFalse();
        result.Meta!.Page!.Total.Should().Be(7);
        result.Meta.Page.LastPage.Should().Be(4);
    }
}
