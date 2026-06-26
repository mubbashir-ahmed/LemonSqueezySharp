using LemonSqueezy.NET.Exceptions;
using LemonSqueezy.NET.Models.Webhooks.Events;

namespace LemonSqueezy.NET.Interfaces.Services;

/// <summary>
/// Parses raw LemonSqueezy webhook payloads into the strongly-typed
/// <see cref="LemonSqueezyWebhookEvent"/> hierarchy so callers can pattern-match per event.
/// </summary>
public interface IWebhookEventParser
{
    /// <summary>
    /// Parses an already-verified payload. Known event names map to their sealed record type;
    /// unrecognised ones return an <see cref="UnknownWebhookEvent"/>.
    /// </summary>
    /// <remarks>This does not verify the signature — only call it on a trusted body.</remarks>
    /// <exception cref="LemonSqueezyException">The body is not valid JSON or is missing <c>meta</c>.</exception>
    LemonSqueezyWebhookEvent Parse(string rawBody);

    /// <summary>
    /// Verifies the <c>X-Signature</c> header against <paramref name="rawBody"/>, then parses it.
    /// </summary>
    /// <exception cref="LemonSqueezyWebhookVerificationException">The signature did not match.</exception>
    LemonSqueezyWebhookEvent VerifyAndParse(string rawBody, string signatureHeader);

    /// <summary>
    /// Non-throwing variant of <see cref="VerifyAndParse"/>: returns <c>false</c> and a
    /// <c>null</c> event when the signature does not match.
    /// </summary>
    bool TryVerifyAndParse(string rawBody, string signatureHeader, out LemonSqueezyWebhookEvent? webhookEvent);
}
