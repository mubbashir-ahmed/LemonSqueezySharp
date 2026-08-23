using System.Net;
using System.Text.Json;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Models.LicenseKeys;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class LicenseKeysClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "5000",
            "type": "license-keys",
            "attributes": {
              "store_id": 1,
              "customer_id": 1,
              "order_id": 500,
              "order_item_id": 900,
              "product_id": 100,
              "user_email": "johndoe@example.com",
              "key": "BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE",
              "key_short": "0BCE",
              "activation_limit": 3,
              "instances_count": 1,
              "disabled": false,
              "status": "active",
              "status_formatted": "Active",
              "expires_at": "2025-01-01T00:00:00Z"
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "license-keys", "attributes": { "status": "active",   "key_short": "AAAA" } },
            { "id": "2", "type": "license-keys", "attributes": { "status": "expired",  "key_short": "BBBB" } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 12 } }
        }
        """;

    private static (LicenseKeysClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new LicenseKeysClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_license_key_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("5000");

        result.Data!.Attributes!.Key.Should().StartWith("BBA8B40F");
        result.Data.Attributes.KeyShort.Should().Be("0BCE");
        result.Data.Attributes.ActivationLimit.Should().Be(3);
        result.Data.Attributes.InstancesCount.Should().Be(1);
        result.Data.Attributes.Status.Should().Be("active");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/license-keys/5000");
    }

    [Fact]
    public async Task UpdateAsync_patches_with_id_in_body_and_only_set_fields()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        _ = await client.UpdateAsync("5000", new UpdateLicenseKeyRequest
        {
            ActivationLimit = 10,
            Disabled = false,
        });

        var request = handler.Requests[0];
        request.Method.Method.Should().Be("PATCH");
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/license-keys/5000");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("license-keys");
        data.GetProperty("id").GetString().Should().Be("5000");
        var attrs = data.GetProperty("attributes");
        attrs.GetProperty("activation_limit").GetInt32().Should().Be(10);
        attrs.GetProperty("disabled").GetBoolean().Should().BeFalse();
        attrs.TryGetProperty("expires_at", out _).Should().BeFalse("nulls must not serialise");
    }

    [Fact]
    public async Task UpdateAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.UpdateAsync("", new UpdateLicenseKeyRequest());

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_store_product_and_status_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new LicenseKeyListFilter
        {
            StoreId = "1",
            ProductId = "100",
            Status = "active",
        });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Bstore_id%5D=1");
        q.Should().Contain("filter%5Bproduct_id%5D=100");
        q.Should().Contain("filter%5Bstatus%5D=active");
    }
}
