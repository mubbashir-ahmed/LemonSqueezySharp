namespace LemonSqueezy.NET.Exceptions;

/// <summary>
/// Thrown when an inbound webhook payload fails HMAC-SHA256 signature verification.
/// </summary>
public sealed class LemonSqueezyWebhookVerificationException : LemonSqueezyException
{
    private const string DefaultMessage = "The webhook signature could not be verified.";

    public LemonSqueezyWebhookVerificationException()
        : base(DefaultMessage)
    {
    }

    public LemonSqueezyWebhookVerificationException(string message)
        : base(message)
    {
    }

    public LemonSqueezyWebhookVerificationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
