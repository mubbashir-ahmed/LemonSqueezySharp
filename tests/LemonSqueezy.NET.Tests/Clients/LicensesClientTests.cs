using System.Net;
using System.Text.RegularExpressions;
using LemonSqueezy.NET.Clients;
using LemonSqueezy.NET.Tests.TestHelpers;

namespace LemonSqueezy.NET.Tests.Clients;

public class LicensesClientTests
{
    private const string ActivatePayload = """
        {
          "activated": true,
          "error": null,
          "license_key": {
            "id": 5000,
            "status": "active",
            "key": "BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE",
            "activation_limit": 3,
            "activation_usage": 1,
            "test_mode": false
          },
          "instance": {
            "id": "0b2b50d2-1234-4567-89ab-cdef01234567",
            "name": "John's MacBook Pro",
            "created_at": "2024-06-01T00:00:00Z"
          },
          "meta": {
            "store_id": 1,
            "order_id": 500,
            "product_id": 100,
            "product_name": "Pro Plan",
            "customer_email": "johndoe@example.com"
          }
        }
        """;

    private const string ValidatePayload = """
        {
          "valid": true,
          "error": null,
          "license_key": { "id": 5000, "status": "active", "activation_limit": 3, "activation_usage": 1 },
          "instance": { "id": "0b2b50d2-1234-4567-89ab-cdef01234567", "name": "Desktop" },
          "meta": { "store_id": 1 }
        }
        """;

    private const string DeactivatePayload = """
        {
          "deactivated": true,
          "error": null,
          "license_key": { "id": 5000, "status": "active", "activation_usage": 0 }
        }
        """;

    private static (LicensesClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new LicensesClient(httpClient), handler);
    }

    [Fact]
    public async Task ActivateAsync_posts_form_with_license_key_and_instance_name()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ActivatePayload);

        var result = await client.ActivateAsync("BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE", "John's MacBook Pro");

        result.Should().NotBeNull();
        result!.Activated.Should().BeTrue();
        result.LicenseKey!.Status.Should().Be("active");
        result.LicenseKey.ActivationUsage.Should().Be(1);
        result.Instance!.Id.Should().Be("0b2b50d2-1234-4567-89ab-cdef01234567");
        result.Meta!.ProductName.Should().Be("Pro Plan");

        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri!.AbsolutePath.Should().EndWith("/v1/licenses/activate");
        request.Content!.Headers.ContentType!.MediaType.Should().Be("application/x-www-form-urlencoded");

        var body = handler.RequestBodies[0]!;
        body.Should().Contain("license_key=BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE");
        // Spaces and apostrophes get encoded in form bodies — match flexibly.
        Regex.IsMatch(body, "instance_name=John(%27|')s\\+MacBook\\+Pro").Should().BeTrue();
    }

    [Fact]
    public async Task ValidateAsync_includes_instance_id_when_provided()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ValidatePayload);

        var result = await client.ValidateAsync(
            "BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE",
            "0b2b50d2-1234-4567-89ab-cdef01234567");

        result!.Valid.Should().BeTrue();
        result.Instance!.Name.Should().Be("Desktop");

        var body = handler.RequestBodies[0]!;
        body.Should().Contain("license_key=BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE");
        body.Should().Contain("instance_id=0b2b50d2-1234-4567-89ab-cdef01234567");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/licenses/validate");
    }

    [Fact]
    public async Task ValidateAsync_omits_instance_id_when_not_supplied()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, ValidatePayload);

        _ = await client.ValidateAsync("BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE");

        var body = handler.RequestBodies[0]!;
        body.Should().Contain("license_key=BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE");
        body.Should().NotContain("instance_id");
    }

    [Fact]
    public async Task DeactivateAsync_posts_form_with_license_key_and_instance_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, DeactivatePayload);

        var result = await client.DeactivateAsync(
            "BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE",
            "0b2b50d2-1234-4567-89ab-cdef01234567");

        result!.Deactivated.Should().BeTrue();
        result.LicenseKey!.ActivationUsage.Should().Be(0);

        var body = handler.RequestBodies[0]!;
        body.Should().Contain("license_key=BBA8B40F-EFFA-4C25-9F92-9C0F2A1A0BCE");
        body.Should().Contain("instance_id=0b2b50d2-1234-4567-89ab-cdef01234567");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/licenses/deactivate");
    }

    [Theory]
    [InlineData("", "Desktop")]
    [InlineData("KEY", "")]
    public async Task ActivateAsync_rejects_missing_inputs(string licenseKey, string instanceName)
    {
        var (client, _) = CreateSut();

        var act = () => client.ActivateAsync(licenseKey, instanceName);

        await act.Should().ThrowAsync<ArgumentException>();
    }
}
