using System.Net;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.LicenseKeyInstances;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class LicenseKeyInstancesClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "id": "888",
            "type": "license-key-instances",
            "attributes": {
              "license_key_id": 5000,
              "identifier": "0b2b50d2-1234-4567-89ab-cdef01234567",
              "name": "John's MacBook Pro",
              "created_at": "2024-06-01T00:00:00Z"
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "license-key-instances", "attributes": { "name": "Desktop" } },
            { "id": "2", "type": "license-key-instances", "attributes": { "name": "Laptop"  } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 3 } }
        }
        """;

    private static (LicenseKeyInstancesClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new LicenseKeyInstancesClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_license_key_instance()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("888");

        result.Data!.Attributes!.LicenseKeyId.Should().Be(5000);
        result.Data.Attributes.Name.Should().Be("John's MacBook Pro");
        result.Data.Attributes.Identifier.Should().Be("0b2b50d2-1234-4567-89ab-cdef01234567");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/license-key-instances/888");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_license_key_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new LicenseKeyInstanceListFilter
        {
            LicenseKeyId = "5000",
            PageSize = 25,
        });

        var q = handler.Requests[0].RequestUri!.Query;
        q.Should().Contain("filter%5Blicense_key_id%5D=5000");
        q.Should().Contain("page%5Bsize%5D=25");
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync();

        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/license-key-instances");
    }
}
