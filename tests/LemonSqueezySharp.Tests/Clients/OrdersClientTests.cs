using System.Net;
using System.Text.Json;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Models.Orders;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class OrdersClientTests
{
    private const string SingleOrderPayload = """
        {
          "data": {
            "type": "orders",
            "id": "1",
            "attributes": {
              "store_id": 1,
              "customer_id": 1,
              "identifier": "104e18a2-d755-4d4b-80c4-a6c1dcbe1c10",
              "order_number": 1,
              "user_name": "John Doe",
              "user_email": "johndoe@example.com",
              "currency": "USD",
              "currency_rate": "1.0000",
              "subtotal": 999,
              "setup_fee": 999,
              "discount_total": 0,
              "tax": 200,
              "total": 1199,
              "subtotal_usd": 999,
              "setup_fee_usd": 999,
              "discount_total_usd": 0,
              "tax_usd": 200,
              "total_usd": 1199,
              "tax_name": "VAT",
              "tax_rate": "20.00",
              "tax_inclusive": false,
              "status": "paid",
              "status_formatted": "Paid",
              "refunded": false,
              "refunded_at": null,
              "subtotal_formatted": "$9.99",
              "setup_fee_formatted": "$9.99",
              "discount_total_formatted": "$0.00",
              "tax_formatted": "$2.00",
              "total_formatted": "$11.99",
              "first_order_item": {
                "id": 1,
                "order_id": 1,
                "product_id": 1,
                "variant_id": 1,
                "product_name": "Test Limited License for 2 years",
                "variant_name": "Default",
                "price": 1199,
                "created_at": "2021-08-17T09:45:53.000000Z",
                "updated_at": "2021-08-17T09:45:53.000000Z",
                "test_mode": false
              },
              "urls": {
                "receipt": "https://app.lemonsqueezy.com/my-orders/104e18a2-d755-4d4b-80c4-a6c1dcbe1c10?signature=abcdef"
              },
              "created_at": "2021-08-17T09:45:53.000000Z",
              "updated_at": "2021-08-17T09:45:53.000000Z",
              "test_mode": false
            }
          }
        }
        """;

    private const string OrderListPayload = """
        {
          "data": [
            { "id": "1", "type": "orders", "attributes": { "user_email": "a@example.com", "total": 100 } },
            { "id": "2", "type": "orders", "attributes": { "user_email": "b@example.com", "total": 200 } }
          ],
          "links": { "next": "https://api.lemonsqueezy.com/v1/orders?page[number]=2" },
          "meta": { "page": { "number": 1, "size": 2, "total": 12 } }
        }
        """;

    private const string InvoicePayload = """
        { "data": { "invoice_url": "https://files.lemonsqueezy.com/invoices/1.pdf" } }
        """;

    private static (OrdersClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new OrdersClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_order_with_first_item_and_urls()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleOrderPayload);

        var result = await client.GetAsync("1");

        result.Data!.Id.Should().Be("1");
        result.Data.Attributes!.UserName.Should().Be("John Doe");
        result.Data.Attributes.UserEmail.Should().Be("johndoe@example.com");
        result.Data.Attributes.Status.Should().Be("paid");
        result.Data.Attributes.Currency.Should().Be("USD");
        result.Data.Attributes.CurrencyRate.Should().Be("1.0000");
        result.Data.Attributes.Subtotal.Should().Be(999);
        result.Data.Attributes.SetupFee.Should().Be(999);
        result.Data.Attributes.SetupFeeFormatted.Should().Be("$9.99");
        result.Data.Attributes.SetupFeeUsd.Should().Be(999);
        result.Data.Attributes.Tax.Should().Be(200);
        result.Data.Attributes.TaxInclusive.Should().BeFalse();
        result.Data.Attributes.Total.Should().Be(1199);
        result.Data.Attributes.TotalFormatted.Should().Be("$11.99");
        result.Data.Attributes.FirstOrderItem!.ProductName.Should().Be("Test Limited License for 2 years");
        result.Data.Attributes.FirstOrderItem.VariantName.Should().Be("Default");
        result.Data.Attributes.FirstOrderItem.Price.Should().Be(1199);
        result.Data.Attributes.Urls!.Receipt.Should().StartWith("https://app.lemonsqueezy.com/my-orders/");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/orders/1");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_store_and_email_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, OrderListPayload);

        _ = await client.ListAsync(new OrderListFilter
        {
            StoreId = "1",
            UserEmail = "ada@example.com",
            PageNumber = 3,
        });

        var query = handler.Requests[0].RequestUri!.Query;
        query.Should().Contain("filter%5Bstore_id%5D=1");
        query.Should().Contain("filter%5Buser_email%5D=ada%40example.com");
        query.Should().Contain("page%5Bnumber%5D=3");
    }

    [Fact]
    public async Task GenerateInvoiceAsync_posts_to_invoice_endpoint_with_address_query_params()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, InvoicePayload);

        var result = await client.GenerateInvoiceAsync("1", new GenerateOrderInvoiceRequest
        {
            Name = "Acme Inc.",
            Country = "US",
            ZipCode = "94105",
            Locale = "en",
        });

        result.Data!.InvoiceUrl.Should().Be("https://files.lemonsqueezy.com/invoices/1.pdf");

        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/orders/1/generate-invoice");
        var query = request.RequestUri.Query;
        query.Should().Contain("name=Acme%20Inc.");
        query.Should().Contain("country=US");
        query.Should().Contain("zip_code=94105");
        query.Should().Contain("locale=en");
        query.Should().NotContain("address");
    }

    [Fact]
    public async Task GenerateInvoiceAsync_without_request_hits_bare_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, InvoicePayload);

        _ = await client.GenerateInvoiceAsync("1");

        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/orders/1/generate-invoice");
    }

    [Fact]
    public async Task RefundAsync_posts_jsonapi_envelope_with_amount()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleOrderPayload);

        var result = await client.RefundAsync("1", new RefundOrderRequest { Amount = 1000 });

        result.Data!.Id.Should().Be("1");

        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/orders/1/refund");
        request.Content!.Headers.ContentType!.MediaType.Should().Be("application/vnd.api+json");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("orders");
        data.GetProperty("id").GetString().Should().Be("1");
        data.GetProperty("attributes").GetProperty("amount").GetInt64().Should().Be(1000);
    }

    [Fact]
    public async Task RefundAsync_without_amount_still_posts_envelope()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleOrderPayload);

        _ = await client.RefundAsync("1");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var attrs = sent.GetProperty("data").GetProperty("attributes");
        attrs.TryGetProperty("amount", out _).Should().BeFalse("nulls must not be sent");
    }

    [Fact]
    public async Task RefundAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.RefundAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }
}
