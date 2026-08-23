using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using LemonSqueezySharp.Configurations;
using LemonSqueezySharp.Services;
using Microsoft.Extensions.Options;

namespace LemonSqueezySharp.Tests.Services;

public class WebhookVerifierTests
{
    private const string Secret = "supersecret";
    private readonly WebhookVerifier _verifier;

    public WebhookVerifierTests()
    {
        var optionsMonitor = Substitute.For<IOptionsMonitor<LemonSqueezyOptions>>();
        optionsMonitor.CurrentValue.Returns(new LemonSqueezyOptions
        {
            ApiKey = "irrelevant",
            WebhookSigningSecret = Secret,
        });

        _verifier = new WebhookVerifier(optionsMonitor);
    }

    [Fact]
    public void Returns_true_for_matching_signature()
    {
        const string body = """{"meta":{"event_name":"order_created"}}""";
        var signature = ComputeSignature(body, Secret);

        _verifier.Verify(body, signature).Should().BeTrue();
    }

    [Fact]
    public void Returns_false_for_mismatched_signature()
    {
        const string body = """{"meta":{"event_name":"order_created"}}""";
        var signature = ComputeSignature(body, "wrong-secret");

        _verifier.Verify(body, signature).Should().BeFalse();
    }

    [Fact]
    public void Returns_false_for_empty_signature_header()
    {
        _verifier.Verify("payload", string.Empty).Should().BeFalse();
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
