using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using LemonSqueezy.NET.Configurations;
using LemonSqueezy.NET.Exceptions;
using LemonSqueezy.NET.Models.Disputes;
using LemonSqueezy.NET.Models.Webhooks.Events;
using LemonSqueezy.NET.Services;
using Microsoft.Extensions.Options;

namespace LemonSqueezy.NET.Tests.Services;

public class WebhookEventParserTests
{
    private const string Secret = "supersecret";
    private readonly WebhookEventParser _parser;

    public WebhookEventParserTests()
    {
        var optionsMonitor = Substitute.For<IOptionsMonitor<LemonSqueezyOptions>>();
        optionsMonitor.CurrentValue.Returns(new LemonSqueezyOptions
        {
            ApiKey = "irrelevant",
            WebhookSigningSecret = Secret,
        });

        _parser = new WebhookEventParser(new WebhookVerifier(optionsMonitor));
    }

    [Fact]
    public void Parse_maps_order_created_to_typed_event()
    {
        const string body = """
        {
          "meta": { "event_name": "order_created", "webhook_id": "wh_1" },
          "data": {
            "type": "orders",
            "id": "42",
            "attributes": { "user_email": "buyer@example.com", "identifier": "abc-123" }
          }
        }
        """;

        var evt = _parser.Parse(body);

        var order = evt.Should().BeOfType<OrderCreatedEvent>().Subject;
        order.EventName.Should().Be("order_created");
        order.Meta.WebhookId.Should().Be("wh_1");
        order.Data!.Id.Should().Be("42");
        order.Data.Type.Should().Be("orders");
        order.Data.Attributes!.UserEmail.Should().Be("buyer@example.com");
    }

    [Fact]
    public void Parse_maps_subscription_payment_success_to_invoice_event()
    {
        const string body = """
        {
          "meta": { "event_name": "subscription_payment_success" },
          "data": { "type": "subscription-invoices", "id": "7", "attributes": {} }
        }
        """;

        var evt = _parser.Parse(body);

        var payment = evt.Should().BeOfType<SubscriptionPaymentSuccessEvent>().Subject;
        payment.Data!.Id.Should().Be("7");
    }

    [Fact]
    public void Parse_maps_customer_updated_to_typed_event()
    {
        const string body = """
        {
          "meta": { "event_name": "customer_updated" },
          "data": { "type": "customers", "id": "3", "attributes": { "email": "c@example.com" } }
        }
        """;

        var evt = _parser.Parse(body);

        var customer = evt.Should().BeOfType<CustomerUpdatedEvent>().Subject;
        customer.Data!.Attributes!.Email.Should().Be("c@example.com");
    }

    [Theory]
    [InlineData("dispute_created", typeof(DisputeCreatedEvent))]
    [InlineData("dispute_resolved", typeof(DisputeResolvedEvent))]
    public void Parse_maps_dispute_events_to_typed_events(string eventName, Type expected)
    {
        var body = $$"""
        {
          "meta": { "event_name": "{{eventName}}" },
          "data": { "type": "disputes", "id": "9", "attributes": { "reason": "fraudulent" } }
        }
        """;

        var evt = _parser.Parse(body);

        evt.Should().BeOfType(expected);
        var dispute = evt.Should().BeAssignableTo<LemonSqueezyWebhookEvent<Dispute>>().Subject;
        dispute.Data!.Attributes!.Reason.Should().Be("fraudulent");
    }

    [Theory]
    [MemberData(nameof(AllEventNames))]
    public void Every_documented_event_name_maps_to_a_typed_event(string eventName)
    {
        var body = $$"""{ "meta": { "event_name": "{{eventName}}" }, "data": { "id": "1" } }""";

        var evt = _parser.Parse(body);

        evt.Should().NotBeOfType<UnknownWebhookEvent>(
            "event '{0}' should map to a strongly-typed event", eventName);
        evt.EventName.Should().Be(eventName);
    }

