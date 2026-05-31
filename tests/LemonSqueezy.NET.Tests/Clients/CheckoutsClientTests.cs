using System.Net;
using System.Text.Json;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.Checkouts;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class CheckoutsClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "ckt_abc123",
            "type": "checkouts",
            "attributes": {
              "store_id": 1,
              "variant_id": 10,
              "custom_price": 1999,
              "product_options": {
                "name": "Pro Plan",
                "description": "Use it now",
                "redirect_url": "https://example.com/thanks",
                "receipt_button_text": "Go to dashboard",
                "enabled_variants": [10, 11]
              },
              "checkout_options": {
                "embed": true,
                "dark": true,
                "button_color": "#7047EB"
              },
              "checkout_data": {
                "email": "ada@example.com",
                "name": "Ada Lovelace",
                "discount_code": "WELCOME10",
                "custom": { "user_id": "42" }
              },
              "preview": false,
              "expires_at": null,
              "url": "https://acme.lemonsqueezy.com/checkout/buy/abc123",
              "test_mode": false,
              "created_at": "2024-06-01T00:00:00Z",
              "updated_at": "2024-06-01T00:00:00Z"
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "ckt_1", "type": "checkouts", "attributes": { "url": "https://example.com/c1" } },
            { "id": "ckt_2", "type": "checkouts", "attributes": { "url": "https://example.com/c2" } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 4 } }
        }
        """;

    private static (CheckoutsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new CheckoutsClient(httpClient), handler);
    }

    [Fact]
    public async Task CreateAsync_posts_jsonapi_envelope_with_store_and_variant_relationships()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.Created, SinglePayload);

        var result = await client.CreateAsync(new CreateCheckoutRequest
        {
            StoreId = "1",
            VariantId = "10",
            CustomPrice = 1999,
            ProductOptions = new CheckoutProductOptions
            {
                Name = "Pro Plan",
                RedirectUrl = "https://example.com/thanks",
                EnabledVariants = new List<long> { 10, 11 },
            },
            CheckoutOptions = new CheckoutOptions
            {
                Embed = true,
                Dark = true,
                ButtonColor = "#7047EB",
            },
            CheckoutData = new CheckoutData
            {
                Email = "ada@example.com",
                Name = "Ada Lovelace",
                DiscountCode = "WELCOME10",
            },
        });

        result.Data!.Id.Should().Be("ckt_abc123");
        result.Data.Attributes!.Url.Should().StartWith("https://acme.lemonsqueezy.com/checkout/buy/");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("checkouts");
        data.TryGetProperty("id", out _).Should().BeFalse("POST must not include an id");

        var attrs = data.GetProperty("attributes");
        attrs.GetProperty("custom_price").GetInt64().Should().Be(1999);
        attrs.GetProperty("product_options").GetProperty("name").GetString().Should().Be("Pro Plan");
        attrs.GetProperty("product_options").GetProperty("redirect_url").GetString().Should().Be("https://example.com/thanks");
        attrs.GetProperty("checkout_options").GetProperty("button_color").GetString().Should().Be("#7047EB");
        attrs.GetProperty("checkout_options").GetProperty("dark").GetBoolean().Should().BeTrue();
        attrs.GetProperty("checkout_data").GetProperty("email").GetString().Should().Be("ada@example.com");
        attrs.GetProperty("checkout_data").GetProperty("discount_code").GetString().Should().Be("WELCOME10");
        attrs.TryGetProperty("preview", out _).Should().BeFalse("nulls must not serialise");

        var rels = data.GetProperty("relationships");
        rels.GetProperty("store").GetProperty("data").GetProperty("id").GetString().Should().Be("1");
        rels.GetProperty("variant").GetProperty("data").GetProperty("id").GetString().Should().Be("10");

        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/checkouts");
    }

    [Theory]
    [InlineData("", "10")]
    [InlineData("1", "")]
    public async Task CreateAsync_rejects_missing_required_relationships(string storeId, string variantId)
    {
        var (client, _) = CreateSut();

        var act = () => client.CreateAsync(new CreateCheckoutRequest
        {
            StoreId = storeId,
            VariantId = variantId,
        });

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("request");
    }

    [Fact]
    public async Task GetAsync_returns_checkout_by_id_with_url()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("ckt_abc123");

        result.Data!.Attributes!.Url.Should().Be("https://acme.lemonsqueezy.com/checkout/buy/abc123");
        result.Data.Attributes.CustomPrice.Should().Be(1999);
        result.Data.Attributes.CheckoutData!.Email.Should().Be("ada@example.com");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/checkouts/ckt_abc123");
    }

    [Fact]
    public async Task ListAsync_serialises_store_and_variant_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new CheckoutListFilter
        {
            StoreId = "1",
            VariantId = "10",
        });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Bstore_id%5D=1");
        q.Should().Contain("filter%5Bvariant_id%5D=10");
    }
}
