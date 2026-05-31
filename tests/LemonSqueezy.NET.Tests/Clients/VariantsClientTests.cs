using System.Net;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Models.Variants;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class VariantsClientTests
{
    private const string SingleVariantPayload = """
        {
          "data": {
            "id": "10",
            "type": "variants",
            "attributes": {
              "product_id": 1,
              "name": "Monthly",
              "slug": "monthly",
              "description": "<p>Billed monthly.</p>",
              "has_license_keys": true,
              "license_activation_limit": 3,
              "is_license_limit_unlimited": false,
              "sort": 1,
              "status": "published",
              "status_formatted": "Published",
              "test_mode": false,
              "created_at": "2024-01-01T00:00:00Z",
              "updated_at": "2024-02-01T00:00:00Z"
            }
          }
        }
        """;

    private const string VariantListPayload = """
        {
          "data": [
            { "id": "1", "type": "variants", "attributes": { "name": "Monthly" } },
            { "id": "2", "type": "variants", "attributes": { "name": "Yearly"  } }
          ],
          "links": { "next": "https://api.lemonsqueezy.com/v1/variants?page[number]=2" },
          "meta": { "page": { "number": 1, "size": 2, "total": 4, "lastPage": 2 } }
        }
        """;

    private static (VariantsClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new VariantsClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_variant_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleVariantPayload);

        var result = await client.GetAsync("10");

        result.Data!.Id.Should().Be("10");
        result.Data.Attributes!.Name.Should().Be("Monthly");
        result.Data.Attributes.ProductId.Should().Be(1);
        result.Data.Attributes.HasLicenseKeys.Should().BeTrue();
        result.Data.Attributes.LicenseActivationLimit.Should().Be(3);
        result.Data.Attributes.Status.Should().Be("published");
        result.Data.Attributes.StatusFormatted.Should().Be("Published");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/variants/10");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_variants_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, VariantListPayload);

        var result = await client.ListAsync();

        result.Items.Should().HaveCount(2);
        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/variants");
    }

    [Fact]
    public async Task ListAsync_serialises_paging_product_and_status_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, VariantListPayload);

        _ = await client.ListAsync(new VariantListFilter
        {
            PageNumber = 2,
            PageSize = 50,
            ProductId = "1",
            Status = "published",
        });

        var query = handler.Requests[0].RequestUri!.Query;
        query.Should().Contain("page%5Bnumber%5D=2");
        query.Should().Contain("page%5Bsize%5D=50");
        query.Should().Contain("filter%5Bproduct_id%5D=1");
        query.Should().Contain("filter%5Bstatus%5D=published");
    }
}
