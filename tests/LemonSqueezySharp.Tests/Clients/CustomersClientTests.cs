using System.Net;
using System.Text.Json;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Exceptions;
using LemonSqueezySharp.Models.Customers;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class CustomersClientTests
{
    private const string CustomerPayload = """
        {
          "data": {
            "id": "42",
            "type": "customers",
            "attributes": {
              "store_id": 1,
              "name": "Ada Lovelace",
              "email": "ada@example.com",
              "status": "subscribed",
              "city": "London",
              "region": "England",
              "country": "GB",
              "test_mode": false,
              "created_at": "2024-01-02T03:04:05Z",
              "updated_at": "2024-02-03T04:05:06Z",
              "urls": { "customer_portal": "https://acme.lemonsqueezy.com/billing" }
            }
          }
        }
        """;

    private const string CustomerListPayload = """
        {
          "data": [
            { "id": "1", "type": "customers", "attributes": { "name": "Alice", "email": "alice@example.com" } },
            { "id": "2", "type": "customers", "attributes": { "name": "Bob",   "email": "bob@example.com" } }
          ],
          "links": {
            "first": "https://api.lemonsqueezy.com/v1/customers?page[number]=1",
            "next":  "https://api.lemonsqueezy.com/v1/customers?page[number]=2"
          },
          "meta": { "page": { "number": 1, "size": 2, "total": 25, "lastPage": 13 } }
        }
        """;

    private static (CustomersClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new CustomersClient(httpClient), handler);
    }

    [Fact]
    public async Task CreateAsync_posts_jsonapi_envelope_with_store_relationship()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.Created, CustomerPayload);

        var result = await client.CreateAsync(new CreateCustomerRequest
        {
            Name = "Ada Lovelace",
            Email = "ada@example.com",
            StoreId = "1",
            City = "London",
            Country = "GB",
        });

        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be("42");
        result.Data.Attributes!.Email.Should().Be("ada@example.com");

        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/customers");
        request.Content!.Headers.ContentType!.MediaType.Should().Be("application/vnd.api+json");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("customers");
        data.TryGetProperty("id", out _).Should().BeFalse("POST must not include an id");
        data.GetProperty("attributes").GetProperty("name").GetString().Should().Be("Ada Lovelace");
        data.GetProperty("attributes").GetProperty("email").GetString().Should().Be("ada@example.com");
        data.GetProperty("attributes").GetProperty("city").GetString().Should().Be("London");
        data.GetProperty("attributes").TryGetProperty("region", out _).Should().BeFalse("nulls must not serialise");

        var store = data.GetProperty("relationships").GetProperty("store").GetProperty("data");
        store.GetProperty("type").GetString().Should().Be("stores");
        store.GetProperty("id").GetString().Should().Be("1");
    }

    [Theory]
    [InlineData("", "ada@example.com", "1")]
    [InlineData("Ada", "", "1")]
    [InlineData("Ada", "ada@example.com", "")]
    public async Task CreateAsync_rejects_missing_required_fields(string name, string email, string storeId)
    {
        var (client, _) = CreateSut();

        var act = () => client.CreateAsync(new CreateCustomerRequest
        {
            Name = name,
            Email = email,
            StoreId = storeId,
        });

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("request");
    }

    [Fact]
    public async Task GetAsync_returns_customer_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, CustomerPayload);

        var result = await client.GetAsync("42");

        result.Data!.Attributes!.Name.Should().Be("Ada Lovelace");
        result.Data.Attributes.Urls!.CustomerPortal.Should().Be("https://acme.lemonsqueezy.com/billing");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/customers/42");
        handler.Requests[0].Method.Should().Be(HttpMethod.Get);
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("   ");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task UpdateAsync_patches_with_id_in_body_and_only_changed_fields()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, CustomerPayload);

        _ = await client.UpdateAsync("42", new UpdateCustomerRequest
        {
            Name = "Ada Byron",
            Status = "archived",
        });

        var request = handler.Requests[0];
        request.Method.Method.Should().Be("PATCH");
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/customers/42");
        request.Content!.Headers.ContentType!.MediaType.Should().Be("application/vnd.api+json");

        var sent = JsonDocument.Parse(handler.RequestBodies[0]!).RootElement;
        var data = sent.GetProperty("data");
        data.GetProperty("type").GetString().Should().Be("customers");
        data.GetProperty("id").GetString().Should().Be("42");
        var attrs = data.GetProperty("attributes");
        attrs.GetProperty("name").GetString().Should().Be("Ada Byron");
        attrs.GetProperty("status").GetString().Should().Be("archived");
        attrs.TryGetProperty("email", out _).Should().BeFalse("untouched fields must not be sent");
        attrs.TryGetProperty("country", out _).Should().BeFalse("untouched fields must not be sent");
    }

    [Fact]
    public async Task UpdateAsync_translates_422_to_ValidationException()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(
            (HttpStatusCode)422,
            """{"errors":[{"status":"422","title":"Unprocessable Entity","detail":"email must be valid"}]}""");

        var act = () => client.UpdateAsync("42", new UpdateCustomerRequest { Email = "not-an-email" });

        var ex = (await act.Should().ThrowAsync<LemonSqueezyValidationException>()).Which;
        ex.Errors.Should().ContainSingle().Which.Should().Contain("email must be valid");
    }

    [Fact]
    public async Task ListAsync_serialises_paging_and_resource_specific_filters()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, CustomerListPayload);

        var result = await client.ListAsync(new CustomerListFilter
        {
            PageNumber = 2,
            PageSize = 25,
            StoreId = "7",
            Email = "alice@example.com",
            Include = "subscriptions",
        });

        result.Items.Should().HaveCount(2);
        result.HasNext.Should().BeTrue();
        result.Meta!.Page!.Total.Should().Be(25);

        var query = handler.Requests[0].RequestUri!.Query;
        query.Should().Contain("page%5Bnumber%5D=2");
        query.Should().Contain("page%5Bsize%5D=25");
        query.Should().Contain("filter%5Bstore_id%5D=7");
        query.Should().Contain("filter%5Bemail%5D=alice%40example.com");
        query.Should().Contain("include=subscriptions");
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_customers_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, CustomerListPayload);

        _ = await client.ListAsync();

        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/customers");
    }
}
