namespace LemonSqueezy.NET.Services;

/// <summary>
/// Verifies inbound LemonSqueezy webhook payloads against the
/// <c>X-Signature</c> header using HMAC-SHA256 over the raw request body.
/// </summary>
public interface IWebhookVerifier
{
    /// <summary>
    /// Returns <c>true</c> when the signature matches an HMAC-SHA256 of
    /// <paramref name="rawBody"/> produced with the configured signing secret.
    /// </summary>
    bool Verify(string rawBody, string signatureHeader);

    /// <summary>
    /// Overload accepting the raw byte payload directly — useful in middleware
    /// where the body has already been buffered.
    /// </summary>
    bool Verify(ReadOnlySpan<byte> rawBody, string signatureHeader);
}
