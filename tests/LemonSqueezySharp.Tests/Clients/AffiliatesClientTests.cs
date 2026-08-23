using System.Net;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Models.Affiliates;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class AffiliatesClientTests
{
    private const string SinglePayload = """
        {
          "data": {
            "type": "affiliates",
            "id": "1",
            "attributes": {
              "store_id": 1,
              "user_id": 2,
              "user_name": "John Doe",
              "user_email": "johndoe@example.com",
              "share_domain": "example.com",
              "status": "active",
              "products": null,
              "application_note": "I'm a digital content creator and marketer promoting B2B solutions via my newsletter",
              "total_earnings": 169550,
              "unpaid_earnings": 132000,
              "created_at": "2023-11-22T04:12:50.000000Z",
              "updated_at": "2023-11-22T04:12:50.000000Z"
            }
          }
        }
        """;

    private const string ListPayload = """
        {
          "data": [
            { "id": "1", "type": "affiliates", "attributes": { "user_name": "Alice", "share_domain": "alice.example.com" } },
            { "id": "2", "type": "affiliates", "attributes": { "user_name": "Bob",   "share_domain": "bob.example.com"   } }
          ],
          "meta": { "page": { "number": 1, "size": 2, "total": 5 } }
        }
        """;

    private static (AffiliatesClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new AffiliatesClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_affiliate()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SinglePayload);

        var result = await client.GetAsync("1");

        result.Data!.Id.Should().Be("1");
        result.Data.Type.Should().Be("affiliates");
        result.Data.Attributes!.StoreId.Should().Be(1);
        result.Data.Attributes.UserId.Should().Be(2);
        result.Data.Attributes.UserName.Should().Be("John Doe");
        result.Data.Attributes.UserEmail.Should().Be("johndoe@example.com");
        result.Data.Attributes.ShareDomain.Should().Be("example.com");
        result.Data.Attributes.Status.Should().Be("active");
        result.Data.Attributes.Products.Should().BeNull();
        result.Data.Attributes.ApplicationNote.Should().StartWith("I'm a digital content creator");
        result.Data.Attributes.TotalEarnings.Should().Be(169550);
        result.Data.Attributes.UnpaidEarnings.Should().Be(132000);
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/affiliates/1");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_store_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        _ = await client.ListAsync(new AffiliateListFilter { StoreId = "1" });

        handler.Requests[0].RequestUri!.Query.Should().Contain("filter%5Bstore_id%5D=1");
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ListPayload);

        var result = await client.ListAsync();

        result.Items.Should().HaveCount(2);
        result.Items[0].Attributes!.UserName.Should().Be("Alice");
        result.Items[0].Attributes.ShareDomain.Should().Be("alice.example.com");
        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/affiliates");
    }
}
