using System.Net;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Exceptions;
using LemonSqueezySharp.Models.Requests;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class StoresClientTests
{
    private const string SingleStorePayload = """
        {
          "data": {
            "id": "1",
            "type": "stores",
            "attributes": {
              "name": "Acme Inc.",
              "slug": "acme-inc",
              "domain": "acme.lemonsqueezy.com",
              "url": "https://acme.lemonsqueezy.com",
              "avatar_url": "https://example.com/avatar.png",
              "plan": "fresh",
              "country": "US",
              "country_nicename": "United States",
              "currency": "USD",
              "total_sales": 1234,
              "total_revenue": 567890,
              "thirty_day_sales": 12,
              "thirty_day_revenue": 3400,
              "created_at": "2024-01-02T03:04:05Z",
              "updated_at": "2024-02-03T04:05:06Z"
            }
          }
        }
        """;

    private const string StoreListPayload = """
        {
          "data": [
            { "id": "1", "type": "stores", "attributes": { "name": "Acme Inc.", "currency": "USD" } },
            { "id": "2", "type": "stores", "attributes": { "name": "Globex",    "currency": "EUR" } }
          ],
          "links": {
            "first": "https://api.lemonsqueezy.com/v1/stores?page[number]=1",
            "last":  "https://api.lemonsqueezy.com/v1/stores?page[number]=3",
            "next":  "https://api.lemonsqueezy.com/v1/stores?page[number]=2"
          },
          "meta": {
            "page": { "number": 1, "size": 2, "total": 5, "lastPage": 3 }
          }
        }
        """;

    private static (StoresClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new StoresClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_store_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleStorePayload);

        var result = await client.GetAsync("1");

        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be("1");
        result.Data.Type.Should().Be("stores");
        result.Data.Attributes.Should().NotBeNull();
        result.Data.Attributes!.Name.Should().Be("Acme Inc.");
        result.Data.Attributes.Currency.Should().Be("USD");
        result.Data.Attributes.TotalRevenue.Should().Be(567890);
        result.Data.Attributes.Plan.Should().Be("fresh");

        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/stores/1");
        handler.Requests[0].Method.Should().Be(HttpMethod.Get);
    }

    [Fact]
    public async Task GetAsync_url_encodes_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleStorePayload);

        _ = await client.GetAsync("a b/c");

        handler.Requests[0].RequestUri!.PathAndQuery.Should().EndWith("/v1/stores/a%20b%2Fc");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("   ");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task GetAsync_for_missing_store_throws_NotFound()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(
            HttpStatusCode.NotFound,
            """{"errors":[{"status":"404","title":"Not Found","detail":"Store not found"}]}""");

        var act = () => client.GetAsync("missing");

        await act.Should().ThrowAsync<LemonSqueezyNotFoundException>()
            .Where(ex => ex.Message.Contains("Store not found"));
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_stores_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, StoreListPayload);

        var result = await client.ListAsync();

        result.Items.Should().HaveCount(2);
        result.Items[0].Attributes!.Name.Should().Be("Acme Inc.");
        result.Items[1].Attributes!.Currency.Should().Be("EUR");

        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/stores");
    }

    [Fact]
    public async Task ListAsync_serialises_page_and_include_into_query_string()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, StoreListPayload);

        _ = await client.ListAsync(new ListFilter
        {
            PageNumber = 2,
            PageSize = 20,
            Include = "products,subscriptions",
        });

        var query = handler.Requests[0].RequestUri!.Query;
        query.Should().Contain("page%5Bnumber%5D=2");
        query.Should().Contain("page%5Bsize%5D=20");
        query.Should().Contain("include=products%2Csubscriptions");
    }

    [Fact]
    public async Task ListAsync_exposes_paging_cursors_from_envelope()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, StoreListPayload);

        var result = await client.ListAsync();

        result.HasNext.Should().BeTrue();
        result.HasPrevious.Should().BeFalse();
        result.Meta!.Page!.Total.Should().Be(5);
        result.Meta.Page.LastPage.Should().Be(3);
    }
}