    public static TheoryData<string> AllEventNames()
    {
        var data = new TheoryData<string>();
        foreach (var field in typeof(WebhookEventNames).GetFields(
            BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
        {
            if (field is { IsLiteral: true, IsInitOnly: false } && field.GetRawConstantValue() is string value)
            {
                data.Add(value);
            }
        }

        return data;
    }

    [Fact]
    public void Parse_returns_unknown_event_for_unrecognised_name()
    {
        const string body = """
        {
          "meta": { "event_name": "something_new" },
          "data": { "type": "mystery", "id": "1" }
        }
        """;

        var evt = _parser.Parse(body);

        var unknown = evt.Should().BeOfType<UnknownWebhookEvent>().Subject;
        unknown.EventName.Should().Be("something_new");
        unknown.RawData.GetProperty("type").GetString().Should().Be("mystery");
    }

    [Fact]
    public void Parse_exposes_custom_data_via_generic_accessor()
    {
        const string body = """
        {
          "meta": {
            "event_name": "order_created",
            "custom_data": { "user_id": "u_99", "plan": "pro" }
          },
          "data": { "type": "orders", "id": "1" }
        }
        """;

        var evt = _parser.Parse(body);

        var custom = evt.GetCustomData<Dictionary<string, string>>();
        custom.Should().NotBeNull();
        custom!["user_id"].Should().Be("u_99");
        custom["plan"].Should().Be("pro");
    }

    [Fact]
    public void Parse_returns_null_custom_data_when_absent()
    {
        const string body = """{ "meta": { "event_name": "order_created" }, "data": { "id": "1" } }""";

        var evt = _parser.Parse(body);

        evt.GetCustomData<Dictionary<string, string>>().Should().BeNull();
    }

    [Fact]
    public void Parse_throws_when_meta_is_missing()
    {
        const string body = """{ "data": { "id": "1" } }""";

        var act = () => _parser.Parse(body);

        act.Should().Throw<LemonSqueezyException>();
    }

    [Fact]
    public void Parse_throws_for_invalid_json()
    {
        var act = () => _parser.Parse("not-json");

        act.Should().Throw<LemonSqueezyException>();
    }

    [Fact]
    public void VerifyAndParse_returns_typed_event_for_valid_signature()
    {
        const string body = """{ "meta": { "event_name": "order_created" }, "data": { "id": "5" } }""";
        var signature = ComputeSignature(body, Secret);

        var evt = _parser.VerifyAndParse(body, signature);

        evt.Should().BeOfType<OrderCreatedEvent>();
    }

    [Fact]
    public void VerifyAndParse_throws_for_invalid_signature()
    {
        const string body = """{ "meta": { "event_name": "order_created" } }""";
        var signature = ComputeSignature(body, "wrong-secret");

        var act = () => _parser.VerifyAndParse(body, signature);

        act.Should().Throw<LemonSqueezyWebhookVerificationException>();
    }

    [Fact]
    public void TryVerifyAndParse_returns_true_and_event_for_valid_signature()
    {
        const string body = """{ "meta": { "event_name": "order_created" }, "data": { "id": "5" } }""";
        var signature = ComputeSignature(body, Secret);

        var ok = _parser.TryVerifyAndParse(body, signature, out var evt);

        ok.Should().BeTrue();
        evt.Should().BeOfType<OrderCreatedEvent>();
    }

    [Fact]
    public void TryVerifyAndParse_returns_false_for_invalid_signature()
    {
        const string body = """{ "meta": { "event_name": "order_created" } }""";
        var signature = ComputeSignature(body, "wrong-secret");

        var ok = _parser.TryVerifyAndParse(body, signature, out var evt);

        ok.Should().BeFalse();
        evt.Should().BeNull();
    }

    private static string ComputeSignature(string body, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));

        var builder = new StringBuilder(hash.Length * 2);
        foreach (var b in hash)
        {
            builder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
        }

        return builder.ToString();
    }
}
