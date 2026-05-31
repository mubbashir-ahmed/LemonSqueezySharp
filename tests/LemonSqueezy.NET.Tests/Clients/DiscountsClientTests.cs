using System.Net;
using System.Text.Json;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.Discounts;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class DiscountsClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "77",
            "type": "discounts",
            "attributes": {
              "store_id": 1,
              "name": "Black Friday",
              "code": "BLACKFRIDAY",
              "amount": 25,
              "amount_type": "percent",
              "is_limited_to_products": false,
              "is_limited_redemptions": true,
              "max_redemptions": 100,
              "duration": "once",
              "status": "published",
              "status_formatted": "Published",
              "test_mode": false
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "discounts", "attributes": { "code": "WELCOME10", "amount": 10 } },
            { "id": "2", "type": "discounts", "attributes": { "code": "VIP20", "amount": 20 } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 5 } }
        }
        """;

    private static (DiscountsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new DiscountsClient(httpClient), handler);
    }

    [Fact]
    public async Task CreateAsync_posts_jsonapi_envelope_with_store_relationship()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.Created, SinglePayload);

        var result = await client.CreateAsync(new CreateDiscountRequest
        {
            Name = "Black Friday",
            Code = "BLACKFRIDAY",
            Amount = 25,
            AmountType = "percent",
            StoreId = "1",
            Duration = "once",
            IsLimitedRedemptions = true,
            MaxRedemptions = 100,
        });

        result.Data!.Id.Should().Be("77");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("discounts");
        var attrs = data.GetProperty("attributes");
        attrs.GetProperty("name").GetString().Should().Be("Black Friday");
        attrs.GetProperty("code").GetString().Should().Be("BLACKFRIDAY");
        attrs.GetProperty("amount").GetInt64().Should().Be(25);
        attrs.GetProperty("amount_type").GetString().Should().Be("percent");
        attrs.GetProperty("duration").GetString().Should().Be("once");
        attrs.GetProperty("max_redemptions").GetInt32().Should().Be(100);

        var store = data.GetProperty("relationships").GetProperty("store").GetProperty("data");
        store.GetProperty("type").GetString().Should().Be("stores");
        store.GetProperty("id").GetString().Should().Be("1");

        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/discounts");
    }

    [Theory]
    [InlineData("", "CODE", "percent", "1")]
    [InlineData("Name", "", "percent", "1")]
    [InlineData("Name", "CODE", "", "1")]
    [InlineData("Name", "CODE", "percent", "")]
    public async Task CreateAsync_rejects_missing_required_fields(
        string name, string code, string amountType, string storeId)
    {
        var (client, _) = CreateSut();

        var act = () => client.CreateAsync(new CreateDiscountRequest
        {
            Name = name,
            Code = code,
            Amount = 10,
            AmountType = amountType,
            StoreId = storeId,
        });

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("request");
    }

    [Fact]
    public async Task GetAsync_returns_discount_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("77");

        result.Data!.Attributes!.Code.Should().Be("BLACKFRIDAY");
        result.Data.Attributes.AmountType.Should().Be("percent");
        result.Data.Attributes.MaxRedemptions.Should().Be(100);
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/discounts/77");
    }

    [Fact]
    public async Task DeleteAsync_sends_DELETE_and_completes_on_204_no_content()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueStatus(HttpStatusCode.NoContent);

        await client.DeleteAsync("77");

        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Delete);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/discounts/77");
    }

    [Fact]
    public async Task DeleteAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.DeleteAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_store_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new DiscountListFilter { StoreId = "1" });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Bstore_id%5D=1");
    }
}
