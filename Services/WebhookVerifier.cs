using System.Security.Cryptography;
using System.Text;
using LemonSqueezy.NET.Configurations;
using LemonSqueezy.NET.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace LemonSqueezy.NET.Services;

/// <inheritdoc cref="IWebhookVerifier"/>
public sealed class WebhookVerifier : IWebhookVerifier
{
    private readonly IOptionsMonitor<LemonSqueezyOptions> _options;

    public WebhookVerifier(IOptionsMonitor<LemonSqueezyOptions> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public bool Verify(string rawBody, string signatureHeader)
    {
        if (rawBody is null)
        {
            throw new ArgumentNullException(nameof(rawBody));
        }

        return Verify(Encoding.UTF8.GetBytes(rawBody), signatureHeader);
    }

    public bool Verify(ReadOnlySpan<byte> rawBody, string signatureHeader)
    {
        if (string.IsNullOrWhiteSpace(signatureHeader))
        {
            return false;
        }

        var secret = _options.CurrentValue.WebhookSigningSecret;
        if (string.IsNullOrEmpty(secret))
        {
            throw new InvalidOperationException(
                $"{nameof(LemonSqueezyOptions.WebhookSigningSecret)} must be configured " +
                "before webhook signatures can be verified.");
        }

        byte[] expected;
#if NET10_0_OR_GREATER
        expected = new byte[32];
        HMACSHA256.HashData(Encoding.UTF8.GetBytes(secret), rawBody, expected);
#else
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
        {
            expected = hmac.ComputeHash(rawBody.ToArray());
        }
#endif

        if (!TryParseHex(signatureHeader, out var provided) || provided.Length != expected.Length)
        {
            return false;
        }

        return FixedTimeEquals(expected, provided);
    }

    private static bool FixedTimeEquals(byte[] left, byte[] right)
    {
#if NET10_0_OR_GREATER
        return CryptographicOperations.FixedTimeEquals(left, right);
#else
        if (left.Length != right.Length)
        {
            return false;
        }

        var accum = 0;
        for (var i = 0; i < left.Length; i++)
        {
            accum |= left[i] ^ right[i];
        }

        return accum == 0;
#endif
    }

    private static bool TryParseHex(string value, out byte[] bytes)
    {
        bytes = Array.Empty<byte>();
        if (value.Length % 2 != 0)
        {
            return false;
        }

        var buffer = new byte[value.Length / 2];
        for (var i = 0; i < buffer.Length; i++)
        {
            if (!byte.TryParse(
                    value.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out buffer[i]))
            {
                return false;
            }
        }

        bytes = buffer;
        return true;
    }
}
