using System.Net;
using System.Text.Json;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Models.SubscriptionInvoices;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class SubscriptionInvoicesClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "55",
            "type": "subscription-invoices",
            "attributes": {
              "store_id": 1,
              "subscription_id": 1,
              "customer_id": 1,
              "user_email": "johndoe@example.com",
              "billing_reason": "renewal",
              "card_brand": "visa",
              "card_last_four": "4242",
              "currency": "USD",
              "status": "paid",
              "status_formatted": "Paid",
              "refunded": false,
              "subtotal": 999,
              "tax": 200,
              "total": 1199,
              "total_formatted": "$11.99",
              "urls": { "invoice_url": "https://files.lemonsqueezy.com/inv/55.pdf" },
              "test_mode": false
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "subscription-invoices", "attributes": { "status": "paid", "total": 100 } },
            { "id": "2", "type": "subscription-invoices", "attributes": { "status": "refunded", "total": 200 } }
          ],
          "links": { "next": "https://api.lemonsqueezy.com/v1/subscription-invoices?page[number]=2" },
          "meta": { "page": { "number": 1, "size": 2, "total": 5 } }
        }
        """;

    private const string InvoiceUrlPayload = """
        { "data": { "invoice_url": "https://files.lemonsqueezy.com/inv/55.pdf" } }
        """;

    private static (SubscriptionInvoicesClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new SubscriptionInvoicesClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_subscription_invoice_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("55");

        result.Data!.Attributes!.BillingReason.Should().Be("renewal");
        result.Data.Attributes.TotalFormatted.Should().Be("$11.99");
        result.Data.Attributes.Urls!.InvoiceUrl.Should().EndWith("/55.pdf");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/subscription-invoices/55");
    }

    [Fact]
    public async Task ListAsync_serialises_subscription_status_and_refunded_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new SubscriptionInvoiceListFilter
        {
            SubscriptionId = "1",
            Status = "paid",
            Refunded = false,
        });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Bsubscription_id%5D=1");
        q.Should().Contain("filter%5Bstatus%5D=paid");
        q.Should().Contain("filter%5Brefunded%5D=false");
    }

    [Fact]
    public async Task GenerateInvoiceAsync_appends_locale_to_query_string()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, InvoiceUrlPayload);

        var result = await client.GenerateInvoiceAsync("55", new GenerateSubscriptionInvoiceRequest
        {
            Country = "US",
            Locale = "en",
        });

        result.Data!.InvoiceUrl.Should().EndWith("/55.pdf");

        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/subscription-invoices/55/generate-invoice");
        request.RequestUri.Query.Should().Contain("locale=en");
        request.RequestUri.Query.Should().Contain("country=US");
    }

    [Fact]
    public async Task RefundAsync_posts_jsonapi_envelope_with_amount()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        _ = await client.RefundAsync("55", new RefundSubscriptionInvoiceRequest { Amount = 500 });

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("subscription-invoices");
        data.GetProperty("id").GetString().Should().Be("55");
        data.GetProperty("attributes").GetProperty("amount").GetInt64().Should().Be(500);
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/subscription-invoices/55/refund");
    }
}
