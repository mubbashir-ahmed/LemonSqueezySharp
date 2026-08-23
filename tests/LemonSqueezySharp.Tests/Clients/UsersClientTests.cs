using System.Net;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Exceptions;
using LemonSqueezySharp.Tests.TestHelpers;

namespace LemonSqueezySharp.Tests.Clients;

public class UsersClientTests
{
    private const string MePayload = """
        {
          "data": {
            "id": "1",
            "type": "users",
            "attributes": {
              "name": "Ada Lovelace",
              "email": "ada@example.com",
              "color": "#abcdef",
              "avatar_url": "https://example.com/avatar.png",
              "has_custom_avatar": true,
              "created_at": "2024-01-02T03:04:05Z",
              "updated_at": "2024-02-03T04:05:06Z"
            },
            "links": {
              "self": "https://api.lemonsqueezy.com/v1/users/1"
            }
          }
        }
        """;

    private static (UsersClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new UsersClient(httpClient), handler);
    }

    [Fact]
    public async Task GetMeAsync_returns_authenticated_user()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, MePayload);

        var result = await client.GetMeAsync();

        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be("1");
        result.Data.Type.Should().Be("users");
        result.Data.Attributes.Should().NotBeNull();
        result.Data.Attributes!.Email.Should().Be("ada@example.com");
        result.Data.Attributes.Name.Should().Be("Ada Lovelace");
        result.Data.Attributes.HasCustomAvatar.Should().BeTrue();
        result.Data.Links!.Self.Should().Be("https://api.lemonsqueezy.com/v1/users/1");

        handler.Requests.Should().ContainSingle();
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/users/me");
        handler.Requests[0].Method.Should().Be(HttpMethod.Get);
    }

    [Fact]
    public async Task GetMeAsync_translates_401_to_AuthenticationException()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(
            HttpStatusCode.Unauthorized,
            """{"errors":[{"status":"401","title":"Unauthorized","detail":"Invalid API key"}]}""");

        var act = () => client.GetMeAsync();

        await act.Should().ThrowAsync<LemonSqueezyAuthenticationException>()
            .Where(ex => ex.Message.Contains("Invalid API key"));
    }

    [Fact]
    public async Task GetMeAsync_translates_429_to_RateLimitException()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueStatus((HttpStatusCode)429, """{"errors":[{"title":"Too Many Requests"}]}""");

        var act = () => client.GetMeAsync();

        var ex = (await act.Should().ThrowAsync<LemonSqueezyRateLimitException>()).Which;
        ex.StatusCode.Should().Be((HttpStatusCode)429);
    }
}
